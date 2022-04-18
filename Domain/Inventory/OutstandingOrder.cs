using Abc.Data.Inventory;
using Abc.Domain.Common;
using Abc.Domain.Orders;

namespace Abc.Domain.Inventory {
    public sealed class OutstandingOrder:Entity<OutstandingOrderData> {
        public OutstandingOrder(): this(null) { }
        public OutstandingOrder(OutstandingOrderData d) : base(d) { }
        public string PurchaseOrderId => get(Data?.PurchaseOrderId);
        public string InventoryEntryId => get(Data?.InventoryEntryId);
        public PurchaseOrder PurchaseOrder 
            => item<IOrdersRepo, IOrder>(PurchaseOrderId) as PurchaseOrder;
    }
}
