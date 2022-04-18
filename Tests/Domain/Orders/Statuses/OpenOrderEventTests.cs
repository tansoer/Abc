using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Statuses;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Statuses {

    [TestClass]
    public class OpenOrderEventTests :SealedTests<OpenOrderEvent, LifecycleEvent> {

        protected override OpenOrderEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.OpenEvent;
            return OrderEventFactory.Create(d) as OpenOrderEvent;
        }
    }
}