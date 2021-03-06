using Abc.Aids.Reflection;
using Abc.Core.Rounding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core.Rounding {

    [TestClass]
    public class RoundingStrategyTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(RoundingStrategy);

        [TestMethod] public void CountTest() => Assert.AreEqual(7, GetEnum.Count<RoundingStrategy>());

        [TestMethod] public void RoundUpTest() => Assert.AreEqual(0, (int) RoundingStrategy.RoundUp);

        [TestMethod] public void RoundDownTest() => Assert.AreEqual(1, (int) RoundingStrategy.RoundDown);

        [TestMethod]
        public void RoundTest()
            => Assert.AreEqual(2, (int) RoundingStrategy.Round);

        [TestMethod]
        public void RoundUpByStepTest()
            => Assert.AreEqual(3, (int) RoundingStrategy.RoundUpByStep);

        [TestMethod]
        public void RoundDownByStepTest()
            => Assert.AreEqual(4, (int) RoundingStrategy.RoundDownByStep);

        [TestMethod]
        public void RoundTowardsPositiveTest()
            => Assert.AreEqual(5, (int) RoundingStrategy.RoundTowardsPositive);

        [TestMethod]
        public void RoundTowardsNegativeTest()
            => Assert.AreEqual(6, (int) RoundingStrategy.RoundTowardsNegative);

    }

}