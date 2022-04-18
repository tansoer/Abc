using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Facade.Common;

namespace Abc.Facade.Inventory {
    public sealed class CapacityManagerViewFactory :AbstractViewFactory<CapacityManagerData,
    CapacityManager, CapacityManagerView> {
        protected internal override CapacityManager toObject(CapacityManagerData d) => new(d);
    }
}
