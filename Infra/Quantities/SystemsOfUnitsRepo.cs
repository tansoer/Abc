using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;

namespace Abc.Infra.Quantities {

    public sealed class SystemsOfUnitsRepo : EntityRepo<SystemOfUnits, SystemOfUnitsData>,
        ISystemsOfUnitsRepo {
        public SystemsOfUnitsRepo(QuantityDb c = null) : base(c, c?.SystemsOfUnits) { }
    }
}

