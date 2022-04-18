using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Facade.Common;

namespace Abc.Facade.Inventory {
    public sealed class InventoryEntryViewFactory :AbstractViewFactory<InventoryEntryData, 
        InventoryEntry, InventoryEntryView> {
        protected internal override InventoryEntry toObject(InventoryEntryData d)
            => InventoryEntryFactory.Create(d);
    }
}
