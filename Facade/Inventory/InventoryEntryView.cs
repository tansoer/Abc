using Abc.Data.Products;
using System.ComponentModel;

namespace Abc.Facade.Inventory {
    public sealed class InventoryEntryView :InventoryItemView{
        [DisplayName("Restock Policy")]
        public string RestockPolicyId { get; set; }
        [DisplayName("Product Type")]
        public string ProductTypeId { get; set; }
        [DisplayName("Inventory Entry Type")]
        public ProductKind InventoryEntryType { get; set; }
        [DisplayName("Capacity Manager")]
        public string CapacityManagerId { get; set; }
    }
}
