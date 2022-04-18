using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Facade.Orders;
using Abc.Tests.Domain.Orders.Delivery;
using Abc.Tests.Domain.Orders.Lines;
using Abc.Tests.Domain.Orders.Payments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Orders {
    [TestClass] public class PurchaseOrderTests :SealedTests<PurchaseOrder, Order> {
        private MockLinesManager mockLines;
        private MockPaymentsManager mockPayments;
        private MockDeliveryManager mockDelivery;

        protected override PurchaseOrder createObject() {
            var d = random<OrderData>();
            d.OrderType = OrderType.PurchaseOrder;
            return OrderFactory.Create(d) as PurchaseOrder;
        }
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            mockLines = new MockLinesManager(obj);
            mockPayments = new MockPaymentsManager(obj);
            mockDelivery = new MockDeliveryManager(obj);
            obj.linesManager = mockLines;
            obj.paymentsManager = mockPayments;
            obj.deliveryManager = mockDelivery;
        }
        [TestMethod] public void IsClosableTest() {
            obj.IsClosable();
            mockLines.ValidateMethods(nameof(mockLines.IsClosable));
        }
        [TestMethod] public void IsCancellableTest() {
            obj.IsCancellable();
            mockLines.ValidateMethods(nameof(mockLines.IsCancellable));
        }
        [TestMethod] public void DateSentTest() => isReadOnly(obj.Data?.DateSentReceived);
        [TestMethod] public void AcceptRefundTest() {
            var e = acceptEvent(OrderEventType.AcceptRefundEvent);
            mockPayments.ValidateMethods(e, nameof(mockPayments.AcceptRefund));
        }
        [TestMethod] public void MakePaymentTest() {
            var e = acceptEvent(OrderEventType.MakePaymentEvent);
            mockPayments.ValidateMethods(e, nameof(mockPayments.MakePayment));
        }
        [TestMethod] public void ProcessInvoiceTest() {
            var e = acceptEvent(OrderEventType.InvoiceEvent);
            mockPayments.ValidateMethods(e, nameof(mockPayments.ReceiveInvoice));
        }
        [TestMethod] public void ProcessReceiptTest() {
            var e = acceptEvent(OrderEventType.ReceiptEvent);
            mockDelivery.ValidateMethods(e, nameof(mockDelivery.AcceptReceipt));
        }
        [TestMethod] public void ProcessDispatchTest() {
            var e = acceptEvent(OrderEventType.DispatchEvent);
            mockDelivery.ValidateMethods(e, nameof(mockDelivery.ReturnItems));
        }
        private IOrderEvent acceptEvent(OrderEventType? t) {
            var d = random<OrderEventData>();
            d.OrderEventType = t ?? OrderEventType.Unspecified;
            var e = (t is null) ? null : OrderEventFactory.Create(d);
            obj.AcceptEvent(e);
            return e;
        }
    }
}