using Abc.Data.Quantities;

namespace Abc.Domain.Quantities {

    public sealed class UnitFactor : UnitAttribute<UnitFactorData> {
        public UnitFactor() : this(null) { }
        public UnitFactor(UnitFactorData d) : base(d) { }
        public double Factor => Data?.Factor ?? 0D;
    }
}
