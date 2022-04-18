using Abc.Data.Inventory;

namespace Abc.Domain.Inventory {
    public sealed class ServiceInventoryEntry :InventoryEntry {
        public ServiceInventoryEntry() : this(null) { }
        public ServiceInventoryEntry(InventoryEntryData d) : base(d) { }
        public string CapacityManagerId => get(Data?.CapacityManagerId);
        public CapacityManager CapacityManager
            => item<ICapacityManagersRepo, CapacityManager>(CapacityManagerId);
    }
}
