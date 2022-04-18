//TODO : Currently, the code is implemented only for the counted minimum and maximum

using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Reflection;
using Abc.Core.Units;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Quantities;

namespace Abc.Domain.Products.Packets {

    public sealed class PackageType : BaseProductType {

        private static string packageTypeId => GetMember.Name<PackageComponentData>(x => x.PackageTypeId);

        private Package currentPackage;
        private List<IProduct> productsToValidate;
        private BaseProductInclusionRule currentRule;
        private List<IProduct> currentProducts;

        public PackageType(ProductTypeData d = null) : base(d) { }

        public IReadOnlyList<PackageComponent> PackageComponents =>
            new GetFrom<IPackageComponentsRepo, PackageComponent>().ListBy(packageTypeId, Id);

        public IReadOnlyList<IProductType> Components =>
            PackageComponents.Select(x => x.ProductType).ToList().AsReadOnly();

        public IReadOnlyList<ProductSetContent> RelatedSets =>
            new GetFrom<IProductSetContentsRepo, ProductSetContent>().ListBy(productTypeId, Id);

        public IReadOnlyList<ProductSet> ProductSets => RelatedSets.Select(x => x.ProductSet).ToList().AsReadOnly();

        public IReadOnlyList<IProductInclusionRule> InclusionRules =>
            new GetFrom<IProductInclusionRulesRepo, IProductInclusionRule>().ListBy(packageTypeId, Id);

        public bool Validate(Package p) {
            currentPackage = p;
            productsToValidate = allPackageProducts();
            if (p is null) return false;

            return isPackageTypeCorrect && isPackageContentCorrect && isAllValidated;
        }
        internal bool isAllValidated => productsToValidate.Count == 0;

        private List<IProduct> allPackageProducts() {
            var l = new List<IProduct>();
            l.AddRange(currentPackage.Contents);

            return l;
        }

        private bool isPackageContentCorrect => InclusionRules.All(isComposedAccordingToRule);


        private bool isComposedAccordingToRule(IProductInclusionRule i) {
            var o = i as BaseProductInclusionRule;
            var b = isComposedAccordingToRule(o);
            if (b) removeValidated(o);
            return b;
        }
        private void removeValidated(BaseProductInclusionRule r) {
            setAttributes(r);
            if (r is ProductInclusionRule || r is ProductInclusionRuleDetail)
                removeValidated();
        }

        private bool isComposedAccordingToRule(BaseProductInclusionRule i) {
            setAttributes(i);
            var b = i switch
            {
                ProductInclusionRule _ => isComposedAccordingToRule(),
                ProductInclusionRuleCondition c => isComposedAccordingToRule(c),
                ProductInclusionRuleDetail d => isComposedAccordingToRule(d),
                _ => false
            };

            return b;
        }

        internal void removeValidated() {
            if (areCountersInUse) removeValidated(currentRule.Maximum.Amount);
            else throw new NotImplementedException();
        }

        internal void removeValidated(double max) {
            for (var i = productsToValidate.Count - 1; i >= 0; i--) {
                var p = productsToValidate[i];

                if (currentRule.ProductSet.Contents.All(x => x.Id != p.TypeId)) continue;

                productsToValidate.Remove(p);
                max -= 1;

                if (max < 1) return;
            }
        }

        internal bool isComposedAccordingToRule() => isCorrect();

        internal bool isComposedAccordingToRule(ProductInclusionRuleDetail d) => isCorrect(d?.MasterInclusion);

        internal bool isComposedAccordingToRule(ProductInclusionRuleCondition d) => isCorrect(d?.DetailedInclusions);

        internal bool isSelectedFromSet => selectedFromSetCount > 0.0;

        internal bool isAllowedByOtherRule {
            get {
                removeAllowedByOtherRule();
                var c = currentProducts.Count;
                return c == 0;
            }
        }

        internal void removeAllowedByOtherRule() {
            var products = currentProducts;
            foreach (var i in InclusionRules) {
                if (!(i is ProductInclusionRuleDetail r)) continue;
                if (currentRule.Id == r.Id) continue;
                if (!r.ProductSet.ContainsAny(products)) continue;
                setAttributes(r.MasterInclusion);
                var isOk = isCorrect();
                if (isOk) removeAllowedByOtherRule(products, r.ProductSet);
            }

            currentProducts = products;
        }

        internal void removeAllowedByOtherRule(List<IProduct> products, ProductSet s) {
            if (s is null) return;
            for (var i = products.Count; i > 0; i--) {
                var p = products[i - 1];
                if (s.Contents.Any(x => x.Id == p.TypeId)) products.Remove(p);
            }
        }

        internal double selectedFromSetCount {
            get {
                currentProducts = new List<IProduct>();
                return currentPackage
                    .Contents
                    .Aggregate(0.0,
                        (count, p) =>
                            currentRule.ProductSet.Contents.Where(t => isThisProduct(p, t))
                                .Aggregate(count, (x, t) => x + 1));
            }
        }

        internal bool isThisProduct(IProduct p, IProductType t) {
            var b = p.TypeId == t.Id;
            if (b) currentProducts?.Add(p);
            return b;
        }

        internal bool areCountersInUse => isCounter(currentRule.Minimum.Unit) && isCounter(currentRule.Maximum.Unit);

        internal static bool isCounter(Unit u) => u?.MeasureId == Counter.Measure.Id;

        internal bool isPackageTypeCorrect => currentPackage.TypeId == Id;

        internal bool isCorrect()
            => areCountersInUse
                ? isCorrect(currentRule.Minimum.Amount, currentRule.Maximum.Amount)
                : throw new NotImplementedException();

        internal bool isCorrect(double minimum, double maximum) {
            var count = selectedFromSetCount;

            if (count < minimum) return false;

            return !(count > maximum);
        }

        internal bool isCorrect(ProductInclusionRuleCondition master) {
            var b = areCountersInUse
                ? isCorrect(master, currentRule.Minimum.Amount, currentRule.Maximum.Amount)
                : throw new NotImplementedException();

            return b;
        }

        internal bool isCorrect(BaseProductInclusionRule master, double minimum, double maximum) {
            if (!isSelectedFromSet) return true;

            if (!isCorrect(minimum, maximum)) return false;
            var r = currentRule;
            setAttributes(master);

            var b = isCorrect();

            if (b) return true;

            setAttributes(r);
            isCorrect(r.MinimumAmount, r.MaximumAmount);
            b = isAllowedByOtherRule;

            return b;

        }

        private bool isCorrect(IReadOnlyList<ProductInclusionRuleDetail> details) {
            if (!isSelectedFromSet) return true;
            foreach (var r in details) {
                setAttributes(r);
                var b = isCorrect();
                if (!b) return false;
            }

            return true;
        }

        internal void setAttributes(BaseProductInclusionRule r) => currentRule = r;

    }
}