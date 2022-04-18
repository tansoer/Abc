using Abc.Data.Inventory;
using Abc.Domain.Products;

namespace Abc.Domain.Inventory {
    public abstract class InventoryEntry :InventoryItem<InventoryEntryData> {
        protected InventoryEntry() : this(null) { }
        protected InventoryEntry(InventoryEntryData d) : base(d) { }
        public string ProductTypeId => get(Data?.ProductTypeId);
        public IProductType ProductType => item<IProductTypesRepo, IProductType>(ProductTypeId);
    }
}
