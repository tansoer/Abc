using Abc.Data.Inventory;
using Abc.Data.Orders;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Domain.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Inventory {
    [TestClass]
    public class ProductInventoryEntryTests: SealedTests<ProductInventoryEntry, InventoryEntry> {
        protected override ProductInventoryEntry createObject() {
            var d = random<InventoryEntryData>();
            while (d.InventoryEntryType == ProductKind.Service) d = random<InventoryEntryData>();
            return InventoryEntryFactory.Create(d) as ProductInventoryEntry;
        }
        [TestMethod] public void RestockPolicyIdTest() => isReadOnly(obj.Data.RestockPolicyId);
        [TestMethod] public async Task RestockPolicyTest() {
            await testItemAsync<RestockPolicyData, RestockPolicy, IRestockPoliciesRepo>(
                obj.RestockPolicyId,
                () => obj.RestockPolicy.Data,
                d => new RestockPolicy(d));
            obj = createObject();
            isNotNull(obj.RestockPolicy);
            isNotNull(obj.RestockPolicy.Data);
        }
        [TestMethod] public void OutstandingOrdersTest() {
            isReadOnly();
            areEqual(0, obj.OutstandingOrders.Count);
            areEqual(0, obj.outstandingOrders.Count);
            for (int i = 0; i < 10; i++) {
                addOutstandingOrdersWithPurchaseOrders();
            }
            areNotEqual(0, obj.OutstandingOrders.Count);
            areNotEqual(0, obj.outstandingOrders.Count);
            areEqual(obj.outstandingOrders.Count, obj.OutstandingOrders.Count);
        }
        private void addOutstandingOrdersWithPurchaseOrders() {
            var purchaseOrderData = random<OrderData>();
            purchaseOrderData.OrderType = OrderType.PurchaseOrder;
            var outstandingOrderData = random<OutstandingOrderData>();
            outstandingOrderData.PurchaseOrderId = purchaseOrderData.Id;
            outstandingOrderData.InventoryEntryId = obj.Id;
            GetRepo.Instance<IOrdersRepo>().Add(OrderFactory.Create(purchaseOrderData));
            GetRepo.Instance<IOutstandingOrdersRepo>().Add(new OutstandingOrder(outstandingOrderData));
        }
    }
}
