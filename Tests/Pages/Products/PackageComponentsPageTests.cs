using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Products {
    [TestClass]
    public class PackageComponentsPageTests :SealedViewFactoryPageTests<PackageComponentsPage,
        IPackageComponentsRepo, PackageComponent, PackageComponentView, PackageComponentData,
        PackageComponentViewFactory> {
        protected override string pageTitle => ProductTitles.PackageComponents;
        protected override string pageUrl => ProductUrls.PackageComponents;
        protected override PackageComponent toObject(PackageComponentData d) => new(d);

        private class packageComponentsRepo :mockRepo<PackageComponent, PackageComponentData>, IPackageComponentsRepo { };
        private class productTypesRepo :mockRepo<IProductType, ProductTypeData>, IProductTypesRepo { };
        private packageComponentsRepo packageComponents;
        private productTypesRepo productTypes;
        private PackageComponentData packageComponentData;
        private ProductTypeData productTypeData;
        protected override PackageComponentsPage createObject() {
            packageComponents = new packageComponentsRepo();
            productTypes = new productTypesRepo();
            packageComponentData = GetRandom.ObjectOf<PackageComponentData>();
            productTypeData = GetRandom.ObjectOf<ProductTypeData>();
            addRandomPackageComponents();
            addRandomProductTypes();
            addRandomPackageTypes();
            return new PackageComponentsPage(packageComponents);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(packageComponents, productTypes);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void addRandomPackageComponents() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? packageComponentData : GetRandom.ObjectOf<PackageComponentData>();
                packageComponents.Add(new PackageComponent(d));
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
        public void ProductTypesTest() {
            var list = productTypes.Get();
            areEqual(list.Count + 1, obj.ProductTypes.Count());
        }

        [TestMethod]
        public void PackageTypesTest() {
            var list = productTypes.Get().Where(x => x.ProductKind == ProductKind.Package).ToList();
            areEqual(list.Count + 1, obj.PackageTypes.Count());
        }

        [TestMethod]
        public void ProductTypeNameTest() {
            var name = obj.ProductTypeName(productTypeData.Id);
            areEqual(productTypeData.Name, name);
        }
    }
}
