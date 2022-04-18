using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Delivery {
    [TestClass]
    public class DeliveryEventTests :AbstractTests<DeliveryEvent, OrderEvent> {

        private class testClass :DeliveryEvent {
            public List<IDeliveryOrderLine> TestLines { get; } = new List<IDeliveryOrderLine>();
            public bool UseTestLines { get; set; } = false;
            public testClass() : this(null) { }
            public testClass(OrderEventData d) : base(d) { }
            public override IReadOnlyList<IDeliveryOrderLine> DeliveryLines => UseTestLines
                ? TestLines.AsReadOnly()
                : base.DeliveryLines;
        }
        private class testLine :DeliveryOrderLine {
            public static int Count;
            public testLine() : this(null) { }
            public testLine(OrderLineData d) : base(d) { }
            public override void RejectProductInstances(string reason, string orderLineId, params string[] productIDs)
                => Count++;
        }
        protected override DeliveryEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType =
                random<bool>() ? OrderEventType.DispatchEvent : OrderEventType.ReceiptEvent;
            return new testClass(d);
        }
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            obj = createObject();
        }
        [TestMethod] public void DeliveryIdTest() => isReadOnly(obj.Data?.ProductDeliveryId);
        [TestMethod]
        public async Task DeliveryTest() =>
            await testItemAsync<ProductDeliveryData, ProductDelivery, IProductDeliveriesRepo>(
               obj.DeliveryId, () => obj.Delivery.Data, d => new ProductDelivery(d));
        [TestMethod]
        public void DeliveryLinesTest() {
            var count = GetRandom.UInt8(10, 30);
            var r = GetRepo.Instance<IOrderLinesRepo>();
            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<OrderLineData>();
                if (i % 2 == 0) {
                    d.OrderId = obj.OrderId;
                    d.OrderEventId = obj.Id;
                    if (d.OrderLineType is OrderLineType.ReceiptLine or OrderLineType.DispatchLine)
                        d.OrderLineType = OrderLineType.Unspecified;
                }
                if (i % 4 == 0) {
                    d.OrderLineType =
                        random<bool>()
                        ? OrderLineType.ReceiptLine
                        : OrderLineType.DispatchLine;
                }
                r.Add(OrderLineFactory.Create(d));
            }
            var ol = obj.OrderLines;
            var dl = obj.DeliveryLines;
            areEqual(Math.Ceiling(count / 2.0), (double)ol.Count);
            areEqual(Math.Ceiling(count / 4.0), (double)dl.Count);
        }
        [TestMethod]
        public void RejectProductInstancesTest() {
            var count = random<byte>();
            ((testClass)obj).UseTestLines = true;
            for (var i = 0; i < count; i++) {
                var d = new OrderLineData {
                    OrderEventId = obj.Id,
                    OrderId = obj.OrderId,
                    OrderLineType =
                        random<bool>()
                        ? OrderLineType.ReceiptLine
                        : OrderLineType.DispatchLine
                };
                ((testClass)obj).TestLines.Add(new testLine(d));
            }
            testLine.Count = 0;
            obj.RejectProductInstances(null, null, null);
            areEqual(count, (byte)testLine.Count);
        }
    }
}
