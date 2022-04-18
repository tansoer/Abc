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
    public class PackageContentsPageTests :SealedViewFactoryPageTests<PackageContentsPage, IPackageContentsRepo, PackageContent,
        PackageContentView, PackageContentData, PackageContentViewFactory> {
        protected override string pageTitle => ProductTitles.PackageContents;
        protected override string pageUrl => ProductUrls.PackageContents;
        private PackageContentData data;
        private ProductData productData;
        private PackageComponentData componentData;
        protected override PackageContent toObject(PackageContentData d) => new(d);

        private class Repo : mockRepo<PackageContent, PackageContentData>, IPackageContentsRepo { }
        private class productsRepo : mockRepo<IProduct, ProductData>, IProductsRepo { }
        private class componentsRepo : mockRepo<PackageComponent, PackageComponentData>, IPackageComponentsRepo { }
        private class typesRepo : mockRepo<IProductType, ProductTypeData>, IProductTypesRepo { }
        private Repo repo;
        private productsRepo products;
        private componentsRepo components;
        private typesRepo productTypes;

        protected override PackageContentsPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos(); 
            return new PackageContentsPage(repo, products);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, products, components, productTypes);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            products = new();
            components = new();
            productTypes = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<PackageContentData>();
            productData = GetRandom.ObjectOf<ProductData>();
            componentData = GetRandom.ObjectOf<PackageComponentData>();
        }
        private void addRandomDataToRepos() {
            addRandomPackageContents();
            addRandomProducts();
            addRandomPackages();
            addRandomComponents();
        }

        private void addRandomPackageContents() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<PackageContentData>();
                repo.Add(new PackageContent(d));
            }
        }
        private void addRandomProducts() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? productData : GetRandom.ObjectOf<ProductData>();
                if (d.ProductKind == ProductKind.Package) d.ProductKind = ProductKind.Service;
                products.Add(ProductFactory.Create(d));
            }
        }
        private void addRandomPackages() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? productData : GetRandom.ObjectOf<ProductData>();
                d.ProductKind = ProductKind.Package;
                products.Add(ProductFactory.Create(d));
            }
        }
        private void addRandomComponents() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? componentData : GetRandom.ObjectOf<PackageComponentData>();
                components.Add(new(d));
            }
        }

        [TestMethod]
        public void ProductsTest() {
            var list = products.Get();
            areEqual(list.Count + 1, obj.Products.Count());
        }

        [TestMethod]
        public void PackagesTest() {
            var list = products.Get().Where(x => x.ProductKind == ProductKind.Package).ToList();
            areEqual(list.Count + 1, obj.Packages.Count());
        }

        [TestMethod]
        public void ComponentsTest() {
            var list = components.Get();
            areEqual(list.Count + 1, obj.Components.Count());
        }

        [TestMethod]
        public void getPackageTypeIdTest() {
            var packageTypeData = GetRandom.ObjectOf<ProductTypeData>();
            packageTypeData.ProductKind = ProductKind.Package;
            productTypes.Add(ProductTypeFactory.Create(packageTypeData));

            var packageData = GetRandom.ObjectOf<ProductData>();
            packageData.ProductKind = ProductKind.Package;
            packageData.ProductTypeId = packageTypeData.Id;
            products.Add(ProductFactory.Create(packageData));

            string actual = obj.getPackageTypeId(packageData.Id);
            areEqual(packageTypeData.Id, actual);
        }

        [TestMethod]
        public void ProductNameTest() {
            var name = obj.ProductName(productData.Id);
            areEqual(productData.Name, name);
        }

        [TestMethod]
        public void ComponentNameTest() {
            var name = obj.ComponentName(componentData.Id);
            areEqual(componentData.Name, name);
        }
    }
}
