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
    public class ReceiptEventTests :SealedTests<ReceiptEvent, DeliveryEvent> {

        protected override ReceiptEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.ReceiptEvent;
            return OrderEventFactory.Create(d) as ReceiptEvent;
        }
        [TestMethod]
        public void ReceiptLinesTest() {
            var count = GetRandom.UInt8(10, 30);
            var r = GetRepo.Instance<IOrderLinesRepo>();
            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<OrderLineData>();
                if (i % 2 == 0) {
                    d.OrderId = obj.OrderId;
                    d.OrderEventId = obj.Id;
                    if (d.OrderLineType == OrderLineType.ReceiptLine)
                        d.OrderLineType = OrderLineType.Unspecified;
                }
                if (i % 4 == 0) {
                    d.OrderLineType = OrderLineType.ReceiptLine;
                }
                r.Add(OrderLineFactory.Create(d));
            }
            var ol = obj.OrderLines;
            var dl = obj.ReceiptLines;
            areEqual(Math.Ceiling(count / 2.0), (double)ol.Count);
            areEqual(Math.Ceiling(count / 4.0), (double)dl.Count);
        }
    }
}