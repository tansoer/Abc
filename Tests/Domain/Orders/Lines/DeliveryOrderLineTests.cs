using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Lines {

    [TestClass]
    public class DeliveryOrderLineTests :AbstractTests<DeliveryOrderLine, BaseOrderLine> {

        string reason;
        string orderId;
        List<string> productIDs;
        private class testClass :DeliveryOrderLine {
            public testClass() : this(null) { }
            public testClass(OrderLineData d) : base(d) { }
        }
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            reason = random<string>();
            orderId = random<string>();
            productIDs = new List<string>();
        }

        protected override DeliveryOrderLine createObject() {
            var d = GetRandom.ObjectOf<OrderLineData>();
            addProductLine(d.OrderLineId);
            return new testClass(d);
        }

        private void addProductLine(string lineId) {
            var r = GetRepo.Instance<IOrderLinesRepo>();
            var d = GetRandom.ObjectOf<OrderLineData>();
            d.OrderLineType = OrderLineType.ProductLine;
            d.Id = lineId;
            r.Add(new OrderLine(d));
        }

        [TestMethod] public void ProductLineTest() => validateProductLine(isReadOnly(obj, nameof(obj.ProductLine)));
        private static void validateProductLine(object o) {
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, typeof(OrderLine));
            Assert.IsFalse(((OrderLine)o).IsUnspecified);
        }

        [TestMethod] public async Task RejectedItemsTest()
            => await testListAsync<IOrderLineItem, OrderLineItemData, IOrderLineItemsRepo>(
                x => x.OrderLineId = obj.Id, x => new RejectedItem(x));

        [TestMethod] public void NumberRejectedTest() => isReadOnly(obj.RejectedItems.Count);
        [TestMethod] public void NumberOfProductsTest() => isReadOnly(obj.ProductLine?.NumberOfProducts ?? 0 - obj.NumberRejected);

        //TODO: Check if these bottom 3 tests actually test anything
        [TestMethod]
        public async Task RejectProductInstancesAllNullNotFailingTest() {
            await RejectedItemsTest();
            var count = obj.RejectedItems.Count;
            obj.RejectProductInstances(null, null, null);
            areEqual(count, obj.RejectedItems.Count);
        }
        [TestMethod]
        public async Task RejectProductInstancesNotThisLineTest() {
            await RejectedItemsTest();
            var count = obj.RejectedItems.Count;
            for (int i = 0; i < count; i++) {
                if (random<bool>()) continue;
                productIDs.Add(obj.RejectedItems[i].Id);
            }
            obj.RejectProductInstances(reason, orderId, productIDs.ToArray());
            areEqual(count, obj.RejectedItems.Count);
        }
        [TestMethod]
        public async Task RejectProductInstancesTest() {
            await RejectedItemsTest();
            var count = obj.RejectedItems.Count;
            for (int i = 0; i < count; i++) {
                if (random<bool>()) continue;
                productIDs.Add(obj.RejectedItems[i].Id);
            }

            obj.RejectProductInstances(reason, orderId, productIDs.ToArray());
            areEqual(count, obj.RejectedItems.Count);
        }
    }
}