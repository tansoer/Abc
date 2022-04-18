using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Discounts;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Discounts {

    [TestClass]
    public class DiscountEventTests :SealedTests<DiscountEvent, OrderEvent> {

        protected override DiscountEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.DiscountEvent;
            return OrderEventFactory.Create(d) as DiscountEvent;
        }

        [TestMethod]
        public async Task DiscountsTest()
            => await testListAsync<IDiscount, DiscountData, IDiscountsRepo>(
                x => x.OrderEventId = obj.Id, DiscountFactory.Create);
    }
}