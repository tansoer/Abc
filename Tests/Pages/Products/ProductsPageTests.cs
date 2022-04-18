using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Aids.Random;
using Abc.Aids.Reflection;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Products {

    [TestClass]
    public class ProductsPageTests : SealedViewsFactoryPageTests<
        ProductsPage,
        IProductsRepo,
        IProduct,
        ProductView,
        ProductData,
        ProductViewFactory,
        ProductKind> {

        protected override Type getBaseClass() => typeof(ViewsFactoryPage<ProductsPage, IProductsRepo, IProduct, ProductView, ProductData, ProductViewFactory, ProductKind>);
 
        protected override string pageTitle => ProductTitles.Products;

        protected override string pageUrl => ProductUrls.Products;

        protected override IProduct toObject(ProductData d) => ProductFactory.Create(d);

        private class testRepo
            : mockRepo<IProduct, ProductData>,
                IProductsRepo { }

        private class unitsRepo
            : mockRepo<Unit, UnitData>,
                IUnitsRepo { }

        private class typesRepo
            : mockRepo<IProductType, ProductTypeData>,
                IProductTypesRepo { }

        private class batchesRepo
            : mockRepo<Batch, BatchData>,
                IBatchesRepo { }
        private class inclusiveRulesRepo
            :mockRepo<IProductInclusionRule, ProductInclusionRuleData>,
                IProductInclusionRulesRepo { }

        private testRepo Repo;
        private unitsRepo units;
        private batchesRepo batches;
        private typesRepo types;
        private inclusiveRulesRepo rules;
        private ProductData data;
        private ProductTypeData typeData;
        private UnitData unitData;
        private BatchData batchData;
        private string selectedId;

        protected override ProductsPage createObject() {
        selectedId = rndStr;
            Repo = new testRepo();
            units = new unitsRepo();
            types = new typesRepo();
            batches = new batchesRepo();
            rules = new inclusiveRulesRepo();
            data = GetRandom.ObjectOf<ProductData>();
            typeData = GetRandom.ObjectOf<ProductTypeData>();
            unitData = GetRandom.ObjectOf<UnitData>();
            batchData = GetRandom.ObjectOf<BatchData>();
            addRandomProducts();
            addRandomProductTypes();
            addRandomUnits();
            addRandomBatches();
            return new ProductsPage(Repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(Repo, units, types, batches, rules);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void addRandomBatches() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? batchData : GetRandom.ObjectOf<BatchData>();
                batches.Add(new Batch(d));
            }
        }

        private void addRandomProducts() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<ProductData>();
                Repo.Add(ProductFactory.Create(d));
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

        private void addRandomProductTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? typeData : GetRandom.ObjectOf<ProductTypeData>();
                types.Add(ProductTypeFactory.Create(d));
            }
        }

        [TestMethod]
        public override void ToObjectTest() {
            var view = GetRandom.ObjectOf<ProductView>();
            var o = obj.toObject(view);
            allPropertiesAreEqual(view, o.Data);
        }

        [TestMethod]
        public override void ToViewTest() {
            var d = GetRandom.ObjectOf<ProductData>();
            var view = obj.toView(toObject(d));
            allPropertiesAreEqual(view, d);
        }

        [TestMethod]
        public void UnitsTest() {
            var list = units.Get();
            Assert.AreEqual(list.Count + 1, obj.Units.Count());
        }

        [TestMethod]
        public void BatchesTest() {
            var list = batches.Get();
            Assert.AreEqual(list.Count + 1, obj.Batches.Count());
        }

        [TestMethod]
        public void ProductTypesTest() {
            var list = types.Get();
            areEqual(list.Count + 1, obj.ProductTypes.Count());
        }

        [TestMethod]
        public void ProductTypeNameTest() {
            obj.updateProductTypes(typeData.ProductKind);
            var name = obj.ProductTypeName(typeData.Id);
            Assert.AreEqual(typeData.Name, name);
        }

        [TestMethod]
        public void UnitNameTest() {
            var name = obj.UnitName(unitData.Id);
            Assert.AreEqual(unitData.Name, name);
        }

        [TestMethod]
        public void BatchNameTest() {
            var name = obj.BatchName(batchData.Id);
            Assert.AreEqual(batchData.Name, name);
        }

        [TestMethod] public void InclusionRulesTest() => isProperty<List<ProductInclusionRuleView>>();

        [TestMethod]
        public async Task OnGetShowFeaturesAsyncTest() {
            var page = await obj.OnGetShowFeaturesAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            isInstanceOfType(page, typeof(RedirectResult));
        }

        [TestMethod]
        public async Task OnGetShowPricesAsyncTest() {
            var page = await obj.OnGetShowPricesAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            isInstanceOfType(page, typeof(RedirectResult));
        }

        [TestMethod]
        public async Task OnGetShowPackageContentsAsyncTest() {
            var page = await obj.OnGetShowPackageContentsAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            isInstanceOfType(page, typeof(RedirectResult));
        }

        [TestMethod]
        public async Task OnGetShowContainerItemsAsyncTest() {
            var page = await obj.OnGetShowContainerItemsAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            isInstanceOfType(page, typeof(RedirectResult));
        }

        [TestMethod]
        public void OnGetDetailsAsyncTest() {
            var id = rndStr;
            var page = obj.OnGetDetailsAsync(id, sortOrder, searchString, pageIndex, fixedFilter, fixedValue);
            Assert.IsInstanceOfType(page, typeof(Task<IActionResult>));
            testPageProperties();
        }

        private T testBackProperty<T>(T value) {
            obj.FixedFilter = GetMember.Name<ProductData>(x => x.BatchId);
            obj.FixedValue = selectedId;

            return value;
        }

        [DataRow(true, "This package is valid")]
        [DataRow(false, "This package is not valid")]
        [DataTestMethod] public void GetPackageValidationMessageTest(bool isValid, string message) {
            areEqual(message, obj.GetPackageValidationMessage(isValid));
        }

        [TestMethod]
        public void ValidatePackageTest() {
            var td = GetRandom.ObjectOf<ProductTypeData>();
            td.ProductKind = ProductKind.Package;
            var packageType = (PackageType)ProductTypeFactory.Create(td);
            GetRepo.Instance<IProductTypesRepo>().Add(packageType);

            obj.Item = GetRandom.ObjectOf<ProductView>();
            obj.Item.ProductKind = ProductKind.Package;
            obj.Item.ProductTypeId = td.Id;
            var package = (Package) new ProductViewFactory().Create(obj.Item);

            areEqual(packageType.Validate(package), obj.ValidatePackage());
        }

        [TestMethod]
        public void LoadInclusionRulesTest() {
            var packageData = GetRandom.ObjectOf<ProductData>();
            packageData.ProductKind = ProductKind.Package;

            var packageTypeData = GetRandom.ObjectOf<ProductTypeData>();
            packageTypeData.ProductKind = ProductKind.Package;
            packageTypeData.Id = packageData.ProductTypeId;
            GetRepo.Instance<IProductTypesRepo>().Add(ProductTypeFactory.Create(packageTypeData));

            var inclusionRulesRepo = GetRepo.Instance<IProductInclusionRulesRepo>();
            int ruleCount = GetRandom.UInt8(5, 10);

            for (var i = 0; i < ruleCount; i++) {
                var inclusionRuleData = GetRandom.ObjectOf<ProductInclusionRuleData>();
                inclusionRuleData.PackageTypeId = packageTypeData.Id;
                inclusionRuleData.InclusionKind = ProductInclusionKind.Ordinal;
                inclusionRulesRepo.Add(ProductInclusionRuleFactory.Create(inclusionRuleData));
            }

            obj.Item = obj.toView(ProductFactory.Create(packageData));
            obj.LoadInclusionRules();
            areEqual(ruleCount, obj.InclusionRules.Count);
        }
    }
}


