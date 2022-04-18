using Abc.Data.Inventory;
using Abc.Domain.Common;

namespace Abc.Domain.Inventory {
    public abstract class InventoryItem<TData> :Entity<TData>
        where TData: InventoryItemData, new(){
        protected InventoryItem() : this(null) { }
        protected InventoryItem(TData d) : base(d) { }
        public string InventoryId => get(Data?.InventoryId);
    }
}
