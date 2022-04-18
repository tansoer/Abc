using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Statuses;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Statuses {

    [TestClass]
    public class LifecycleEventTests :AbstractTests<LifecycleEvent, OrderEvent> {
        private class testClass :LifecycleEvent {
            public testClass() : this(null) { }
            public testClass(OrderEventData d) : base(d) { }
        }
        protected override LifecycleEvent createObject() => new testClass(random<OrderEventData>());
        internal static T createEvent<T>(OrderEventType t) where T : OrderEvent {
            var d = random<OrderEventData>();
            d.OrderEventType = t;
            return OrderEventFactory.Create(d) as T;
        }
    }
}