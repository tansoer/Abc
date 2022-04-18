using Abc.Aids.Reflection;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class PricingStrategyTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(PricingStrategy);

        [TestMethod]
        public void CountTest()
            => Assert.AreEqual(3, GetEnum.Count<PricingStrategy>());

        [TestMethod]
        public void AssignedTest() =>
            Assert.AreEqual(1, (int) PricingStrategy.Assigned);

        [TestMethod]
        public void AggregatedTest() =>
            Assert.AreEqual(2, (int) PricingStrategy.Aggregated);

        [TestMethod]
        public void UnspecifiedTest() =>
            Assert.AreEqual(0, (int) PricingStrategy.Unspecified);
    }

}