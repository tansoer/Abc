using Abc.Aids.Reflection;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class ReservationStatusTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(ReservationStatus);

        [TestMethod]
        public void CountTest()
            => Assert.AreEqual(5, GetEnum.Count<ReservationStatus>());

        [TestMethod]
        public void UnspecifiedTest() =>
            Assert.AreEqual(0, (int) ReservationStatus.Unspecified);

        [TestMethod]
        public void AvailableTest() =>
            Assert.AreEqual(1, (int) ReservationStatus.Available);

        [TestMethod]
        public void ReservedTest() =>
            Assert.AreEqual(2, (int) ReservationStatus.Reserved);

        [TestMethod]
        public void SoldTest() =>
            Assert.AreEqual(3, (int) ReservationStatus.Sold);

        [TestMethod]
        public void CancelledTest() =>
            Assert.AreEqual(9, (int) ReservationStatus.Cancelled);

    }

}