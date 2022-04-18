using System;
using Abc.Aids.Calculator;
using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Domain.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Values {
    [TestClass]
    public class BaseCommonValueTests :AbstractTests<BaseCommonValue, Value<ValueData>> {
        private class testClass :BaseCommonValue {
            public testClass(ValueData d = null) : base(d) { }
        }
        protected override BaseCommonValue createObject() => new testClass();
    }
    public abstract class BaseValueTests<TClass, T> :SealedTests<TClass, BaseCommonValue<TClass, T>>
        where TClass : BaseCommonValue<TClass, T> where T : IComparable {
        protected Func<TClass, IValue> unaryFunction;
        protected Func<TClass, IValue, IValue> binaryFunction;
        protected Func<TClass, IValue, IValue, IValue> tripleFunction;
        protected string functionSign;
        [TestMethod] public void AddDaysTest() => testBinFunc((x, y) => x.AddDays(y), "+d", addDaysTest);
        [TestMethod] public void AddHoursTest() => testBinFunc((x, y) => x.AddHours(y), "+h", addHoursTest);
        [TestMethod] public void AddMinutesTest() => testBinFunc((x, y) => x.AddMinutes(y), "+m", addMinutesTest);
        [TestMethod] public void AddMonthsTest() => testBinFunc((x, y) => x.AddMonths(y), "+M", addMonthsTest);
        [TestMethod] public void AddSecondsTest() => testBinFunc((x, y) => x.AddSeconds(y), "+s", addSecondsTest);
        [TestMethod] public void AddTest() => testBinFunc((x, y) => x.Add(y), "+", addTest);
        [TestMethod] public void AddYearsTest() => testBinFunc((x, y) => x.AddYears(y), "+y", addYearsTest);
        [TestMethod] public void AndTest() => testBinFunc((x, y) => x.And(y), "&", andTest);
        [TestMethod] public void ContainsTest() => testBinFunc((x, y) => x.Contains(y), "←→", containsTest);
        [TestMethod] public void DivideTest() => testBinFunc((x, y) => x.Divide(y), ":", divideTest);
        [TestMethod] public void EndsWithTest() => testBinFunc((x, y) => x.EndsWith(y), "→", endsWithTest);
        [TestMethod] public void GetAgeTest() => testUnFunc(x => x.GetAge(), "a", getAgeTest);
        [TestMethod] public void GetDayTest() => testUnFunc(x => x.GetDay(), "d", getDayTest);
        [TestMethod] public void GetHourTest() => testUnFunc(x => x.GetHour(), "h", getHourTest);
        [TestMethod] public void GetIntervalTest() => testBinFunc((x, y) => x.GetInterval(y), "←→", intervalTest);
        [TestMethod] public void GetLengthTest() => testUnFunc(x => x.GetLength(), "←→", getLengthTest);
        [TestMethod] public void GetMinuteTest() => testUnFunc(x => x.GetMinute(), "m", getMinuteTest);
        [TestMethod] public void GetMonthTest() => testUnFunc(x => x.GetMonth(), "M", getMonthTest);
        [TestMethod] public void GetSecondTest() => testUnFunc(x => x.GetSecond(), "s", getSecondTest);
        [TestMethod] public void GetYearTest() => testUnFunc(x => x.GetYear(), "y", getYearTest);
        [TestMethod] public void InverseTest() => testUnFunc(x => x.Inverse(), "¹/ₓ", inverseTest);
        [TestMethod] public void IsEqualTest() => testBinFunc((x, y) => x.IsEqual(y), "==", isEqualTest);
        [TestMethod] public void IsGreaterTest() => testBinFunc((x, y) => x.IsGreater(y), ">", isGreaterTest);
        [TestMethod] public void IsLessTest() => testBinFunc((x, y) => x.IsLess(y), "<", isLessTest);
        [TestMethod] public void MultiplyTest() => testBinFunc((x, y) => x.Multiply(y), "*", multiplyTest);
        [TestMethod] public void NotTest() => testUnFunc(x => x.Not(), "!", notTest);
        [TestMethod] public void OppositeTest() => testUnFunc(x => x.Opposite(), "-", oppositeTest);
        [TestMethod] public void OrTest() => testBinFunc((x, y) => x.Or(y), "|", orTest);
        [TestMethod] public void PowerTest() => testBinFunc((x, y) => x.Power(y), "^", powerTest);
        [TestMethod] public void SqrtTest() => testUnFunc(x => x.Sqrt(), "√", sqrtTest);
        [TestMethod] public void SquareTest() => testUnFunc(x => x.Square(), "²", squareTest);
        [TestMethod] public void StartsWithTest() => testBinFunc((x, y) => x.StartsWith(y), "←", startsWithTest);
        [TestMethod] public void SubstringTest() => testTripFunc((x, y, z) => x.Substring(y, z), "→←", substringTest);
        [TestMethod] public void SubstringToEndTest() => testBinFunc((x, y) => x.Substring(y), "→←", substringToEndTest);
        [TestMethod] public void SubtractTest() => testBinFunc((x, y) => x.Subtract(y), "-", subtractTest);
        [TestMethod] public void ToDaysTest() => testUnFunc(x => x.ToDays(), "→d", toDaysTest);
        [TestMethod] public void ToHoursTest() => testUnFunc(x => x.ToHours(), "→h", toHoursTest);
        [TestMethod] public void ToLowerTest() => testUnFunc(x => x.ToLower(), "↓", toLowerTest);
        [TestMethod] public void ToMinutesTest() => testUnFunc(x => x.ToMinutes(), "→m", toMinutesTest);
        [TestMethod] public void ToMonthsTest() => testUnFunc(x => x.ToMonths(), "→M", toMonthsTest);
        [TestMethod] public void ToSecondsTest() => testUnFunc(x => x.ToSeconds(), "→s", toSecondsTest);
        [TestMethod] public void ToUpperTest() => testUnFunc(x => x.ToUpper(), "↑", toUpperTest);
        [TestMethod] public void ToYearsTest() => testUnFunc(x => x.ToYears(), "→y", toYearsTest);
        [TestMethod] public void TrimTest() => testUnFunc(x => x.Trim(), "→←", trimTest);
        [TestMethod] public void XorTest() => testBinFunc((x, y) => x.Xor(y), "^", xorTest);
        protected virtual void addDaysTest() => testBinFuncError();
        protected virtual void addHoursTest() => testBinFuncError();
        protected virtual void addMinutesTest() => testBinFuncError();
        protected virtual void addMonthsTest() => testBinFuncError();
        protected virtual void addSecondsTest() => testBinFuncError();
        protected virtual void addTest() => testBinFuncError();
        protected virtual void addYearsTest() => testBinFuncError();
        protected virtual void andTest() => testBinFuncError();
        protected virtual void containsTest() => testBinFuncError();
        protected virtual void divideTest() => testBinFuncError();
        protected virtual void endsWithTest() => testBinFuncError();
        protected virtual void getAgeTest() => testUnFuncError();
        protected virtual void getDayTest() => testUnFuncError();
        protected virtual void getHourTest() => testUnFuncError();
        protected virtual void getLengthTest() => testUnFuncError();
        protected virtual void getMinuteTest() => testUnFuncError();
        protected virtual void getMonthTest() => testUnFuncError();
        protected virtual void getSecondTest() => testUnFuncError();
        protected virtual void getYearTest() => testUnFuncError();
        protected virtual void intervalTest() => testBinFuncError();
        protected virtual void inverseTest() => testUnFuncError();
        protected virtual void isEqualTest() => testBinFuncError();
        protected virtual void isGreaterTest() => testBinFuncError();
        protected virtual void isLessTest() => testBinFuncError();
        protected virtual void multiplyTest() => testBinFuncError();
        protected virtual void notTest() => testUnFuncError();
        protected virtual void oppositeTest() => testUnFuncError();
        protected virtual void orTest() => testBinFuncError();
        protected virtual void powerTest() => testBinFuncError();
        protected virtual void sqrtTest() => testUnFuncError();
        protected virtual void squareTest() => testUnFuncError();
        protected virtual void startsWithTest() => testBinFuncError();
        protected virtual void substringTest() => testTripFuncError();
        protected virtual void substringToEndTest() => testBinFuncError();
        protected virtual void subtractTest() => testBinFuncError();
        protected virtual void toDaysTest() => testUnFuncError();
        protected virtual void toHoursTest() => testUnFuncError();
        protected virtual void toLowerTest() => testUnFuncError();
        protected virtual void toMinutesTest() => testUnFuncError();
        protected virtual void toMonthsTest() => testUnFuncError();
        protected virtual void toSecondsTest() => testUnFuncError();
        protected virtual void toUpperTest() => testUnFuncError();
        protected virtual void toYearsTest() => testUnFuncError();
        protected virtual void trimTest() => testUnFuncError();
        protected virtual void xorTest() => testBinFuncError();
        protected virtual TClass varX(T v) => (TClass)ValueFactory.Create("x", v);
        protected virtual TClass varY(T v) => (TClass)ValueFactory.Create("y", v);
        protected virtual IValue varY<TArgument>(TArgument v) => ValueFactory.Create("y", v);
        protected virtual TClass varZ(T v) => (TClass)ValueFactory.Create("x", v);
        protected virtual IValue varZ<TArgument>(TArgument v) => ValueFactory.Create("z", v);
        protected void testBinFuncError() {
            var x = varX(GetRandom.ObjectOf<T>());
            var y = varY(GetRandom.ObjectOf<T>());
            assert(binaryFunction(x, y));
        }
        protected void testTripFuncError() {
            var x = varX(GetRandom.ObjectOf<T>());
            var y = varY(GetRandom.ObjectOf<T>());
            var z = varZ(GetRandom.ObjectOf<T>());
            assert(tripleFunction(x, y, z));
        }
        protected void testUnFuncError() {
            var x = varX(GetRandom.ObjectOf<T>());
            assert(unaryFunction(x));
        }
        private void testTripFunc(Func<TClass, IValue, IValue, IValue> f, string sign, Action expected) {
            tripleFunction = f;
            testFunc(sign, expected);
        }
        private void testBinFunc(Func<TClass, IValue, IValue> f, string sign, Action expected) {
            binaryFunction = f;
            testFunc(sign, expected);
        }
        private void testUnFunc(Func<TClass, IValue> f, string sign, Action expected) {
            unaryFunction = f;
            testFunc(sign, expected);
        }
        private void testFunc(string sign, Action expected) {
            functionSign = sign;
            expected();
        }
        private static void validate(object expected, object actual) {
            if (expected is double) assertByChars(expected, actual);
            else if (expected is decimal) assertByChars(expected, actual);
            else if (expected is DateTime) assertByChars(expected, actual);
            else areEqual(expected, actual);
        }
        protected virtual void assert<TArgument>(T a, TArgument b, object expected) {
            var x = varX(a);
            var y = varY(b);
            var name = $"(({a}) {functionSign} ({b}))";
            assert(binaryFunction(x, y), name, expected);
        }
        protected virtual void assert<TArgument>(T a, TArgument b, TArgument c, object expected) {
            var x = varX(a);
            var y = varY(b);
            var z = varZ(c);
            var name = $"(({a}) {functionSign} ({b},{c}))";
            assert(tripleFunction(x, y, z), name, expected);
        }
        protected virtual void assert(T a, T b, object expected) {
            var x = varX(a);
            var y = varY(b);
            var name = $"(({a}) {functionSign} ({b}))";
            assert(binaryFunction(x, y), name, expected);
        }
        protected virtual void assert(T a, object expected) {
            var x = varX(a);
            var name = $"({functionSign} ({a}))";
            assert(unaryFunction(x), name, expected);
        }
        private static void assert(IValue value, string name, object expected) {
            isNotNull(value);
            areEqual(name, value.Name);
            validate(expected, value.GetValue());
        }
        private static void assert(IValue value) {
            isNotNull(value);
            isInstanceOfType(value, typeof(ErrorValue));
        }
        private static void assertByChars(object expected, object actual) {
            var a = actual?.ToString() ?? string.Empty;
            var e = expected?.ToString() ?? string.Empty;
            var count = a.Length < e.Length ? a.Length : e.Length;
            for (var i = 0; i < count; i++) {
                if (a[i] == e[i]) continue;
                if (i < (count / 2) -1 ) areEqual(expected, actual);
            }
        }
    }
}