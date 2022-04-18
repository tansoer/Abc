using Abc.Data.Inventory;
using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Domain.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Inventory {
    [TestClass]
    public class OutstandingOrderTests: SealedTests<OutstandingOrder, Entity<OutstandingOrderData>> {
        protected override OutstandingOrder createObject() => new (random<OutstandingOrderData>());
        [TestMethod] public void PurchaseOrderIdTest() => isReadOnly(obj.Data.PurchaseOrderId);
        [TestMethod] public void InventoryEntryIdTest() => isReadOnly(obj.Data.InventoryEntryId);
        [TestMethod] public async Task PurchaseOrderTest() {
            var d = random<OrderData>();
            d.Id = obj.PurchaseOrderId;
            d.OrderType = OrderType.PurchaseOrder;
            await testItemAsync<OrderData, IOrder, IOrdersRepo>
            (d, () => obj.PurchaseOrder.Data, OrderFactory.Create);
        }
    }
}
