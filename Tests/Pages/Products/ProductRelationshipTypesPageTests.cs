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
    public class ProductRelationshipTypesPageTests :SealedViewPageTests<ProductRelationshipTypesPage,
        IProductRelationshipTypesRepo, ProductRelationshipType, ProductRelationshipTypeView, ProductRelationshipTypeData> {
        protected override string pageTitle => ProductTitles.ProductRelationshipTypes;
        protected override string pageUrl => ProductUrls.ProductRelationshipTypes;
        protected override ProductRelationshipType toObject(ProductRelationshipTypeData d) => new(d);

        private class productRelationshipTypesRepo :mockRepo<ProductRelationshipType, ProductRelationshipTypeData>, IProductRelationshipTypesRepo { };
        private class productTypesRepo: mockRepo<IProductType, ProductTypeData>, IProductTypesRepo { };

        private productRelationshipTypesRepo productRelationshipTypes;
        private productTypesRepo productTypes;

        private ProductRelationshipTypeData productRelationshipTypeData;
        private ProductTypeData productTypeData;

        protected override ProductRelationshipTypesPage createObject() {
            productRelationshipTypes = new();
            productTypes = new();
            productRelationshipTypeData = GetRandom.ObjectOf<ProductRelationshipTypeData>();
            productTypeData = GetRandom.ObjectOf<ProductTypeData>();
            addRandomProductRelationshipTypes();
            addRandomProductTypes();
            return new ProductRelationshipTypesPage(productRelationshipTypes);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(productRelationshipTypes, productTypes);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void addRandomProductRelationshipTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? productRelationshipTypeData : GetRandom.ObjectOf<ProductRelationshipTypeData>();
                productRelationshipTypes.Add(new ProductRelationshipType(d));
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
        public void BaseTypesTest() {
            var list = productRelationshipTypes.Get();
            Assert.AreEqual(list.Count + 1, obj.BaseTypes.Count());
        }

        [TestMethod]
        public void ProductTypesTest() {
            var list = productTypes.Get();
            Assert.AreEqual(list.Count + 1, obj.ProductTypes.Count());
        }

        [TestMethod]
        public void ValueForTest() {
            var mock = new mockHtmlHelper<ProductRelationshipTypesPage>();
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
        public void ProductTypeNameTest() {
            var name = obj.ProductTypeName(productTypeData.Id);
            Assert.AreEqual(productTypeData.Name, name);
        }

        [TestMethod]
        public void BaseTypeNameTest() {
            var name = obj.BaseTypeName(productRelationshipTypeData.Id);
            Assert.AreEqual(productRelationshipTypeData.Name, name);
        }
    }
}
