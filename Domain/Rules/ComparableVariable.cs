using System;
using Abc.Aids.Values;
using Abc.Data.Rules;

namespace Abc.Domain.Rules {

    public abstract class ComparableVariable<TVariable, TValue> : BaseVariable<TVariable, TValue>
        where TVariable : IVariable<TValue>
        where TValue : IComparable {

        protected ComparableVariable(RuleElementData d = null) : base(d) { }

        public override IVariable Add(IVariable v) => variable(v, "+", Compute.Add);

        public override IVariable Subtract(IVariable v) => variable(v, "-", Compute.Subtract);

        public override IVariable Opposite() => variable("-", Compute.Opposite);


    }

}

