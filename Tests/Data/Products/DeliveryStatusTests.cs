using Abc.Aids.Reflection;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class DeliveryStatusTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(DeliveryStatus);

        [TestMethod]
        public void CountTest()
            => Assert.AreEqual(5, GetEnum.Count<DeliveryStatus>());

        [TestMethod]
        public void UnspecifiedTest() =>
            Assert.AreEqual(0, (int) DeliveryStatus.Unspecified);

        [TestMethod]
        public void ScheduledTest() =>
            Assert.AreEqual(1, (int) DeliveryStatus.Scheduled);

        [TestMethod]
        public void ExecutingTest() =>
            Assert.AreEqual(2, (int) DeliveryStatus.Executing);

        [TestMethod]
        public void CompletedTest() =>
            Assert.AreEqual(3, (int) DeliveryStatus.Completed);

        [TestMethod]
        public void CancelledTest() =>
            Assert.AreEqual(9, (int) DeliveryStatus.Cancelled);
    }

}