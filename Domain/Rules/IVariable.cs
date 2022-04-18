using System;
using Abc.Aids.Calculator;

namespace Abc.Domain.Rules {

    public interface IVariable<out TValue> : IVariable where TValue : IComparable {
        TValue Value { get; }
    }
    public interface IVariable : IRuleElement {
        object GetValue();
        IVariable Calculate(Operation op, IVariable a = null, IVariable b = null);
        IVariable IsEqual(IVariable v);
        IVariable IsGreater(IVariable v);
        IVariable IsLess(IVariable v);
        IVariable And(IVariable v);
        IVariable Or(IVariable v);
        IVariable Xor(IVariable v);
        IVariable Not();
        IVariable Add(IVariable v);
        IVariable Multiply(IVariable v);
        IVariable Divide(IVariable v);
        IVariable Subtract(IVariable v);
        IVariable Power(IVariable v);
        IVariable Inverse();
        IVariable Opposite();
        IVariable Square();
        IVariable Sqrt();
        IVariable GetSecond();
        IVariable GetMinute();
        IVariable GetHour();
        IVariable GetDay();
        IVariable GetMonth();
        IVariable GetYear();
        IVariable GetInterval(IVariable v);
        IVariable GetAge();
        IVariable ToSeconds();
        IVariable ToMinutes();
        IVariable ToHours();
        IVariable ToDays();
        IVariable ToMonths();
        IVariable ToYears();
        IVariable AddSeconds(IVariable v);
        IVariable AddMinutes(IVariable v);
        IVariable AddHours(IVariable v);
        IVariable AddDays(IVariable v);
        IVariable AddMonths(IVariable v);
        IVariable AddYears(IVariable v);
        IVariable GetLength();
        IVariable ToUpper();
        IVariable ToLower();
        IVariable Trim();
        IVariable Substring(IVariable x, IVariable y = null);
        IVariable Contains(IVariable v);
        IVariable EndsWith(IVariable v);
        IVariable StartsWith(IVariable v);
    }

}
