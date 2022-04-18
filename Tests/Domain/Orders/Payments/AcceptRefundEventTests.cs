using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Payments;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Payments {

    [TestClass]
    public class AcceptRefundEventTests :SealedTests<AcceptRefundEvent, PaymentEvent> {

        protected override AcceptRefundEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.AcceptRefundEvent;
            return OrderEventFactory.Create(d) as AcceptRefundEvent;
        }
    }
}