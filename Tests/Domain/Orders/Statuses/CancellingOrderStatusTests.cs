using Abc.Data.Orders;
using Abc.Domain.Orders.Statuses;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Statuses {
    [TestClass]
    public class CancellingOrderStatusTests :OrderStatusesTests<CancellingOrderStatus, OrderStatus> {

        protected override CancellingOrderStatus createObject() {
            var d = random<OrderStatusData>();
            d.OrderStatus = OrderLifecycleStatus.Cancelling;
            return OrderStatusFactory.Create(d) as CancellingOrderStatus;
        }
        [TestMethod] public void CancelTest() =>
            testStatus(cancelEvent, OrderLifecycleStatus.Cancelled, null, true);
        [TestMethod] public void CloseTest() =>
            testStatus(closeEvent, OrderLifecycleStatus.Cancelled, null, true);
        
        [TestMethod] public void OpenOrderIsNotCancellableTest() => 
            testStatus(openEvent,OrderLifecycleStatus.Open, null, false);
    }
}