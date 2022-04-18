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
    public class ProductRelationshipsPageTests :SealedViewPageTests<
        ProductRelationshipsPage, IProductRelationshipsRepo, ProductRelationship, ProductRelationshipView, ProductRelationshipData> {
        protected override string pageTitle => ProductTitles.ProductRelationships;
        protected override string pageUrl => ProductUrls.ProductRelationships;

        protected override ProductRelationship toObject(ProductRelationshipData d) => new (d);

        private class productRelationshipsRepo :mockRepo<ProductRelationship, ProductRelationshipData>, IProductRelationshipsRepo { };
        private class productRelationshipTypesRepo :mockRepo<ProductRelationshipType, ProductRelationshipTypeData>, IProductRelationshipTypesRepo { };
        private class productsRepo :mockRepo<IProduct, ProductData>, IProductsRepo { };

        private productRelationshipsRepo productRelationships;
        private productRelationshipTypesRepo relationshipTypes;
        private productsRepo products;

        private ProductRelationshipData productRelationshipData;
        private ProductRelationshipTypeData relationshipTypeData;
        private ProductData productData;

        protected override ProductRelationshipsPage createObject() {
            productRelationships = new ();
            relationshipTypes = new();
            products = new();
            productRelationshipData = GetRandom.ObjectOf<ProductRelationshipData>();
            relationshipTypeData = GetRandom.ObjectOf<ProductRelationshipTypeData>();
            productData = GetRandom.ObjectOf<ProductData>();
            addRandomProductRelationships();
            addRandomProductRelationshipTypes();
            addRandomProducts();
            return new ProductRelationshipsPage(productRelationships);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(productRelationships, relationshipTypes, products);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void addRandomProductRelationships() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? productRelationshipData : GetRandom.ObjectOf<ProductRelationshipData>();
                productRelationships.Add(new ProductRelationship(d));
            }
        }

        private void addRandomProductRelationshipTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? relationshipTypeData : GetRandom.ObjectOf<ProductRelationshipTypeData>();
                relationshipTypes.Add(new (d));
            }
        }

        private void addRandomProducts() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? productData : GetRandom.ObjectOf<ProductData>();
                d.ProductKind = ProductKind.Product;
                products.Add(ProductFactory.Create(d));
            }
        }

        [TestMethod]
        public void ProductRelationshipTypesTest() {
            var list = relationshipTypes.Get();
            Assert.AreEqual(list.Count + 1, obj.ProductRelationshipTypes.Count());
        }

        [TestMethod]
        public void ProductsTest() {
            var list = products.Get();
            Assert.AreEqual(list.Count + 1, obj.Products.Count());
        }

        [TestMethod]
        public void ValueForTest() {
            var mock = new mockHtmlHelper<ProductRelationshipsPage>();
            Assert.IsNull(obj.ValueFor(mock, 0));
            Assert.IsNull(obj.ValueFor(mock, 1));
            Assert.IsNull(obj.ValueFor(mock, 2));
        }

        [TestMethod]
        public void OnGetCreateTest() {
            var page = obj.OnGetCreate(sortOrder, searchString, pageIndex, fixedFilter, fixedValue, createSwitch);
            Assert.IsInstanceOfType(page, typeof(PageResult));
            testPageProperties();
        }

        [TestMethod]
        public void ProductRelationshipTypeNameTest() {
            var name = obj.ProductRelationshipTypeName(relationshipTypeData.Id);
            Assert.AreEqual(relationshipTypeData.Name, name);
        }

        [TestMethod]
        public void ProductNameTest() {
            var name = obj.ProductName(productData.Id);
            Assert.AreEqual(productData.Name, name);
        }
    }
}
