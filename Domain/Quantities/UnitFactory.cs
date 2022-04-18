using Abc.Data.Quantities;

namespace Abc.Domain.Quantities {
    public static class UnitFactory {
        public static Unit Create(UnitData d = null) => d?.UnitType switch {
                UnitType.Derived => new DerivedUnit(d),
                UnitType.Functioned => new FunctionedUnit(d),
                _ => new FactoredUnit(d)
            };
    }
}