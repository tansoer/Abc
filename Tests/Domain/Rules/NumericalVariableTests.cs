using System;
using Abc.Domain.Rules;

namespace Abc.Tests.Domain.Rules {

    public class NumericalVariableTests<TClass, T> : ComparableVariableTests<TClass, T>
        where TClass : NumericalVariable<TClass, T> where T : IComparable {


        protected override Type getBaseClass() => typeof(NumericalVariable<TClass, T>);
    }

}