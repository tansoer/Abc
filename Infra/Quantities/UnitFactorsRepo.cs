using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;

namespace Abc.Infra.Quantities {

    public sealed class UnitFactorsRepo : EntityRepo<UnitFactor, UnitFactorData>, IUnitFactorsRepo {
        public UnitFactorsRepo(QuantityDb c = null) : base(c, c?.UnitFactors) { }
    }
}
