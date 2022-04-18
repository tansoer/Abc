using System;
using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Products {

    [TestClass]
    public class ProductTypeFactoryTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(ProductTypeFactory);

        [DataTestMethod]
        [DataRow(ProductKind.Package, typeof(PackageType))]
        [DataRow(ProductKind.Container, typeof(ContainerType))]
        [DataRow(ProductKind.Product, typeof(ProductType))]
        [DataRow(ProductKind.Service, typeof(ServiceType))]
        [DataRow(ProductKind.MeasuredProduct, typeof(MeasuredProductType))]
        [DataRow(ProductKind.IdenticalProduct, typeof(IdenticalProductType))]
        [DataRow(ProductKind.UniqueProduct, typeof(UniqueProductType))]
        [DataRow(ProductKind.Unspecified, typeof(ProductTypeError))]
        public void CreateTest(ProductKind kind, Type t) {
            var d = GetRandom.ObjectOf<ProductTypeData>();
            d.ProductKind = kind;
            var o = ProductTypeFactory.Create(d);
            Assert.IsInstanceOfType(o, t);
            allPropertiesAreEqual(d, o.Data);
        }

    }

}