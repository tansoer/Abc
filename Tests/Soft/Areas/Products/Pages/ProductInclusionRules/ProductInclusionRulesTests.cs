using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Products.Packets;
using Abc.Domain.Quantities;
using Abc.Facade.Products;
using Abc.Infra.Quantities;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Products.Pages.ProductInclusionRules {
    public abstract class ProductInclusionRulesTests :BaseProductTests<ProductInclusionRuleView, ProductInclusionRuleData> {
        protected ProductInclusionKind? inclusionKind;
        protected List<SelectListItem> units => createSelectList(getContext<QuantityDb>().Units);
        protected List<SelectListItem> productSets => createSelectList(db.ProductSets);
        protected List<SelectListItem> productInclusionRules => createSelectList(db.ProductInclusions);
        protected List<SelectListItem> packageTypes => createSelectList(db.ProductTypes, x => x.ProductKind == ProductKind.Package);
        protected override string baseUrl() => ProductUrls.ProductInclusionRules;
        protected override void addRelatedItems(ProductInclusionRuleData d) {
            if (d is null) return;
            addUnit(d.UnitId);
            addProductSet(d.ProductSetId);
            addProductInclusion(d.ProductInclusionId);
            addProductType(d.PackageTypeId, ProductKind.Package);
        }
        private void addUnit(string id) {
            var ud = GetRandom.ObjectOf<UnitData>();
            ud.Id = id;
            GetRepo.Instance<IUnitsRepo>().Add(UnitFactory.Create(ud));
        }
        protected override void changeValuesExceptId(ProductInclusionRuleData d) {
            var id = d.Id;
            var productKind = d.InclusionKind;
            d = GetRandom.ObjectOf<ProductInclusionRuleData>();
            d.Id = id;
            d.InclusionKind = productKind;
            addRelatedItems(d);
        }
        protected override ProductInclusionRuleData correctOriginal(ProductInclusionRuleData oldItem, ProductInclusionRuleData newItem) {
            oldItem.InclusionKind = newItem.InclusionKind;
            update<IProductInclusionRulesRepo, IProductInclusionRule>(ProductInclusionRuleFactory.Create(oldItem));
            return oldItem;
        }
        protected override string getItemId(ProductInclusionRuleData d) => d.Id;
        protected override void setItemId(ProductInclusionRuleData d, string id) => d.Id = id;
        protected override ProductInclusionRuleData randomItem() {
            var d = base.randomItem();
            d.InclusionKind = inclusionKind ?? d.InclusionKind;
            correctProperties(d);
            return d;
        }
        private static void correctProperties(ProductInclusionRuleData d) {
            d.MinimumAmount = GetRandom.Double(-1000, 1000);
            d.MaximumAmount = GetRandom.Double(-1000, 1000);
            d.ValidFrom = GetRandom.DateTime(DateTime.Now.AddYears(-10), DateTime.Now).Date;
            d.ValidTo = GetRandom.DateTime(DateTime.Now, DateTime.Now.AddYears(10)).Date;
        }
        protected override ProductInclusionRuleView toView(ProductInclusionRuleData d) {
            var o = ProductInclusionRuleFactory.Create(d);
            var v = new ProductInclusionRuleViewFactory().Create(o);
            return v;
        }
        protected override string pageUrl() {
            var url = base.pageUrl();
            if (inclusionKind is null) return url;
            var typeIdx = Convert.ToInt32(inclusionKind);
            url += $"&switchOfCreate={typeIdx}";
            return url;
        }
        protected override IEnumerable<Expression<Func<ProductInclusionRuleView, object>>> indexPageColumns =>
            new Expression<Func<ProductInclusionRuleView, object>>[] {
                x => x.Name,
                x => x.InclusionKind,
                x => x.MinimumAmount,
                x => x.MaximumAmount,
                x => x.UnitId,
                x => x.PackageTypeId,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            ProductInclusionRuleView view = toView(item);
            canView(view, m => m.Id);
            canView(view, m => m.Name);
            canView(view, m => m.Code);
            canView(view, m => m.Details);
            canView(view, m => m.InclusionKind);
            canView(view, m => m.MinimumAmount);
            canView(view, m => m.MaximumAmount);
            canView(view, m => m.UnitId, Unspecified.String);
            canView(view, m => m.PackageTypeId, Unspecified.String);            
            canView(view, m => m.ProductSetId, Unspecified.String);
            if (item.InclusionKind is ProductInclusionKind.Conditional or ProductInclusionKind.Detail)
                canView(view, m => m.ProductInclusionId, Unspecified.String);
            canView(view, m => m.ValidFrom);
            canView(view, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            ProductInclusionRuleView view = toView(item);
            inclusionKind = view.InclusionKind;
            canEdit(view, m => m.Name, true);
            canEdit(view, m => m.Code);
            canEdit(view, m => m.Details);
            canEdit(view, m => m.MinimumAmount, true, true, 0);
            canEdit(view, m => m.MaximumAmount, true, true, 0);
            canSelect(view, m => m.UnitId, units);
            canSelect(view, m => m.PackageTypeId, packageTypes);
            canSelect(view, m => m.ProductSetId, productSets);
            if (inclusionKind is ProductInclusionKind.Conditional or ProductInclusionKind.Detail)
                canSelect(view, m => m.ProductInclusionId, productInclusionRules);
            canEdit(view, m => m.ValidFrom);
            canEdit(view, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.MinimumAmount, true, true, 0);
            canEdit(null, m => m.MaximumAmount, true, true, 0);
            canSelect(null, m => m.UnitId, units);
            canSelect(null, m => m.PackageTypeId, packageTypes);
            canSelect(null, m => m.ProductSetId, productSets);
            if (inclusionKind is ProductInclusionKind.Conditional or ProductInclusionKind.Detail)
                canSelect(null, m => m.ProductInclusionId, productInclusionRules);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void validateItems(ProductInclusionRuleData d1, ProductInclusionRuleData d2) {
            var except = composeExcepted(d1);
            allPropertiesAreEqual(d1, d2, except);
        }
        private static string[] composeExcepted(ProductInclusionRuleData d) {
            var l = new List<string> {
                nameof(d.ProductInclusionId)
            };
            if (d.InclusionKind is ProductInclusionKind.Conditional or ProductInclusionKind.Detail)
                l.Remove(nameof(d.ProductInclusionId));
            return l.ToArray();
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            hasHidden(item, x => x.InclusionKind, true);
        }
        [DataTestMethod]
        [DataRow(ProductInclusionKind.Detail)]
        [DataRow(ProductInclusionKind.Conditional)]
        [DataRow(ProductInclusionKind.Ordinal)]
        [DataRow(ProductInclusionKind.Unspecified)]
        public void CanDisplayDataTest(ProductInclusionKind k) {
            inclusionKind = k;
            CanDisplayDataTest();
        }
    }
    [TestClass] public class CreatePageTests :ProductInclusionRulesTests {
        [DataTestMethod]
        [DataRow(ProductInclusionKind.Detail)]
        [DataRow(ProductInclusionKind.Conditional)]
        [DataRow(ProductInclusionKind.Ordinal)]
        [DataRow(ProductInclusionKind.Unspecified)]
        public void CanClickCreateButtonTest(ProductInclusionKind k) {
            inclusionKind = k;
            canClickCreateButtonTest();
        }
    }
    [TestClass] public class DeletePageTests :ProductInclusionRulesTests {
        [DataTestMethod]
        [DataRow(ProductInclusionKind.Detail)]
        [DataRow(ProductInclusionKind.Conditional)]
        [DataRow(ProductInclusionKind.Ordinal)]
        [DataRow(ProductInclusionKind.Unspecified)]
        public void CanClickDeleteButtonTest(ProductInclusionKind k) {
            inclusionKind = k;
            canClickDeleteButtonTest();
        }
    }
    [TestClass] public class DetailsPageTests :ProductInclusionRulesTests {
        [DataTestMethod]
        [DataRow(ProductInclusionKind.Detail)]
        [DataRow(ProductInclusionKind.Conditional)]
        [DataRow(ProductInclusionKind.Ordinal)]
        [DataRow(ProductInclusionKind.Unspecified)]
        public void CanClickEditButtonInDetailsTest(ProductInclusionKind t) {
            inclusionKind = t;
            canClickEditButtonInDetailsTest();
        }
    }
    [TestClass] public class EditPageTests :ProductInclusionRulesTests {
        [DataTestMethod]
        [DataRow(ProductInclusionKind.Detail)]
        [DataRow(ProductInclusionKind.Conditional)]
        [DataRow(ProductInclusionKind.Ordinal)]
        [DataRow(ProductInclusionKind.Unspecified)]
        public void CanClickEditButtonTest(ProductInclusionKind t) {
            inclusionKind = t;
            canClickEditButtonTest();
        }
    }
    [TestClass] public class IndexPageTests :ProductInclusionRulesTests { }
}
