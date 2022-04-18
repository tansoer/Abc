using Abc.Data.Orders;
using Abc.Domain.Orders.Statuses;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Domain.Orders.Statuses {
    [TestClass]
    public class OrderStatusFactoryTests: TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(OrderStatusFactory);
        
        [DataTestMethod]
        [DataRow(OrderLifecycleStatus.Initialized, typeof(InitializedOrderStatus))]
        [DataRow(OrderLifecycleStatus.Open, typeof(OpenOrderStatus))]
        [DataRow(OrderLifecycleStatus.Closed, typeof(ClosedOrderStatus))]
        [DataRow(OrderLifecycleStatus.Cancelling, typeof(CancellingOrderStatus))]
        [DataRow(OrderLifecycleStatus.Cancelled, typeof(CancelledOrderStatus))]
        [DataRow(OrderLifecycleStatus.Unspecified, typeof(UnspecifiedOrderStatus))]

        public void CreateTest(OrderLifecycleStatus status, Type t) {
            var d = random<OrderStatusData>();
            d.OrderStatus = status;
            var o = OrderStatusFactory.Create(d);
            Assert.IsInstanceOfType(o, t);
            allPropertiesAreEqual(d, o.Data);
        }
    }
}
