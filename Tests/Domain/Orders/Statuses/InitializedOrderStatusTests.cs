using Abc.Data.Orders;
using Abc.Domain.Orders.Statuses;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Statuses {

    [TestClass]
    public class InitializedOrderStatusTests :OrderStatusesTests<InitializedOrderStatus, OrderStatus> {

        protected override InitializedOrderStatus createObject() {
            var d = random<OrderStatusData>();
            d.OrderStatus = OrderLifecycleStatus.Initialized;
            return OrderStatusFactory.Create(d) as InitializedOrderStatus;
        }
        [TestMethod] public void OpenTest() => testStatus(openEvent, OrderLifecycleStatus.Open, null, null);
        [TestMethod] public void CancelTest() => testStatus(cancelEvent, OrderLifecycleStatus.Cancelled, null, null);
        [TestMethod] public void CloseTest() => testStatus(closeEvent, OrderLifecycleStatus.Cancelled, null, null);
    }
}
