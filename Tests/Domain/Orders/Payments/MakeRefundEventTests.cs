using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Payments;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Payments {

    [TestClass]
    public class MakeRefundEventTests :SealedTests<MakeRefundEvent, PaymentEvent> {

        protected override MakeRefundEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.MakeRefundEvent;
            return OrderEventFactory.Create(d) as MakeRefundEvent;
        }
    }
}