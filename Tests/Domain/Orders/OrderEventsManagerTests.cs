using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders {
    [TestClass]
    public class OrderEventsManagerTests :SealedTests<OrderEventsManager, OrderManager> {
        private IOrder order;

        [TestInitialize]
        public override void TestInitialize() {
            order = OrderFactory.Create(random<OrderData>());
            base.TestInitialize();
        }
        protected override OrderEventsManager createObject() => new(order);

        [TestMethod]
        public async Task AuditTrailTest()
            => await testListAsync<IOrderEvent, OrderEventData, IOrderEventsRepo>(
                d => d.OrderId = obj.order.Id,
                d => OrderEventFactory.Create(d));

        [TestMethod]
        public async Task RegisterEventTest() {
            await AuditTrailTest();
            var count = obj.AuditTrail.Count;
            var d = random<OrderEventData>();
            var expected = random<bool>();
            var actual = obj.RegisterEvent(OrderEventFactory.Create(d), expected);
            areEqual(expected, actual);
            areEqual(count + 1, obj.AuditTrail.Count);
        }
        [TestMethod]
        public void ComposeDataTest() {
            var d = random<OrderEventData>();
            var expected = random<bool>();
            var actual = obj.composeData(OrderEventFactory.Create(d), expected);
            allPropertiesAreEqual(d, actual,
                nameof(d.OrderId), nameof(d.IsProcessed), nameof(d.ValidFrom));
            areEqual(expected, actual.IsProcessed);
            areEqual(order.Id, actual.OrderId);
            isTrue(DateTime.Now > actual.ValidFrom);
            isTrue(DateTime.Now.AddSeconds(-1) < actual.ValidFrom);
        }
    }
}