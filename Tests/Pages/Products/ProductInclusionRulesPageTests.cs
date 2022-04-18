using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Abc.Domain.Quantities;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Abc.Pages.Products;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Products {
    [TestClass]
    public class ProductInclusionRulesPageTests :SealedViewsFactoryPageTests<
        ProductInclusionRulesPage,
        IProductInclusionRulesRepo,
        IProductInclusionRule,
        ProductInclusionRuleView,
        ProductInclusionRuleData,
        ProductInclusionRuleViewFactory,
        ProductInclusionKind> {

        private ProductInclusionRuleData data;
        private UnitData unitData;
        private ProductSetData productSetData;
        private ProductTypeData packageTypeData;

        protected override string pageTitle => ProductTitles.ProductInclusionRules;
        protected override string pageUrl => ProductUrls.ProductInclusionRules;

        protected override IProductInclusionRule toObject(ProductInclusionRuleData d) => ProductInclusionRuleFactory.Create(d);

        private class testRepo : mockRepo<IProductInclusionRule, ProductInclusionRuleData>, IProductInclusionRulesRepo { }
        private class unitsRepo : mockRepo<Unit, UnitData>, IUnitsRepo { }
        private class productSetsRepo : mockRepo<ProductSet, ProductSetData>, IProductSetsRepo { }
        private class productTypesRepo : mockRepo<IProductType, ProductTypeData>, IProductTypesRepo { }
        
        private testRepo repo;
        private unitsRepo units;
        private productSetsRepo productSets;
        private productTypesRepo productTypes;

        protected override ProductInclusionRulesPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new ProductInclusionRulesPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, units, productSets, productTypes);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void initializeRepos() {
            repo = new();
            units = new();
            productSets = new();
            productTypes = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<ProductInclusionRuleData>();
            unitData = GetRandom.ObjectOf<UnitData>();
            productSetData = GetRandom.ObjectOf<ProductSetData>();
            packageTypeData = GetRandom.ObjectOf<ProductTypeData>();
        }        
        private void addRandomDataToRepos() {
            addRandomProductInclusionRules();
            addRandomUnits();
            addRandomProductSets();
            addRandomPackageTypes();
        }

        private void addRandomProductInclusionRules() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<ProductInclusionRuleData>();
                repo.Add(ProductInclusionRuleFactory.Create(d));
            }
        }
        private void addRandomUnits() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? unitData : GetRandom.ObjectOf<UnitData>();
                units.Add(UnitFactory.Create(d));
            }
        }
        private void addRandomProductSets() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? productSetData : GetRandom.ObjectOf<ProductSetData>();
                productSets.Add(new ProductSet(d));
            }
        }
        private void addRandomPackageTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? packageTypeData : GetRandom.ObjectOf<ProductTypeData>();
                d.ProductKind = ProductKind.Package;
                productTypes.Add(ProductTypeFactory.Create(d));
            }
        }

        [TestMethod]
        public void UnitsTest() {
            var list = units.Get();
            Assert.AreEqual(list.Count + 1, obj.Units.Count());
        }

        [TestMethod]
        public void ProductSetsTest() {
            var list = productSets.Get();
            Assert.AreEqual(list.Count + 1, obj.ProductSets.Count());
        }

        [TestMethod]
        public void ProductInclusionRulesTest() {
            var list = repo.Get();
            Assert.AreEqual(list.Count + 1, obj.ProductInclusionRules.Count());
        }

        [TestMethod]
        public void PackageTypesTest() {
            var list = productTypes.Get();
            Assert.AreEqual(list.Count + 1, obj.PackageTypes.Count());
        }

        [TestMethod]
        public void UnitNameTest() {
            var name = obj.UnitName(unitData.Id);
            Assert.AreEqual(unitData.Name, name);
        }

        [TestMethod]
        public void ProductSetNameTest() {
            var name = obj.ProductSetName(productSetData.Id);
            Assert.AreEqual(productSetData.Name, name);
        }

        [TestMethod]
        public void MasterInclusionRuleNameTest() {
            var name = obj.MasterInclusionRuleName(data.Id);
            Assert.AreEqual(data.Name, name);
        }

        [TestMethod]
        public void PackageTypeNameTest() {
            var name = obj.PackageTypeName(packageTypeData.Id);
            Assert.AreEqual(packageTypeData.Name, name);
        }

        [DataRow(ProductInclusionKind.Unspecified, false)]
        [DataRow(ProductInclusionKind.Ordinal, false)]
        [DataRow(ProductInclusionKind.Conditional, true)]
        [DataRow(ProductInclusionKind.Detail, true)]
        [DataTestMethod]
        public void ShouldShowMasterInclusionRuleIdTest(ProductInclusionKind kind, bool expected) {
            areEqual(obj.ShouldShowMasterInclusionRuleId(kind), expected);
        }
    }
}
