using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Domain.Orders.Delivery {
    [TestClass]
    public partial class DispatchEventTests :SealedTests<DispatchEvent, DeliveryEvent> {

        protected override DispatchEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.DispatchEvent;
            return OrderEventFactory.Create(d) as DispatchEvent;
        }
        [TestMethod]
        public void DispatchLinesTest() {
            var count = GetRandom.UInt8(10, 30);
            var r = GetRepo.Instance<IOrderLinesRepo>();
            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<OrderLineData>();
                if (i % 2 == 0) {
                    d.OrderId = obj.OrderId;
                    d.OrderEventId = obj.Id;
                    if (d.OrderLineType == OrderLineType.DispatchLine)
                        d.OrderLineType = OrderLineType.Unspecified;
                }
                if (i % 4 == 0) {
                    d.OrderLineType = OrderLineType.DispatchLine;
                }
                r.Add(OrderLineFactory.Create(d));
            }
            var ol = obj.OrderLines;
            var dl = obj.DispatchLines;
            areEqual(Math.Ceiling(count / 2.0), (double)ol.Count);
            areEqual(Math.Ceiling(count / 4.0), (double)dl.Count);
        }

    }
}