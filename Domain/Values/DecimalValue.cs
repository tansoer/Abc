using Abc.Aids.Extensions;
using Abc.Data.Common;

namespace Abc.Domain.Values {

    public sealed class DecimalValue : BaseNumericalValue<DecimalValue, decimal> {

        public DecimalValue(ValueData d = null) : base(d) { }
        public DecimalValue(string name, decimal value)
            : base(name, new ValueData {
                Value = Variable.ToString(value),
                UnitOrCurrencyId = null,
                ValueType = ValueType.Decimal
            }) {
        }

        protected override decimal toValue(string v) => Variable.TryParse<decimal>(v);

    }

}