using Abc.Aids.Extensions;
using Abc.Data.Common;

namespace Abc.Domain.Values {
    public sealed class DoubleValue : BaseNumericalValue<DoubleValue, double> {
        public DoubleValue(ValueData d = null) : base(d) { }
        public DoubleValue(string name, double value)
            : base(name, new ValueData {
                Value = Variable.ToString(value),
                UnitOrCurrencyId = null,
                ValueType = ValueType.Double
            }) { }
        protected override double toValue(string v) => Variable.TryParse<double>(v);
    }
}
