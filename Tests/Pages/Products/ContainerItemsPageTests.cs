using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Products {
    [TestClass]
    public class ContainerItemsPageTests :SealedViewPageTests<ContainerItemsPage, IContainerItemsRepo,
        ContainerItem, ContainerItemView, ContainerItemData> {
        protected override string pageTitle => ProductTitles.ContainerItems;
        protected override string pageUrl => ProductUrls.ContainerItems;
        protected override ContainerItem toObject(ContainerItemData d) => new(d);

        private ContainerItemData Data;
        private ProductData ProductData;

        private class Repo :mockRepo<ContainerItem, ContainerItemData>, IContainerItemsRepo { }
        private class ProductsRepo :mockRepo<IProduct, ProductData>, IProductsRepo { }
        private Repo repo;
        private ProductsRepo products;

        protected override ContainerItemsPage createObject() {
            repo = new();
            products = new();
            Data = GetRandom.ObjectOf<ContainerItemData>();
            ProductData = GetRandom.ObjectOf<ProductData>();
            addRandomContainerItems();
            addRandomContainers();
            addRandomProducts();
            return new ContainerItemsPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, products);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void addRandomContainerItems() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? Data : GetRandom.ObjectOf<ContainerItemData>();
                repo.Add(new(d));
            }
        }
        private void addRandomContainers() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? ProductData : GetRandom.ObjectOf<ProductData>();
                d.ProductKind = ProductKind.Container;
                products.Add(ProductFactory.Create(d));
            }
        }
        private void addRandomProducts() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? ProductData : GetRandom.ObjectOf<ProductData>();
                d.ProductKind = ProductKind.Product;
                products.Add(ProductFactory.Create(d));
            }
        }

        [TestMethod] public void ProductsTest() {
            var list = products.Get();
            Assert.AreEqual(list.Count + 1, obj.Products.Count());
        }

        [TestMethod] public void ContainersTest() {
            var list = products.Get().Where(x => x.ProductKind == ProductKind.Container).ToList();
            Assert.AreEqual(list.Count + 1, obj.Containers.Count());
        }

        [TestMethod]
        public void ValueForTest() {
            var mock = new mockHtmlHelper<ContainerItemsPage>();
            Assert.IsNull(obj.ValueFor(mock, 0));
            Assert.IsNull(obj.ValueFor(mock, 1));
        }

        [TestMethod] public void ProductNameTest() {
            var name = obj.ProductName(ProductData.Id);
            Assert.AreEqual(ProductData.Name, name);
        }

        [TestMethod]
        public void OnGetCreateTest() {
            var page = obj.OnGetCreate(sortOrder, searchString, pageIndex, fixedFilter, fixedValue, createSwitch);
            Assert.IsInstanceOfType(page, typeof(PageResult));
            testPageProperties();
        }
    }
}
