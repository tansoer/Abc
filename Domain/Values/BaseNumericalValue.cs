using System;
using Abc.Aids.Calculator;
using Abc.Aids.Values;
using Abc.Data.Common;

namespace Abc.Domain.Values {
    public abstract class BaseNumericalValue<TVariable, TValue> : BaseComparableValue<TVariable, TValue>
        where TVariable : IValue<TValue>
        where TValue : IComparable {
        protected BaseNumericalValue(ValueData d = null) : base(d) { }
        protected BaseNumericalValue(string name, ValueData d = null) : base(name, d) { }
        public override IValue Multiply(IValue v) => variable(v, "*", Compute.Multiply);
        public override IValue Divide(IValue v) => variable(v, ":", Compute.Divide);
        public override IValue Power(IValue v) => variable(v, "^", Compute.Power);
        public override IValue Inverse() => variable("¹/ₓ", Compute.Inverse);
        public override IValue Square() => variable("²", Compute.Square);
        public override IValue Sqrt() => variable("√", Compute.Sqrt);
    }
}