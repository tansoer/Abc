using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Infra.Common;

namespace Abc.Infra.Inventory {
    public sealed class InventoryEntriesRepo
        :PagedRepo<InventoryEntry, InventoryEntryData>, IInventoryEntriesRepo {
        public InventoryEntriesRepo(InventoryDb c = null) : base(c, c?.InventoryEntries) { }
        protected internal override InventoryEntry toDomainObject(InventoryEntryData d)
            => InventoryEntryFactory.Create(d);
    }
}