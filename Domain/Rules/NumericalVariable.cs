using System;
using Abc.Aids.Values;
using Abc.Data.Rules;

namespace Abc.Domain.Rules {

    public abstract class NumericalVariable<TVariable, TValue> : ComparableVariable<TVariable, TValue>
        where TVariable : IVariable<TValue>
        where TValue : IComparable {

        protected NumericalVariable(RuleElementData d = null) : base(d) { }

        public override IVariable Multiply(IVariable v) => variable(v, "*", Compute.Multiply);

        public override IVariable Divide(IVariable v) => variable(v, ":", Compute.Divide);

        public override IVariable Power(IVariable v) => variable(v, "^", Compute.Power);

        public override IVariable Inverse() => variable("¹/ₓ", Compute.Inverse);

        public override IVariable Square() => variable("²", Compute.Square);

        public override IVariable Sqrt() => variable("√", Compute.Sqrt);


    }

}