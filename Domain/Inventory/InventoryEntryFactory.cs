using Abc.Data.Inventory;
using Abc.Data.Products;

namespace Abc.Domain.Inventory {
    public static class InventoryEntryFactory {
        public static InventoryEntry Create(InventoryEntryData d) =>
            d.InventoryEntryType switch {
                ProductKind.Service => new ServiceInventoryEntry(d),
                _ => new ProductInventoryEntry(d),
            };
    }
}
