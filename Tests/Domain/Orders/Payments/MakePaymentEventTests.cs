using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Payments;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Payments {

    [TestClass]
    public class MakePaymentEventTests :SealedTests<MakePaymentEvent, PaymentEvent> {

        protected override MakePaymentEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.MakePaymentEvent;
            return OrderEventFactory.Create(d) as MakePaymentEvent;
        }
    }
}