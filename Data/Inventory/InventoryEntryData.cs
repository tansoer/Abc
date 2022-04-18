
using Abc.Data.Products;

namespace Abc.Data.Inventory {
    public sealed class InventoryEntryData: InventoryItemData {
        public string RestockPolicyId { get; set; }
        public string ProductTypeId { get; set; }
        public ProductKind InventoryEntryType { get; set; }
        public string CapacityManagerId { get; set; }
    }
}
