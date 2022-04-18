using Abc.Aids.Extensions;
using Abc.Data.Common;

namespace Abc.Domain.Values {

    public sealed class IntegerValue : BaseNumericalValue<IntegerValue, int> {
        public IntegerValue(ValueData d = null) : base(d) { }
        public IntegerValue(string name, int value)
            : base(name, new ValueData {
                Value = Variable.ToString(value),
                UnitOrCurrencyId = null,
                ValueType = ValueType.Integer
            }) {
        }
        protected override int toValue(string v) => Variable.TryParse<int>(v);
    }
}