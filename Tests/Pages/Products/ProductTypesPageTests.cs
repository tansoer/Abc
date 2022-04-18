using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Data.Quantities;
using Abc.Domain.Products;
using Abc.Domain.Quantities;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Abc.Pages.Products;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Abc.Domain.Common;

namespace Abc.Tests.Pages.Products {

    [TestClass]
    public class ProductTypesPageTests : SealedViewsFactoryPageTests<
        ProductTypesPage,
        IProductTypesRepo,
        IProductType,
        ProductTypeView,
        ProductTypeData,
        ProductTypeViewFactory,
        ProductKind> {

        private ProductTypeData data;
        private UnitData unitData;

        protected override string pageTitle => ProductTitles.ProductTypes;

        protected override string pageUrl => ProductUrls.ProductTypes;

        protected override IProductType toObject(ProductTypeData d) => ProductTypeFactory.Create(d);

        private class testRepo
            : mockRepo<IProductType, ProductTypeData>,
                IProductTypesRepo { }

        private class unitsRepo
            : mockRepo<Unit, UnitData>,
                IUnitsRepo { }

        private testRepo repo;
        private unitsRepo units;

        protected override ProductTypesPage createObject() {
            repo = new testRepo();
            units = new unitsRepo();
            data = GetRandom.ObjectOf<ProductTypeData>();
            unitData = GetRandom.ObjectOf<UnitData>();
            addRandomProductTypes();
            addRandomUnits();
            var o = new ProductTypesPage(repo);
            return o;
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, units);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
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
                var d = i == idx ? data : GetRandom.ObjectOf<ProductTypeData>();
                d.IsBaseType = true;
                repo.Add(ProductTypeFactory.Create(d));

                if (i != idx) continue;
                var x = GetRandom.ObjectOf<ProductTypeData>();
                x.Id = d.BaseTypeId;
                x.ProductKind = d.ProductKind;
                x.IsBaseType = true;
                repo.Add(ProductTypeFactory.Create(x));
            }
        }

        [TestMethod]
        public void BaseTypeNameTest() {
            obj.updateProductTypes(data.ProductKind);
            var name = obj.BaseTypeName(data.Id);
            Assert.AreEqual(data.Name, name);
        }

        [TestMethod]
        public void UnitNameTest() {
            var name = obj.UnitName(unitData.Id);
            Assert.AreEqual(unitData.Name, name);
        }

        [TestMethod]
        public void UnitsTest() {
            var list = units.Get();
            Assert.AreEqual(list.Count + 1, obj.Units.Count());
        }

        [TestMethod]
        public void ProductTypesTest() {
            var list = repo.Get();
            Assert.AreEqual(list.Count + 1, obj.ProductTypes.Count());
        }

        [TestMethod]
        public async Task OnGetShowProductInstancesAsyncTest() {
            var page = await obj.OnGetShowProductInstancesAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            isInstanceOfType(page, typeof(RedirectResult));
        }

        [TestMethod]
        public async Task OnGetShowFeatureTypesAsyncTest() {
            var page = await obj.OnGetShowFeatureTypesAsync(rndStr, sortOrder, searchString, pageIndex,
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
        public async Task OnGetShowPackageComponentsAsyncTest() {
            var page = await obj.OnGetShowPackageComponentsAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            isInstanceOfType(page, typeof(RedirectResult));
        }

        [TestMethod]
        public async Task OnGetShowInclusionRulesAsyncTest() {
            var page = await obj.OnGetShowInclusionRulesAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            isInstanceOfType(page, typeof(RedirectResult));
        }

        [TestMethod]
        public async Task OnGetShowProductSetsAsyncTest() {
            var page = await obj.OnGetShowProductSetsAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            isInstanceOfType(page, typeof(RedirectResult));
        }
    }
}