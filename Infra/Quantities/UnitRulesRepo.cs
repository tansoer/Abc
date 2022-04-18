using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;

namespace Abc.Infra.Quantities {
    public sealed class UnitRulesRepo : EntityRepo<UnitRules, UnitRulesData>, IUnitRulesRepo {
        public UnitRulesRepo(QuantityDb c = null) : base(c, c?.UnitRules) { }
    }
}