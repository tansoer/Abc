using Abc.Facade.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Inventory {
    [TestClass]
    public class ReservationViewTests :SealedTests<ReservationView, InventoryItemView> {
        [TestMethod] public void ReservationRequestIdTest()
            => isNullableProperty<string>("Reservation Request");
        [TestMethod] public void CancellationPolicyRuleSetIdTest() 
            => isNullableProperty<string>("Cancellation Policy Rule Set");
    }
}
