using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Infra.Common;

namespace Abc.Infra.Inventory {
    public sealed class CapacityManagersRepo :
        EntityRepo<CapacityManager, CapacityManagerData>, ICapacityManagersRepo {
        public CapacityManagersRepo(InventoryDb c) : base(c, c?.CapacityManagers) { }
    }
}