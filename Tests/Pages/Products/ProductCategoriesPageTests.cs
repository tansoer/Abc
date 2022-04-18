using System;
using System.Linq;
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
    public class ProductCategoriesPageTests : SealedViewFactoryPageTests<
        ProductCategoriesPage,
        IProductCategoriesRepo,
        ProductCategory,
        ProductCategoryView,
        ProductCategoryData,
        ProductCategoryViewFactory> {

        private ProductCategoryData data;

        protected override string pageTitle => ProductTitles.ProductCategories;

        protected override string pageUrl => ProductUrls.ProductCategories;

        protected override ProductCategory toObject(ProductCategoryData d) => new(d);

        private class testRepo
            : mockRepo<ProductCategory, ProductCategoryData>,
                IProductCategoriesRepo { }

        private testRepo Repo;

        protected override ProductCategoriesPage createObject() {
        Repo = new testRepo();
            addRandomProductTypes();
            return new ProductCategoriesPage(Repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(Repo);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void addRandomProductTypes() {
            var count = GetRandom.Int32(5, 10);
            var idx = GetRandom.Int32(1, count - 1);

            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<ProductCategoryData>();
                if (i == idx) data = d;
                Repo.Add(new ProductCategory(d));
            }
        }

        [TestMethod]
        public void CategoryNameTest() {
            var name = obj.CategoryName(data.Id);
            Assert.AreEqual(data.Name, name);
        }

        [TestMethod]
        public void CategoriesTest() {
            var list = Repo.Get();
            Assert.AreEqual(list.Count + 1, obj.Categories.Count());
        }

        [TestMethod]
        public async Task OnGetShowCatalogEntriesAsyncTest() {
            var page = await obj.OnGetShowCatalogEntriesAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            Assert.IsInstanceOfType(page, typeof(RedirectResult));
        }
    }

}