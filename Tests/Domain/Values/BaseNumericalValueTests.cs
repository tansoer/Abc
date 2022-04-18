using System;
using Abc.Domain.Values;

namespace Abc.Tests.Domain.Values {
    public class BaseNumericalValueTests<TClass, T> : BaseComparableValueTests<TClass, T>
        where TClass : BaseNumericalValue<TClass, T> where T : IComparable {
        protected override Type getBaseClass() => typeof(BaseNumericalValue<TClass, T>);
    }
}