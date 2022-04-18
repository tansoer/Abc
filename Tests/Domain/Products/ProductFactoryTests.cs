using System;
using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Products {
    [TestClass]
    public class ProductFactoryTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(ProductFactory);

        [DataTestMethod]
        [DataRow(ProductKind.Package, typeof(Package))]
        [DataRow(ProductKind.Container, typeof(Container))]
        [DataRow(ProductKind.Product, typeof(Product))]
        [DataRow(ProductKind.Service, typeof(Service))]
        [DataRow(ProductKind.MeasuredProduct, typeof(MeasuredProduct))]
        [DataRow(ProductKind.IdenticalProduct, typeof(IdenticalProduct))]
        [DataRow(ProductKind.UniqueProduct, typeof(UniqueProduct))]
        [DataRow(ProductKind.Unspecified, typeof(ProductError))]
        public void CreateTest(ProductKind kind, Type t) {
            var d = GetRandom.ObjectOf<ProductData>();
            d.ProductKind = kind;
            var o = ProductFactory.Create(d);
            Assert.IsInstanceOfType(o, t);
            allPropertiesAreEqual(d, o.Data);

        }

    }

}