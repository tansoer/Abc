using Abc.Aids.Calculator;
using Abc.Aids.Values;
using Abc.Data.Common;
using Abc.Domain.Common;

namespace Abc.Domain.Values {

    public sealed class StringValue : BaseCommonValue<StringValue, string> {
        public StringValue(ValueData d) : base(d) { }
        public StringValue(string name, string value)
            : base(name, new ValueData {
                Value = value,
                UnitOrCurrencyId = null,
                ValueType = ValueType.String
            }) { }
        protected override string toValue(string v) => v ?? Unspecified.String;
        public override IValue Add(IValue v) => variable(v, "+", Compute.Add);
        public override IValue GetLength() => variable("←→", Compute.GetLength);
        public override IValue ToUpper() => variable("↑", Compute.ToUpper);
        public override IValue ToLower() => variable("↓", Compute.ToLower);
        public override IValue Trim() => variable("→←", Compute.Trim);
        public override IValue Substring(IValue x, IValue y = null) => variable(x, y, "→←", Compute.Substring);
        public override IValue Contains(IValue v) => variable(v, "←→", Compute.Contains);
        public override IValue EndsWith(IValue v) => variable(v, "→", Compute.EndsWith);
        public override IValue StartsWith(IValue v) => variable(v, "←", Compute.StartsWith);
    }

}
