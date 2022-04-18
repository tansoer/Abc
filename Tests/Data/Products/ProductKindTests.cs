using Abc.Aids.Random;
using Abc.Aids.Reflection;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {
    [TestClass] public class ProductKindTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(ProductKind);
        [TestMethod] public void CountTest() => Assert.AreEqual(8, GetEnum.Count<ProductKind>());
        [TestMethod] public void UnspecifiedTest() => Assert.AreEqual(0, (int)ProductKind.Unspecified);
        [TestMethod] public void ProductTest() => Assert.AreEqual(1, (int)ProductKind.Product);
        [TestMethod] public void MeasuredProductTest() => Assert.AreEqual(2, (int)ProductKind.MeasuredProduct);
        [TestMethod] public void UniqueProductTest() => Assert.AreEqual(3, (int)ProductKind.UniqueProduct);
        [TestMethod] public void IdenticalProductTest() => Assert.AreEqual(4, (int)ProductKind.IdenticalProduct);
        [TestMethod] public void ServiceTest() => Assert.AreEqual(5, (int)ProductKind.Service);
        [TestMethod] public void ContainerTest() => Assert.AreEqual(6, (int)ProductKind.Container);
        [TestMethod] public void PackageTest() => Assert.AreEqual(9, (int)ProductKind.Package);
        [TestMethod] public void GetRandomTest() {
            for (int i = 0; i < 100; i++) {
                var t = GetRandom.EnumOf<ProductKind>();
                Assert.IsInstanceOfType(t, typeof(ProductKind));
            }
        }
        [TestMethod] public void GetRandomByTypeTest() {
            for (int i = 0; i < 100; i++) {
                var t = GetRandom.EnumOf(typeof(ProductKind));
                Assert.IsInstanceOfType(t, typeof(ProductKind));
            }
        }
    }
}