using Abc.Domain.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Abc.Domain.Orders.Payments;
using Abc.Data.Orders;
using System.Threading.Tasks;
using Abc.Domain.Currencies;
using Abc.Data.Currencies;
using System.Linq;
using System.Collections.Generic;

namespace Abc.Tests.Domain.Orders.Payments {
    public sealed class MockPaymentsManager :MockOrderManager, IOrderPaymentsManager {

        public MockPaymentsManager(IOrder o) : base(o) { }
        public IReadOnlyList<OrderPayment> Payments => registerMethod<IReadOnlyList<OrderPayment>>();
        public IReadOnlyList<Invoice> Invoices => registerMethod<IReadOnlyList<Invoice>>();
        public bool AcceptPayment(AcceptPaymentEvent e) => registerMethod<bool>(e);
        public bool AcceptRefund(AcceptRefundEvent e) => registerMethod<bool>(e);
        public bool MakePayment(MakePaymentEvent e) => registerMethod<bool>(e);
        public bool MakeRefund(MakeRefundEvent e) => registerMethod<bool>(e);
        public bool ReceiveInvoice(InvoiceEvent e) => registerMethod<bool>(e);
        public bool SendInvoice(InvoiceEvent e) => registerMethod<bool>(e);
    }
    [TestClass] public class OrderPaymentsManagerTests :SealedTests<OrderPaymentsManager, OrderManager> {
        private IOrder order;

        [TestInitialize] public override void TestInitialize() {
            order = OrderFactory.Create(random<OrderData>());
            base.TestInitialize();
        }
        protected override OrderPaymentsManager createObject() => new(order);

        [TestMethod] public async Task PaymentsTest()
            => await testListAsync<BasePayment, PaymentData, IPaymentsRepo>(
                d => d.OrderId = obj.order.Id,
                d => new OrderPayment(d),
                true);
        [TestMethod] public async Task InvoicesTest()
            => await testListAsync<Invoice, InvoiceData, IInvoicesRepo>(
                d => d.OrderId = obj.order.Id,
                d => new Invoice(d));
        [TestMethod] public async Task OrderPaymentsTest() {
            await PaymentsTest();
            var payments = obj.payments;
            var orderPayments = obj.Payments;
            areEqual(orderPayments.Count, payments.OfType<OrderPayment>().Count());
            var found = false;
            foreach (var l in payments) {
                if (l is not OrderPayment x) continue;
                found = true;
                var y = orderPayments.FirstOrDefault(z => z.Id == x.Id) ?? new OrderPayment();
                allPropertiesAreEqual(x.Data, y.Data);
            }
            isTrue(found);
        }
        [TestMethod] public void AcceptPaymentTest() => notTested();
        [TestMethod] public void MakeRefundTest() => notTested();
        [TestMethod] public void SendInvoiceTest() => notTested();
        [TestMethod] public void AcceptRefundTest() => notTested();
        [TestMethod] public void MakePaymentTest() => notTested();
        [TestMethod] public void ReceiveInvoiceTest() => notTested();
    }
}
