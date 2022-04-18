using Abc.Aids.Reflection;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class ProductInclusionKindTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(ProductInclusionKind);

        [TestMethod]
        public void CountTest()
            => Assert.AreEqual(4, GetEnum.Count<ProductInclusionKind>());

        [TestMethod]
        public void UnspecifiedTest() =>
            Assert.AreEqual(0, (int) ProductInclusionKind.Unspecified);

        [TestMethod]
        public void OrdinalTest() =>
            Assert.AreEqual(1, (int) ProductInclusionKind.Ordinal);

        [TestMethod]
        public void ConditionalTest() =>
            Assert.AreEqual(2, (int) ProductInclusionKind.Conditional);

        [TestMethod]
        public void DetailTest() =>
            Assert.AreEqual(3, (int) ProductInclusionKind.Detail);
    }

}