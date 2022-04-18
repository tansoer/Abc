using Abc.Data.Parties;
using Abc.Domain.Quantities;

namespace Abc.Domain.Parties.Attributes {
    public sealed class BodyMetricQuantity : BodyMetric<Quantity> {
        public BodyMetricQuantity() : this(null) { }
        public BodyMetricQuantity(BodyMetricData d) : base(d) { }
        public override Quantity Value => new(value, unit);
        internal Unit unit => item<IUnitsRepo, Unit>(unitId);
        internal string unitId => get(Data?.UnitId);
        internal double value => double.TryParse(get(Data?.Value), out var d) 
            ? d : double.NaN;
        public override string ToString() => Value.ToString();
    }
}