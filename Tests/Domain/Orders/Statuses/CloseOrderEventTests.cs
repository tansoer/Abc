using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Statuses;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Statuses {

    [TestClass]
    public class CloseOrderEventTests :SealedTests<CloseOrderEvent, LifecycleEvent> {

        protected override CloseOrderEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.CloseEvent;
            return OrderEventFactory.Create(d) as CloseOrderEvent;
        }
    }
}