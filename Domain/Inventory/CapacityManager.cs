using Abc.Data.Inventory;
using Abc.Domain.Common;

namespace Abc.Domain.Inventory {
    public sealed class CapacityManager:Entity<CapacityManagerData> {
        public CapacityManager(): this(null) { }
        public CapacityManager(CapacityManagerData d): base(d) { }
    }
}
