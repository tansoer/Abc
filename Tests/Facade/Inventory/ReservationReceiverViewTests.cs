using Abc.Facade.Common;
using Abc.Facade.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Inventory {
    [TestClass]
    public class ReservationReceiverViewTests :SealedTests<ReservationReceiverView, EntityBaseView> {
        [TestMethod]
        public void ReceiverPartySummaryIdTest()
            => isNullableProperty<string>("Receiver Party Summary");
        [TestMethod]
        public void ReservationRequestIdTest()
            => isNullableProperty<string>("Reservation Request");
    }
}
