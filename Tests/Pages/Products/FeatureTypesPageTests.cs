using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Features;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Abc.Pages.Products;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Abc.Tests.Pages.Products {

    [TestClass]
    public class FeatureTypesPageTests : SealedViewFactoryPageTests<
        FeatureTypesPage, IFeatureTypesRepo,
        FeatureType, FeatureTypeView, FeatureTypeData, FeatureTypeViewFactory> {

        protected override string pageTitle => ProductTitles.FeatureTypes;
        protected override string pageUrl => ProductUrls.FeatureTypes;
        protected override FeatureType toObject(FeatureTypeData d) => new(d);

        internal class testRepo
            : mockRepo<FeatureType, FeatureTypeData>,
                IFeatureTypesRepo {
        }

        private class typesRepo
            : mockRepo<IProductType, ProductTypeData>,
                IProductTypesRepo {
        }

        private testRepo Repo;
        private typesRepo productTypesRepo;

        protected override FeatureTypesPage createObject() {
        Repo = new testRepo();
            productTypesRepo = new typesRepo();
            addRandomFeatureTypes();
            return new FeatureTypesPage(Repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(Repo, productTypesRepo);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void addRandomFeatureTypes() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var d = GetRandom.ObjectOf<FeatureTypeData>();
                Repo.Add(new FeatureType(d));
                var td = GetRandom.ObjectOf<ProductTypeData>();
                td.Id = d.ProductTypeId;
                productTypesRepo.Add(ProductTypeFactory.Create(td));
            }
        }

        [TestMethod]
        public void ProductTypeNameTest() {
            var t = productTypesRepo.list[GetRandom.Int32(0, productTypesRepo.list.Count - 1)];
            var n = obj.ProductTypeName(t.Id);
            Assert.AreEqual(n, t.Name);
        }

        [TestMethod]
        public void ProductTypesTest() => isReadOnly(obj.ProductTypes);

        [TestMethod]
        public async Task OnGetShowFeaturesAsyncTest() {
            var page = await obj.OnGetShowFeaturesAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            isInstanceOfType(page, typeof(RedirectResult));
        }

        [TestMethod]
        public async Task OnGetShowPossibleValuesAsyncTest() {
            var page = await obj.OnGetShowPossibleValuesAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            isInstanceOfType(page, typeof(RedirectResult));
        }
    }

}