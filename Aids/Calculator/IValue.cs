using System;

namespace Abc.Aids.Calculator {

    public interface IValue<out TValue> :IValue where TValue : IComparable {
        TValue Value { get; }
    }
    public interface IValue :IStringOperations, IDateTimeOperations,
        IBooleanOperations, IArithmeticOperations, IComparisonOperations {
        object GetValue();
        string Name { get; }
        IValue Calculate(Operation op, IValue a = null, IValue b = null);
        bool IsUnspecified { get; }
    }
    public interface IComparisonOperations {
        IValue IsEqual(IValue v);
        IValue IsGreater(IValue v);
        IValue IsLess(IValue v);
    }
    public interface IBooleanOperations {
        IValue And(IValue v);
        IValue Or(IValue v);
        IValue Xor(IValue v);
        IValue Not();
    }
    public interface IArithmeticOperations {
        IValue Add(IValue v);
        IValue Multiply(IValue v);
        IValue Divide(IValue v);
        IValue Subtract(IValue v);
        IValue Power(IValue v);
        IValue Inverse();
        IValue Opposite();
        IValue Square();
        IValue Sqrt();
    }
    public interface IDateTimeOperations :IDateTimeAddOperations,
        IDateTimeConvertOperations {
        IValue GetSecond();
        IValue GetMinute();
        IValue GetHour();
        IValue GetDay();
        IValue GetMonth();
        IValue GetYear();
        IValue GetInterval(IValue v);
        IValue GetAge();
    }
    public interface IDateTimeConvertOperations {
        IValue ToSeconds();
        IValue ToMinutes();
        IValue ToHours();
        IValue ToDays();
        IValue ToMonths();
        IValue ToYears();
    }
    public interface IDateTimeAddOperations {
        IValue AddSeconds(IValue v);
        IValue AddMinutes(IValue v);
        IValue AddHours(IValue v);
        IValue AddDays(IValue v);
        IValue AddMonths(IValue v);
        IValue AddYears(IValue v);
    }
    public interface IStringOperations {
        IValue GetLength();
        IValue ToUpper();
        IValue ToLower();
        IValue Trim();
        IValue Substring(IValue x, IValue y = null);
        IValue Contains(IValue v);
        IValue EndsWith(IValue v);
        IValue StartsWith(IValue v);
    }
}
