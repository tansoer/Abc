using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Common;

namespace Abc.Facade.Quantities {

    public sealed class SystemOfUnitsViewFactory : AbstractViewFactory<SystemOfUnitsData, SystemOfUnits, SystemOfUnitsView> {
        protected internal override SystemOfUnits toObject(SystemOfUnitsData d) => new(d);
    }

}
