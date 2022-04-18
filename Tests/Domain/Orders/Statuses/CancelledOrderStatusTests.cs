using Abc.Data.Orders;
using Abc.Domain.Orders.Statuses;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Statuses {

    [TestClass] public class CancelledOrderStatusTests :OrderStatusesTests<CancelledOrderStatus, OrderStatus> {

        protected override CancelledOrderStatus createObject() {
            var d = random<OrderStatusData>();
            d.OrderStatus = OrderLifecycleStatus.Cancelled;
            return OrderStatusFactory.Create(d) as CancelledOrderStatus;
        }

        [TestMethod] public void OpenTest() => testStatus(openEvent);
        [TestMethod] public void CancelTest() => testStatus(cancelEvent);
        [TestMethod] public void CloseTest() => testStatus(closeEvent);
    }
}