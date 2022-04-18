using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Payments;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Payments {

    [TestClass]
    public class AcceptPaymentEventTests :SealedTests<AcceptPaymentEvent, PaymentEvent> {

        protected override AcceptPaymentEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.AcceptPaymentEvent;
            return OrderEventFactory.Create(d) as AcceptPaymentEvent;
        }
    }
}