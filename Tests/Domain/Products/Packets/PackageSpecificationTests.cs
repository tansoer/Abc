using Abc.Aids.Random;
using Abc.Core.Units;
using Abc.Data.Products;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SystemOfUnits = Abc.Domain.Quantities.SystemOfUnits;

namespace Abc.Tests.Domain.Products.Packets {

    public abstract class PackageSpecificationTests {

        private readonly IProductTypesRepo types;
        private readonly IProductSetsRepo sets;
        private readonly IProductSetContentsRepo setContents;
        private readonly IProductInclusionRulesRepo inclusions;
        private readonly IProductsRepo products;
        private readonly IPackageContentsRepo packageContents;

        protected PackageSpecificationTests() {
            types = GetRepo.Instance<IProductTypesRepo>();
            sets = GetRepo.Instance<IProductSetsRepo>();
            setContents = GetRepo.Instance<IProductSetContentsRepo>();
            inclusions = GetRepo.Instance<IProductInclusionRulesRepo>();
            products = GetRepo.Instance<IProductsRepo>();
            packageContents = GetRepo.Instance<IPackageContentsRepo>();

            var m = addMeasure();
            var su = addSystemOfUnits();
            var u = addUnit(m);
            addUnitFactor(u, su);
        }

        internal static SystemOfUnits addSystemOfUnits() {
            var r = GetRepo.Instance<ISystemsOfUnitsRepo>();
            var su = new SystemOfUnits(new SystemOfUnitsData {
                Id = Abc.Core.Units.SystemOfUnits.SiSystemId
            });
            r.Add(su);

            return su;
        }
        internal static void addUnitFactor(Abc.Domain.Quantities.Unit u, SystemOfUnits su) {
            var unitFactors = GetRepo.Instance<IUnitFactorsRepo>();
            var uf = new UnitFactor(new UnitFactorData { Factor = 1.0, UnitId = u.Id, SystemOfUnitsId = su.Id });
            unitFactors.Add(uf);
        }
        internal static Abc.Domain.Quantities.Unit addUnit(Measure m) {
            var units = GetRepo.Instance<IUnitsRepo>();
            var u = new FactoredUnit(new UnitData {
                Id = Counter.unitName,
                MeasureId = m.Id
            });
            units.Add(u);

            return u;
        }
        internal Measure addMeasure() {
            var measures = GetRepo.Instance<IMeasuresRepo>();
            var m = new BaseMeasure(new MeasureData {
                Id = Counter.Measure.Id,
                Name = Counter.Measure.Name,
                Code = Counter.Measure.Code,
                Details = Counter.Measure.Definition

            });
            measures.Add(m);

            return m;
        }
        protected ProductType crProductType(string name) {
            var d = new ProductTypeData { Name = name, ProductKind = ProductKind.Product };
            var o = ProductTypeFactory.Create(d) as ProductType;
            types.Add(o);

            return o;
        }

        protected ProductSet crProductSet(string name, params ProductType[] productTypes) {
            var d = new ProductSetData { Name = name };
            var o = new ProductSet(d);
            sets.Add(o);

            foreach (var p in productTypes) {
                setContents.Add(
                        new ProductSetContent(
                            new ProductSetContentData { ProductSetId = o.Id, ProductTypeId = p.Id }));
            }

            return o;
        }

        protected ProductInclusionRule addProductInclusion(PackageType package, ProductSet set, int min, int max) {

            var d = new ProductInclusionRuleData {
                Name = set?.Name,
                PackageTypeId = package?.Id,
                InclusionKind = ProductInclusionKind.Ordinal,
                ProductSetId = set?.Id,
                MaximumAmount = max,
                MinimumAmount = min,
                UnitId = Counter.unitName
            };
            var o = ProductInclusionRuleFactory.Create(d) as ProductInclusionRule;
            inclusions.Add(o);

            return o;

        }

        protected IProductInclusionRule addDetailInclusion(PackageType package, ProductInclusionRuleCondition condition,
            ProductSet set, int min, int max) {
            var d = new ProductInclusionRuleData {
                Name = set?.Name,
                PackageTypeId = package?.Id,
                InclusionKind = ProductInclusionKind.Detail,
                ProductSetId = set?.Id,
                ProductInclusionId = condition?.Id,
                MaximumAmount = max,
                MinimumAmount = min,
                UnitId = Counter.unitName
            };
            var o = ProductInclusionRuleFactory.Create(d) as ProductInclusionRuleDetail;
            inclusions.Add(o);

            return o;
        }
        protected ProductInclusionRuleCondition addConditionalInclusion(PackageType package, ProductSet set, int min,
            int max) {
            var d = new ProductInclusionRuleData {
                Name = set?.Name,
                PackageTypeId = package?.Id,
                InclusionKind = ProductInclusionKind.Conditional,
                ProductSetId = set?.Id,
                MaximumAmount = max,
                MinimumAmount = min,
                UnitId = Counter.unitName
            };
            var o = ProductInclusionRuleFactory.Create(d) as ProductInclusionRuleCondition;
            inclusions.Add(o);

            return o;
        }

        protected Package crPackage(PackageType t, ProductType[] productTypes) {
            var d = new ProductData {
                ProductKind = t.ProductKind,
                ProductTypeId = t.Id,
                Name = t.Name
            };
            var o = ProductFactory.Create(d) as Package;
            products.Add(o);
            foreach (var p in productTypes) addPackageContent(o, p);

            return o;
        }

        protected void addPackageContent(Package package, ProductType t) {
            var p = addProduct(t);
            addPackageContent(package, t, p);
        }

        private void addPackageContent(Package package, ProductType t, Product p) {
            var d = new PackageContentData {
                PackageId = package.Id, ProductId = p.Id, ComponentId = t.Id
            };
            packageContents.Add(new PackageContent(d));
        }

        private Product addProduct(ProductType t) {
            var d = new ProductData {
                ProductKind = t.ProductKind,
                ProductTypeId = t.Id,
                Name = t.Name
            };
            var o = ProductFactory.Create(d) as Product;
            products.Add(o);

            return o;
        }

        protected void isNotCorrectPackage(PackageType t, params ProductType[] productTypes) {
            var p = crPackage(t, productTypes);
            Assert.IsFalse(t.Validate(p));
        }

        protected void isCorrectPackage(PackageType t, params ProductType[] productTypes) {
            var p = crPackage(t, productTypes);
            Assert.IsTrue(t.Validate(p));
        }

        protected ProductType getRandomProductType(ProductSet set) {
            var x = GetRandom.Int32(0, set.NumberOfElements);
            var o = set.GetElement(x) as ProductType;

            return o;
        }

    }

}