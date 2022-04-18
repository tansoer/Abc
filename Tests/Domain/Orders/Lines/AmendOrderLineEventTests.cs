using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Lines {

    [TestClass]
    public class AmendOrderLineEventTests :AmendOrderEventTests<AmendOrderLineEvent, AmendEvent> {
        protected override AmendOrderLineEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.AmendOrderLineEvent;
            return OrderEventFactory.Create(d) as AmendOrderLineEvent;
        }
        [TestMethod] public void OrderLineIdTest() => isReadOnly(obj.Data.OrderLineId, true);
        [TestMethod] public void OldOrderLineIdTest() => isReadOnly(obj.Data.OldOrderLineId, true);
        [TestMethod]
        public async Task OrderLineTest() {
            var d = random<OrderLineData>();
            d.Id = obj.orderLineId;
            while (d.OrderLineType == OrderLineType.Unspecified)
                d.OrderLineType = random<OrderLineType>();
            await testItemAsync<OrderLineData, IOrderLine, IOrderLinesRepo>(
                d, () => obj.OrderLine.Data, OrderLineFactory.Create);

        }
        [TestMethod]
        public async Task OldOrderLineTest() {
            var d = random<OrderLineData>();
            d.Id = obj.oldOrderLineId;
            while (d.OrderLineType == OrderLineType.Unspecified)
                d.OrderLineType = random<OrderLineType>();
            await testItemAsync<OrderLineData, IOrderLine, IOrderLinesRepo>(
                d, () => obj.OldOrderLine.Data, OrderLineFactory.Create);
        }
        [TestMethod]
        public async Task ReturnedItemsTest()
            => await testListAsync<ReturnedItem, ReturnedItemData, IReturnedItemsRepo>(
                d => d.OrderEventId = obj.Id, d => new ReturnedItem(d));
        [TestMethod]
        public async Task IsNewEventTest() {
            isFalse(obj.IsNewEvent);
            await OrderLineTest();
            isTrue(obj.IsNewEvent);
            await OldOrderLineTest();
            isFalse(obj.IsNewEvent);
        }
        [TestMethod]
        public async Task IsRemoveEventTest() {
            isFalse(obj.IsRemoveEvent);
            await OldOrderLineTest();
            isTrue(obj.IsRemoveEvent);
            await OrderLineTest();
            isFalse(obj.IsRemoveEvent);
        }
        [TestMethod]
        public async Task IsCorrectTest() {
            isFalse(obj.IsCorrect(random<OrderLineType>()));
            await isCorrectTest(true, true, true, true);
            await isCorrectTest(false, false, false, false);
            await isCorrectTest(true, false, false, false);
            await isCorrectTest(false, true, false, false);
            await isCorrectTest(false, false, true, false);
            await isCorrectTest(true, true, false, false);
            await isCorrectTest(false, true, true, false);
            await isCorrectTest(true, false, true, false);
        }
        private async Task isCorrectTest(bool isSigned, bool isNotProcessed, bool isTypeOf, bool expected) {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.AmendOrderLineEvent;
            await setIsSigned(d, isSigned);
            setIsNotProcessed(d, isNotProcessed);
            var t = randomType(OrderLineType.Unspecified);
            await setIsCorrectTypeOf(d, isTypeOf, t);
            obj = OrderEventFactory.Create(d) as AmendOrderLineEvent;
            Assert.IsNotNull(obj);
            areEqual(expected, obj.IsCorrect(t));
        }
        private static async Task setIsCorrectTypeOf(OrderEventData d, bool isTypeOf, OrderLineType t) {
            var neededType = isTypeOf ? t : randomType(t);
            if (random<bool>()) await addItem(neededType, d.OldOrderLineId);
            else if (random<bool>()) await addItem(neededType, d.OrderLineId);
            else {
                await addItem(neededType, d.OldOrderLineId);
                await addItem(neededType, d.OrderLineId);
            }
        }
        [TestMethod]
        public async Task IsTypeOfTest() {
            var t = random<OrderLineType>();
            while (t == obj.OrderLine.Data.OrderLineType) t = random<OrderLineType>();
            isFalse(obj.IsTypeOf(t));
            await isTypeTest(() => obj.OrderLine, OrderLineTest);
            await isTypeTest(() => obj.OldOrderLine, OldOrderLineTest);
            await isBothOrderLineTypeTest();
        }
        private async Task isBothOrderLineTypeTest() {
            obj = createObject();
            var t = randomType(OrderLineType.Unspecified);
            var otherT = randomType(t);
            await addItem(t, obj.oldOrderLineId);
            await addItem(t, obj.orderLineId);
            isTrue(obj.IsTypeOf(t));
            isFalse(obj.IsTypeOf(otherT));
        }
        private static OrderLineType randomType(OrderLineType t) {
            var x = random<OrderLineType>();
            while (x == t) x = random<OrderLineType>();
            return x;
        }
        private static async Task addItem(OrderLineType t, string id) {
            var d = random<OrderLineData>();
            d.OrderLineType = t;
            d.Id = id;
            await addAsync<IOrderLinesRepo, IOrderLine>(OrderLineFactory.Create(d));
        }
        private async Task isTypeTest(Func<IOrderLine> l, Func<Task> f) {
            obj = createObject();
            await f();
            var t = l().Data.OrderLineType;
            var otherT = random<OrderLineType>();
            while (t == otherT) otherT = random<OrderLineType>();
            isTrue(obj.IsTypeOf(t));
            isFalse(obj.IsTypeOf(otherT));
        }
    }
}