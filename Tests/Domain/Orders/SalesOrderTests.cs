using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Facade.Orders;
using Abc.Tests.Domain.Orders.Delivery;
using Abc.Tests.Domain.Orders.Lines;
using Abc.Tests.Domain.Orders.Payments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders {

    [TestClass] public class SalesOrderTests :SealedTests<SalesOrder, Order> {
        private MockLinesManager mockLines;
        private MockPaymentsManager mockPayments;
        private MockDeliveryManager mockDelivery;

        protected override SalesOrder createObject() {
            var d = random<OrderData>();
            d.OrderType = OrderType.SalesOrder;
            return OrderFactory.Create(d) as SalesOrder;
        }
        [TestInitialize]
        public override void TestInitialize() {
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
        [TestMethod] public void DatePurchaseOrderReceivedTest() => isReadOnly(obj.Data?.DateSentReceived);
        [TestMethod] public void PurchaseOrderIdTest() => isReadOnly(obj.Data?.PurchaseOrderId);
        [TestMethod] public async Task PurchaseOrderTest() {
            var d = random<OrderData>();
            d.Id = obj.PurchaseOrderId;
            d.OrderType = OrderType.PurchaseOrder;
            await testItemAsync<OrderData, IOrder, IOrdersRepo>(
                d, () => obj.PurchaseOrder.Data, OrderFactory.Create);
        }
        [TestMethod] public void ProcessReceiptTest() 
            => mockDelivery.ValidateMethods(acceptEvent(OrderEventType.ReceiptEvent), 
                nameof(mockDelivery.ReceiptReturned));
        [TestMethod] public void ProcessDispatchTest() 
            => mockDelivery.ValidateMethods(acceptEvent(OrderEventType.DispatchEvent), 
                nameof(mockDelivery.DispatchItems));
        [TestMethod] public void AcceptPaymentTest() 
            => mockPayments.ValidateMethods(acceptEvent(OrderEventType.AcceptPaymentEvent), 
                nameof(mockPayments.AcceptPayment));
        [TestMethod] public void MakeRefundTest() 
            => mockPayments.ValidateMethods(acceptEvent(OrderEventType.MakeRefundEvent), 
                nameof(mockPayments.MakeRefund));
        [TestMethod] public void ProcessInvoiceTest()  
            => mockPayments.ValidateMethods(acceptEvent(OrderEventType.InvoiceEvent), 
                nameof(mockPayments.SendInvoice));
        private IOrderEvent acceptEvent(OrderEventType? t) {
            var d = random<OrderEventData>();
            d.OrderEventType = t ?? OrderEventType.Unspecified;
            var e = (t is null) ? null : OrderEventFactory.Create(d);
            obj.AcceptEvent(e);
            return e;
        }
    }
}