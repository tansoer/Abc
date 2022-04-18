using Abc.Data.Orders;
using Abc.Domain.Orders.Statuses;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Statuses {

    [TestClass]
    public class OpenOrderStatusTests :OrderStatusesTests<OpenOrderStatus, OrderStatus> {
        protected override OpenOrderStatus createObject() {
            var d = random<OrderStatusData>();
            d.OrderStatus = OrderLifecycleStatus.Open;
            return OrderStatusFactory.Create(d) as OpenOrderStatus;
        }
        [TestMethod]public void OpenTest() => testStatus(openEvent);
        [TestMethod] public void CancelTest() 
            => testStatus(cancelEvent,OrderLifecycleStatus.Cancelling, null, null);
        [TestMethod] public void CloseTest() 
            => testStatus(closeEvent,OrderLifecycleStatus.Closed, true, null);
    }
}