using Abc.Aids.Reflection;
using Abc.Data.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Quantities {

    [TestClass]
    public class UnitTypeTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(UnitType);

        [TestMethod]
        public void CountTest()
            => Assert.AreEqual(5, GetEnum.Count<UnitType>());

        [TestMethod]
        public void UnspecifiedTest() =>
            Assert.AreEqual(0, (int) UnitType.Unspecified);

        [TestMethod]
        public void FactoredTest() =>
            Assert.AreEqual(1, (int) UnitType.Factored);

        [TestMethod]
        public void DerivedTest() =>
            Assert.AreEqual(2, (int) UnitType.Derived);

        [TestMethod]
        public void FunctionedTest() =>
            Assert.AreEqual(3, (int) UnitType.Functioned);

        [TestMethod]
        public void ErrorTest() =>
            Assert.AreEqual(9, (int) UnitType.Error);

    }

}