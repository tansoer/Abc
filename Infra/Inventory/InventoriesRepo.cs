using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Infra.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Inventory {
    public sealed class InventoriesRepo :PagedRepo<Domain.Inventory.Inventory,
        InventoryData>, IInventoriesRepo {
        public InventoriesRepo(InventoryDb c = null) :base(c, c.Inventories) { }
        protected internal override Domain.Inventory.Inventory toDomainObject(InventoryData d)
            => new(d);
    }
}
