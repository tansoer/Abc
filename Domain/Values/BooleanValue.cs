using Abc.Aids.Calculator;
using Abc.Aids.Extensions;
using Abc.Aids.Values;
using Abc.Data.Common;

namespace Abc.Domain.Values {

    public sealed class BooleanValue : BaseCommonValue<BooleanValue, bool> {

        public BooleanValue(ValueData d = null) : base(d) { }
        public BooleanValue(string name, bool value)
            : base(name, new ValueData {
                Value = Variable.ToString(value),
                UnitOrCurrencyId = null,
                ValueType = ValueType.Boolean
            }) { }

        protected override bool toValue(string v) => Variable.TryParse<bool>(v);

        public override IValue Not() => variable("!", Compute.Not);

        public override IValue And(IValue v) => variable(v, "&", Compute.And);

        public override IValue Or(IValue v) => variable(v, "|", Compute.Or);

        public override IValue Xor(IValue v) => variable(v, "^", Compute.Xor);

        public override IValue Add(IValue v) => variable(v, "+", Compute.Add);

        public override IValue Multiply(IValue v) => variable(v, "*", Compute.Multiply);

    }

}