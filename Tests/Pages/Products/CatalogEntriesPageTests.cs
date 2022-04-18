using System;
using System.Threading.Tasks;
using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products.Catalog;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Products {
    [TestClass]
    public class CatalogEntriesPageTests :SealedViewFactoryPageTests<
        CatalogEntriesPage,
        ICatalogEntriesRepo,
        CatalogEntry,
        CatalogEntryView,
        CatalogEntryData,
        CatalogEntryViewFactory> {

        protected override string pageTitle => ProductTitles.CatalogEntries;
        protected override string pageUrl => ProductUrls.CatalogEntries;
        protected override CatalogEntry toObject(CatalogEntryData d) => new(d);

        private class testRepo
            :mockRepo<CatalogEntry, CatalogEntryData>,
                ICatalogEntriesRepo {
        }

        private class testProductCatalogsRepo
            :mockRepo<ProductCatalog, ProductCatalogData>,
                IProductCatalogsRepo {
        }

        private class testProductCategoriesRepo
            :mockRepo<ProductCategory, ProductCategoryData>,
                IProductCategoriesRepo {
        }

        private testRepo Repo;
        private testProductCatalogsRepo productCatalogsRepo;
        private testProductCategoriesRepo productCategoriesRepo;

        protected override CatalogEntriesPage createObject() {
        Repo = new testRepo();
            productCatalogsRepo = new testProductCatalogsRepo();
            productCategoriesRepo = new testProductCategoriesRepo();
            addRandomCatalogEntries();
            addRandomCatalogs();
            addRandomCategories();
            return new CatalogEntriesPage(Repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(productCatalogsRepo, productCategoriesRepo);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void addRandomCatalogEntries() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                Repo.Add(new CatalogEntry(GetRandom.ObjectOf<CatalogEntryData>()));
        }

        private void addRandomCatalogs() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                productCatalogsRepo.Add(new ProductCatalog(GetRandom.ObjectOf<ProductCatalogData>()));
        }

        private void addRandomCategories() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                productCategoriesRepo.Add(new ProductCategory(GetRandom.ObjectOf<ProductCategoryData>()));
        }

        [TestMethod]
        public async Task OnGetShowCatalogedProductsAsyncTest() {
            var page = await obj.OnGetShowCatalogedProductsAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            Assert.IsInstanceOfType(page, typeof(RedirectResult));
        }

        [TestMethod]
        public void CatalogNameTest() {
            var catalog = productCatalogsRepo.list[GetRandom.Int32(0, productCatalogsRepo.list.Count - 1)];
            var actual = obj.CatalogName(catalog.Id);
            Assert.AreEqual(catalog.Name, actual);
        }

        [TestMethod]
        public void CategoryNameTest() {
            var category =
                productCategoriesRepo.list[GetRandom.Int32(0, productCategoriesRepo.list.Count - 1)];
            var actual = obj.CategoryName(category.Id);
            Assert.AreEqual(category.Name, actual);
        }

        [TestMethod]
        public void CatalogsTest() => isReadOnly(obj.Catalogs);

        [TestMethod]
        public void CategoriesTest() => isReadOnly(obj.Categories);
    }
}