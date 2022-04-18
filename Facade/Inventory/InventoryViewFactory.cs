using Abc.Data.Inventory;
using Abc.Facade.Common;

namespace Abc.Facade.Inventory {
    public sealed class InventoryViewFactory :AbstractViewFactory<
        InventoryData, Domain.Inventory.Inventory, InventoryView> {
        protected internal override Domain.Inventory.Inventory toObject(InventoryData d)
            => new(d);
    }
}
