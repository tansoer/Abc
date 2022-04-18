using System;
using Abc.Aids.Calculator;
using Abc.Aids.Values;
using Abc.Data.Common;

namespace Abc.Domain.Values {

    public abstract class BaseComparableValue<TVariable, TValue> : BaseCommonValue<TVariable, TValue>
        where TVariable : IValue<TValue>
        where TValue : IComparable {
        protected BaseComparableValue(ValueData d = null) : base(d) { }
        protected BaseComparableValue(string name, ValueData d = null) : base(name, d) { }
        public override IValue Add(IValue v) => variable(v, "+", Compute.Add);
        public override IValue Subtract(IValue v) => variable(v, "-", Compute.Subtract);
        public override IValue Opposite() => variable("-", Compute.Opposite);
    }
}

