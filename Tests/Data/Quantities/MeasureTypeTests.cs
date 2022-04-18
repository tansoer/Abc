using Abc.Aids.Reflection;
using Abc.Data.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Quantities {
    [TestClass]
    public class MeasureTypeTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(MeasureType);

        [TestMethod]
        public void CountTest()
            => Assert.AreEqual(4, GetEnum.Count<MeasureType>());

        [TestMethod]
        public void UnspecifiedTest() =>
            Assert.AreEqual(0, (int) MeasureType.Unspecified);

        [TestMethod]
        public void BaseTest() =>
            Assert.AreEqual(1, (int) MeasureType.Base);

        [TestMethod]
        public void DerivedTest() =>
            Assert.AreEqual(2, (int) MeasureType.Derived);


        [TestMethod]
        public void ErrorTest() =>
            Assert.AreEqual(9, (int) MeasureType.Error);

    }

}