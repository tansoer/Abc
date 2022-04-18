using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders {

    [TestClass]
    public class AmendEventTests :AbstractTests<AmendEvent, OrderEvent> {
        private class testClass :AmendEvent {
            public testClass() : this(null) { }
            public testClass(OrderEventData d) : base(d) { }
            public override bool IsNewEvent => true;
            public override bool IsRemoveEvent => true;
        }
        protected override AmendEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.AmendOrderLineEvent;
            return OrderEventFactory.Create(d) as AmendEvent;
        }

        [TestMethod] public void IsNewEventTest() => isAbstractReadOnly();
        [TestMethod] public void IsRemoveEventTest() => isAbstractReadOnly();
    }
}