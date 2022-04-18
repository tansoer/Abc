using Abc.Data.Common;

namespace Abc.Data.Inventory {
    public sealed class OutstandingOrderData: EntityBaseData {
        public string PurchaseOrderId { get; set; }
        public string InventoryEntryId { get; set; }
    }
}
