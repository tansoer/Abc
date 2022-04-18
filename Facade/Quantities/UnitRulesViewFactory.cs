using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Common;

namespace Abc.Facade.Quantities {
    public sealed class UnitRulesViewFactory :AbstractViewFactory<UnitRulesData, UnitRules, UnitRulesView> {
        public override UnitRulesView Create(UnitRules o) {
            var v = base.Create(o);
            v.ToFormula = o?.ToBaseUnitRule?.Formula();
            v.FromFormula = o?.FromBaseUnitRule?.Formula();
            return v;
        }
        protected internal override UnitRules toObject(UnitRulesData d) => new(d);
    }

}
