using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Statuses;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Statuses {

    [TestClass]
    public class CancelOrderEventTests :SealedTests<CancelOrderEvent, LifecycleEvent> {

        protected override CancelOrderEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.CancelEvent;
            return OrderEventFactory.Create(d) as CancelOrderEvent;
        }

        [TestMethod]
        public async Task ReturnedItemsTest()
            => await testListAsync<ReturnedItem, ReturnedItemData, IReturnedItemsRepo>(
                x => x.OrderEventId = obj.Id, x => new ReturnedItem(x));
    }
}