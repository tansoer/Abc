using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Common;

namespace Abc.Facade.Quantities {

    public sealed class UnitViewFactory : AbstractViewFactory<UnitData, Unit, UnitView> {

        public override UnitView Create(Unit o) {
            var v = base.Create(o);
            v.UnitType = o switch
            {
                FactoredUnit _ => UnitType.Factored,
                FunctionedUnit _ => UnitType.Functioned,
                DerivedUnit _ => UnitType.Derived,
                _ => UnitType.Unspecified
            };
            v.Formula = o.Formula(true);
            return v;
        }

        protected internal override Unit toObject(UnitData d) => UnitFactory.Create(d);
    }

}
