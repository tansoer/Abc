using Abc.Data.Currencies;
using Abc.Data.Orders;
using Abc.Domain.Currencies;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Orders.Payments;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Payments {
    [TestClass]
    public class PaymentEventTests :AbstractTests<PaymentEvent, OrderEvent> {
        private class testClass :PaymentEvent {
            public testClass(OrderEventData d) : base(d) { }
        }

        protected override PaymentEvent createObject() => new testClass(random<OrderEventData>());
        [TestMethod] public void OrderLineIdTest() => isReadOnly(obj.Data.OrderLineId, true);
        [TestMethod] public void PaymentIdTest() => isReadOnly(obj.Data.PaymentId, true);
        [TestMethod] public async Task PaymentTest() => 
            await testItemAsync<PaymentData, BasePayment, IPaymentsRepo>(
                obj.paymentId, () => obj.Payment.Data, d => new OrderPayment(d));
        [TestMethod] public async Task OrderLineTest() => 
            await testItemAsync<OrderLineData, IOrderLine, IOrderLinesRepo>(
                obj.orderLineId, () => obj.OrderLine.Data, OrderLineFactory.Create);
    }
}