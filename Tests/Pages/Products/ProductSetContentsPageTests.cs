using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Products {
    [TestClass]
    public class ProductSetContentsPageTests :SealedViewFactoryPageTests<ProductSetContentsPage,
        IProductSetContentsRepo, ProductSetContent, ProductSetContentView, ProductSetContentData,
        ProductSetContentViewFactory> {
        protected override string pageTitle => ProductTitles.ProductSetContents;
        protected override string pageUrl => ProductUrls.ProductSetContents;
        private ProductSetContentData Data;
        private ProductSetData productSetData;
        private ProductTypeData productTypeData;

        protected override ProductSetContent toObject(ProductSetContentData d)
            => new(d);

        private class Repo :mockRepo<ProductSetContent, ProductSetContentData>, IProductSetContentsRepo { }
        private class productSetsRepo :mockRepo<ProductSet, ProductSetData>, IProductSetsRepo { }
        private class productTypesRepo :mockRepo<IProductType, ProductTypeData>, IProductTypesRepo { }
        private Repo repo;
        private productSetsRepo productSets;
        private productTypesRepo productTypes;

        protected override ProductSetContentsPage createObject() {
            initializeRepos();           
            setRandomData();
            addRandomDataToRepos();
            return new ProductSetContentsPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, productSets, productTypes);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            productSets = new();
            productTypes = new();
        }
        private void setRandomData() {
            Data = GetRandom.ObjectOf<ProductSetContentData>();
            productSetData = GetRandom.ObjectOf<ProductSetData>();
            productTypeData = GetRandom.ObjectOf<ProductTypeData>();
        }
        private void addRandomDataToRepos() {
            addRandomProductSetContents();
            addRandomProductSets();
            addRandomProductTypes();
        }

        private void addRandomProductSetContents() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? Data : GetRandom.ObjectOf<ProductSetContentData>();
                repo.Add(new(d));
            }
        }
        private void addRandomProductSets() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? productSetData : GetRandom.ObjectOf<ProductSetData>();
                productSets.Add(new(d));
            }
        }
        private void addRandomProductTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? productTypeData : GetRandom.ObjectOf<ProductTypeData>();
                productTypes.Add(ProductTypeFactory.Create(d));
            }
        }

        [TestMethod]
        public void ProductSetsTest() {
            var list = productSets.Get();
            Assert.AreEqual(list.Count + 1, obj.ProductSets.Count());
        }

        [TestMethod]
        public void ProductTypesTest() {
            var list = productTypes.Get();
            Assert.AreEqual(list.Count + 1, obj.ProductTypes.Count());
        }

        [TestMethod]
        public void ProductSetNameTest() {
            var name = obj.ProductSetName(productSetData.Id);
            Assert.AreEqual(productSetData.Name, name);
        }

        [TestMethod]
        public void ProductTypeNameTest() {
            var name = obj.ProductTypeName(productTypeData.Id);
            Assert.AreEqual(productTypeData.Name, name);
        }
    }
}
