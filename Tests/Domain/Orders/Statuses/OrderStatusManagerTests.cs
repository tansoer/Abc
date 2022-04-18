using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Statuses;
using Abc.Tests.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Statuses {
    [TestClass] public class OrderStatusManagerTests :SealedTests<OrderStatusManager, OrderManager> {
        private IOrder order;
        private ILifecycleEvent testEvent;
        private MockStatus mockStatus;
        private class MockStatus :EntityTests.MockEntity<OrderStatusData>, IOrderStatus {
            public string OrderId => Mock.Func<string>();
            public string OrderEventId => Mock.Func<string>();
            public OrderLifecycleStatus Status => Mock.Func<OrderLifecycleStatus>();
            public event EventHandler<IOrderStatus> OnStatusChanged;
            public bool ProcessOrderEvent(ILifecycleEvent e) => Mock.Func<bool>(e);
            public void doOnStatusChanged(object sender, IOrderStatus s)
               => OnStatusChanged?.Invoke(sender, s);
            public bool IsInitialized() => Mock.Func<bool>();
        }
        [TestInitialize] public override void TestInitialize() {
            order = OrderFactory.Create(random<OrderData>());
            base.TestInitialize();
            testEvent = LifecycleEventTests.createEvent<LifecycleEvent>(
                (OrderEventType)GetRandom.UInt8(1, 3));
            mockStatus = new MockStatus();
        }
        protected override OrderStatusManager createObject() => new(order);
        [TestMethod] public async Task AuditTrailTest()
            => await testListAsync<IOrderStatus, OrderStatusData, IOrderStatusesRepo>(
                d => { 
                    d.OrderId = obj.order.Id;
                    d.ValidFrom = DateTime.Now.AddDays(GetRandom.Int8(max: -5)); },
                d => OrderStatusFactory.Create(d));
        [TestMethod] public async Task CurrentStatusTest() { 
            await AuditTrailTest();
            var actual = obj.CurrentStatus;
            foreach (var item in obj.AuditTrail) 
               isTrue(item.ValidFrom <= actual.ValidFrom);
        }
        [TestMethod] public void ProcessEventTest() {
            obj.currentStatus = mockStatus;
            obj.ProcessEvent(testEvent);
            isTrue(mockStatus.Mock.CalledMethods.Contains(
                nameof(mockStatus.ProcessOrderEvent)));
            obj.currentStatus = null;
        }
        [TestMethod] public void IsInitializedTest() {
            obj.currentStatus = mockStatus;
            obj.IsInitialized();
            isTrue(mockStatus.Mock.CalledMethods.Contains(
                nameof(mockStatus.IsInitialized)));
            obj.currentStatus = null;
        }
    }
}