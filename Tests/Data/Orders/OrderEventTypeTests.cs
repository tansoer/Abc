using Abc.Data.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders
{
    [TestClass]
    public class OrderEventTypeTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(OrderEventType);

        [TestMethod] public void UnspecifiedTest() => Assert.AreEqual(0, (int) OrderEventType.Unspecified);
        [TestMethod] public void OpenEventTest() => Assert.AreEqual(1, (int) OrderEventType.OpenEvent);
        [TestMethod] public void CloseEventTest() => Assert.AreEqual(2, (int) OrderEventType.CloseEvent);
        [TestMethod] public void CancelEventTest() => Assert.AreEqual(3, (int) OrderEventType.CancelEvent);
        [TestMethod] public void AmendOrderLineEventTest() => Assert.AreEqual(4, (int) OrderEventType.AmendOrderLineEvent);
        [TestMethod] public void AmendTermsAndConditionsEventTest() => Assert.AreEqual(5, (int) OrderEventType.AmendTermsAndConditionsEvent);
        [TestMethod] public void AmendPartySummaryEventTest() => Assert.AreEqual(6, (int) OrderEventType.AmendPartySummaryEvent);
        [TestMethod] public void DiscountEventTest() => Assert.AreEqual(7, (int) OrderEventType.DiscountEvent);
        [TestMethod] public void DispatchEventTest() => Assert.AreEqual(8, (int) OrderEventType.DispatchEvent);
        [TestMethod] public void ReceiptEventTest() => Assert.AreEqual(9, (int) OrderEventType.ReceiptEvent);
        [TestMethod] public void InvoiceEventTest() => Assert.AreEqual(10, (int) OrderEventType.InvoiceEvent);
        [TestMethod] public void MakePaymentEventTest() => Assert.AreEqual(11, (int) OrderEventType.MakePaymentEvent);
        [TestMethod] public void AcceptPaymentEventTest() => Assert.AreEqual(12, (int) OrderEventType.AcceptPaymentEvent);
        [TestMethod] public void MakeRefundEventTest() => Assert.AreEqual(13, (int) OrderEventType.MakeRefundEvent);
        [TestMethod] public void AcceptRefundEventTest() => Assert.AreEqual(14, (int) OrderEventType.AcceptRefundEvent);
    }
}