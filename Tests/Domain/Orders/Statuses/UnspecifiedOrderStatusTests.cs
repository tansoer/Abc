using Abc.Data.Orders;
using Abc.Domain.Orders.Statuses;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Statuses {
    [TestClass] public class UnspecifiedOrderStatusTests :OrderStatusesTests<UnspecifiedOrderStatus, OrderStatus> {
        protected override UnspecifiedOrderStatus createObject() {
            var d = random<OrderStatusData>();
            d.OrderStatus = OrderLifecycleStatus.Unspecified;
            return OrderStatusFactory.Create(d) as UnspecifiedOrderStatus;
        }
        [TestMethod] public void OpenTest() => testStatus(openEvent);
        [TestMethod] public void CancelTest() => testStatus(cancelEvent);
        [TestMethod] public void CloseTest() => testStatus(closeEvent);
    }
}