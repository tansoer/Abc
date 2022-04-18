using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Orders.Statuses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Abc.Aids.Random;
using System.Threading.Tasks;
using Abc.Domain.Orders;
using System;
using Abc.Facade.Orders;

namespace Abc.Tests.Domain.Orders.Statuses {
    [TestClass]
    public class OrderStatusTests :AbstractTests<OrderStatus, Entity<OrderStatusData>> {
        private ILifecycleEvent testEvent;
        private IOrderStatus currentStatus;
        private IOrderStatus newStatus;
        private IOpenOrderEvent openEvent;
        private ICloseOrderEvent closeEvent;
        private ICancelOrderEvent cancelEvent;
        private class testClass :OrderStatus {
            public testClass() : this(null) { }
            public testClass(OrderStatusData d) : base(d) { }
        }
        protected override OrderStatus createObject()
            => new testClass(random<OrderStatusData>());
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            testEvent = createEvent<LifecycleEvent>((OrderEventType)GetRandom.UInt8(1,3));
            openEvent = createEvent<OpenOrderEvent>(OrderEventType.OpenEvent);
            closeEvent = createEvent<CloseOrderEvent>(OrderEventType.CloseEvent);
            cancelEvent = createEvent<CancelOrderEvent>(OrderEventType.CancelEvent);
            isNotNull(testEvent);
            isNotNull(openEvent);
            isNotNull(closeEvent);
            isNotNull(cancelEvent);
            currentStatus = null;
            newStatus = null;
        }

        private static T createEvent<T>(OrderEventType t) where T: LifecycleEvent =>
            LifecycleEventTests.createEvent<T>(t);

        [TestMethod] public void StatusTest() => isReadOnly(obj.Data.OrderStatus);
        [TestMethod] public void OrderIdTest() => isReadOnly(obj.Data.OrderId);
        [TestMethod] public async Task OrderTest() => 
            await testItemAsync<OrderData, IOrder, IOrdersRepo>(
                obj.OrderId, () => obj.Order.Data, d => OrderFactory.Create (d));
        [TestMethod] public void OrderEventIdTest() => isReadOnly(obj.Data.OrderEventId);
        [TestMethod] public void ProcessOrderEventTest() 
            => areEqual(false, obj.ProcessOrderEvent(testEvent));
        [TestMethod] public void ErrorTest() => areEqual(false, obj.error(testEvent));
        [TestMethod] public void OpenTest() => areEqual(false, obj.open(openEvent));
        [TestMethod] public void CloceTest() => areEqual(false, obj.close(closeEvent));
        [TestMethod] public void CancelTest() => areEqual(false, obj.cancel(cancelEvent));
        
        [DataTestMethod] 
        [DataRow(OrderLifecycleStatus.Unspecified)]
        [DataRow(OrderLifecycleStatus.Initialized)]
        [DataRow(OrderLifecycleStatus.Open)]
        [DataRow(OrderLifecycleStatus.Closed)]
        [DataRow(OrderLifecycleStatus.Cancelling)]
        [DataRow(OrderLifecycleStatus.Cancelled)]
        public void OnStatusChangedTest(OrderLifecycleStatus s) {
            obj.OnStatusChanged += onStatusChanged;
            obj.changeStatus(testEvent, s);
            allPropertiesAreEqual(obj.Data, currentStatus.Data);
            areEqual(s, newStatus.Status);
            areEqual(testEvent.Id, newStatus.OrderEventId);
            areEqual(obj.OrderId, newStatus.OrderId);
            isTrue(newStatus.ValidFrom < DateTime.Now);
            isTrue(newStatus.ValidFrom > DateTime.Now.AddSeconds(-1));
        }
        [DataTestMethod]
        [DataRow(OrderLifecycleStatus.Unspecified, false)]
        [DataRow(OrderLifecycleStatus.Initialized, true)]
        [DataRow(OrderLifecycleStatus.Open, false)]
        [DataRow(OrderLifecycleStatus.Closed, false)]
        [DataRow(OrderLifecycleStatus.Cancelling, false)]
        [DataRow(OrderLifecycleStatus.Cancelled, false)]
        public void IsInitializedTest(OrderLifecycleStatus s, bool expected) {
            var d = random<OrderStatusData>();
            d.OrderStatus = s;
            var o = OrderStatusFactory.Create(d);
            areEqual(expected, o.IsInitialized());
        }
        private void onStatusChanged(object sender, IOrderStatus e) {
            currentStatus = sender as IOrderStatus;
            newStatus = e;
        }
    }
}
