using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Catalog;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Pages.Products {
    [TestClass]
    public class CatalogedProductPageTests : SealedViewFactoryPageTests<
        CatalogedProductPage,
        ICatalogedProductsRepo,
        CatalogedProduct,
        CatalogedProductView,
        CatalogedProductData,
        CatalogedProductViewFactory> {

        protected override string pageTitle => ProductTitles.CatalogedProducts;
        protected override string pageUrl => ProductUrls.CatalogedProducts;
        protected override CatalogedProduct toObject(CatalogedProductData d) => new(d);
        private class testRepo
            : mockRepo<CatalogedProduct, CatalogedProductData>,
                ICatalogedProductsRepo {
        }
        private class catalogEntriesRepo
            : mockRepo<CatalogEntry, CatalogEntryData>,
                ICatalogEntriesRepo { }
        private class productTypesRepo
            : mockRepo<IProductType, ProductTypeData>,
                IProductTypesRepo { }
        private testRepo Repo;
        private catalogEntriesRepo catalogEntries;
        private productTypesRepo productTypes;
        protected override CatalogedProductPage createObject() {
        Repo = new testRepo();
            catalogEntries = new catalogEntriesRepo();
            productTypes = new productTypesRepo();
            addRandomCatalogEntries();
            addRandomProductTypes();
            return new CatalogedProductPage(Repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(catalogEntries, productTypes);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void addRandomCatalogEntries() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var d = GetRandom.ObjectOf<CatalogEntryData>();
                catalogEntries.Add(new CatalogEntry(d));
            }
        }
        private void addRandomProductTypes() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var d = GetRandom.ObjectOf<ProductTypeData>();
                productTypes.Add(new ProductType(d));
            }
        }

        [TestMethod]
        public void CatalogEntryNameTest() {
            var t = catalogEntries.list[GetRandom.Int32(0, catalogEntries.list.Count - 1)];
            var n = obj.CatalogEntryName(t.Id);
            Assert.AreEqual(n, t.Data.Name);
        }

        [TestMethod]
        public void ProductTypeNameTest() {
            var t = productTypes.list[GetRandom.Int32(0, productTypes.list.Count - 1)];
            var n = obj.ProductTypeName(t.Id);
            Assert.AreEqual(n, t.Data.Name);
        }

        [TestMethod]
        public void CatalogEntriesTest() => isReadOnly(obj.CatalogEntries);

        [TestMethod]
        public void ProductTypesTest() => isReadOnly(obj.ProductTypes);
    }
}