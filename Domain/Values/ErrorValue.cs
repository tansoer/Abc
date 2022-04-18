using Abc.Aids.Calculator;
using Abc.Data.Common;
using Abc.Domain.Common;

namespace Abc.Domain.Values {
    public sealed class ErrorValue : BaseCommonValue<ErrorValue, string> {
        public ErrorValue(ValueData d = null) : base(d) { }
        public ErrorValue(string name) : base(name, new ValueData {
            Value = name,
            ValueType = ValueType.Error,
        }) { }
        protected override string toValue(string v) => v ?? Unspecified.String;
        public override IValue IsEqual(IValue v) => v;
        public override IValue IsGreater(IValue v) => v;
        public override IValue IsLess(IValue v) => v;
    }
}
