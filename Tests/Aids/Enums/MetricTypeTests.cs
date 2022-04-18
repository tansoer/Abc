using Abc.Aids.Enums;
using Abc.Aids.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Enums {
    [TestClass]
    public class MetricTypeTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(MetricType);

        [TestMethod]
        public void CountTest()
            => Assert.AreEqual(3, GetEnum.Count<MetricType>());

        [TestMethod]
        public void UnspecifiedTest() =>
            Assert.AreEqual(0, (int)MetricType.Unspecified);

        [TestMethod]
        public void StringTest() =>
            Assert.AreEqual(1, (int)MetricType.String);

        [TestMethod]
        public void QuantityTest() =>
            Assert.AreEqual(2, (int)MetricType.Quantity);
    }
}
