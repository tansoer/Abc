using Abc.Data.Common;

namespace Abc.Data.Inventory {
    public abstract class InventoryItemData: EntityBaseData {
        public string InventoryId { get; set; }
    }
}