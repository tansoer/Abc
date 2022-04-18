using System;
using Abc.Domain.Values;

namespace Abc.Tests.Domain.Values {

    public class BaseComparableValueTests<TClass, T> : BaseValueTests<TClass, T>
        where TClass : BaseComparableValue<TClass, T> where T : IComparable {
        protected T f = default;
        protected T t = default;
        protected override Type getBaseClass() => typeof(BaseComparableValue<TClass, T>);
        protected override void isEqualTest() {
            assert(f, f, true);
            assert(f, t, false);
            assert(t, f, false);
            assert(t, t, true);
        }
        protected override void isGreaterTest() {
            assert(t, t, false);
            assert(t, f, true);
            assert(f, t, false);
            assert(f, f, false);
        }
        protected override void isLessTest() {
            assert(t, t, false);
            assert(t, f, false);
            assert(f, t, true);
            assert(f, f, false);
        }
    }
}