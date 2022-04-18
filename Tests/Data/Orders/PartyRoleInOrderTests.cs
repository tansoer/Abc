using Abc.Data.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders {
    [TestClass] public class PartyRoleInOrderTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(PartyRoleInOrder);
        [TestMethod] public void UnspecifiedTest() => areEqual(0, (int)PartyRoleInOrder.Unspecified);
        [TestMethod] public void VendorTest() => areEqual(1, (int)PartyRoleInOrder.Vendor);
        [TestMethod] public void SalesAgentTest() => areEqual(2, (int)PartyRoleInOrder.SalesAgent);
        [TestMethod] public void PaymentReceiverTest() => areEqual(3, (int)PartyRoleInOrder.PaymentReceiver);
        [TestMethod] public void OrderInitiatorTest() => areEqual(4, (int)PartyRoleInOrder.OrderInitiator);
        [TestMethod] public void PurchaserTest() => areEqual(5, (int)PartyRoleInOrder.Purchaser);
        [TestMethod] public void OrderReceiverTest() => areEqual(6, (int)PartyRoleInOrder.OrderReceiver);
        [TestMethod] public void OrderLineReceiverTest() => areEqual(7, (int)PartyRoleInOrder.OrderLineReceiver);
    }
}