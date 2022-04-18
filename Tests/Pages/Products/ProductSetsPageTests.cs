using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.Tests.Pages.Products {
    [TestClass]
    public class ProductSetsPageTests :SealedViewFactoryPageTests<ProductSetsPage, IProductSetsRepo,
        ProductSet, ProductSetView, ProductSetData, ProductSetViewFactory> {
        protected override string pageTitle => ProductTitles.ProductSets;
        protected override string pageUrl => ProductUrls.ProductSets;

        private ProductSetData Data;
        private ProductTypeData productTypeData;

        protected override ProductSet toObject(ProductSetData d) => new(d);

        private class Repo : mockRepo<ProductSet, ProductSetData>, IProductSetsRepo { }
        private class ProductTypesRepo : mockRepo<IProductType, ProductTypeData>, IProductTypesRepo { }
        private Repo repo;
        private ProductTypesRepo productTypes;

        protected override ProductSetsPage createObject() {
            repo = new();
            productTypes = new();
            Data = GetRandom.ObjectOf<ProductSetData>();
            productTypeData = GetRandom.ObjectOf<ProductTypeData>();
            addRandomProductSets();
            addRandomPackageTypes();
            return new ProductSetsPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, productTypes);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void addRandomProductSets() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? Data : GetRandom.ObjectOf<ProductSetData>();
                repo.Add(new(d));
            }
        }

        private void addRandomPackageTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? productTypeData : GetRandom.ObjectOf<ProductTypeData>();
                d.ProductKind = ProductKind.Package;
                productTypes.Add(ProductTypeFactory.Create(d));
            }
        }

        [TestMethod]
        public void PackageTypesTest() {
            var list = productTypes.Get();
            Assert.AreEqual(list.Count + 1, obj.PackageTypes.Count());
        }

        [TestMethod]
        public void PackageTypeNameTest() {
            var name = obj.PackageTypeName(productTypeData.Id);
            Assert.AreEqual(productTypeData.Name, name);
        }

        [TestMethod]
        public async Task OnGetShowProductSetContentsAsyncTest() {
            var page = await obj.OnGetShowProductSetContentsAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            isInstanceOfType(page, typeof(RedirectResult));
        }
    }
}
