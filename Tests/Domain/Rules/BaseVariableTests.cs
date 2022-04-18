using System;
using Abc.Aids.Random;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Rules {

    public class BaseVariableTests<TClass, T> : SealedTests<TClass, BaseVariable<TClass, T>>
        where TClass : BaseVariable<TClass, T> where T : IComparable {

        protected Func<TClass, IVariable, IVariable> function;
        protected Func<TClass, IVariable> unaryFunction;
        protected Func<TClass, IVariable, IVariable, IVariable> tripleFunction;
        protected string operation;

        protected virtual TClass varX(T v) => (TClass) VariableFactory.Create("x", v);
        protected virtual TClass varY(T v) => (TClass) VariableFactory.Create("y", v);
        protected virtual TClass varZ(T v) => (TClass) VariableFactory.Create("z", v);
        protected virtual IVariable varY<TArgument>(TArgument v) => VariableFactory.Create("y", v);
        protected virtual IVariable varZ<TArgument>(TArgument v) => VariableFactory.Create("z", v);

        protected virtual void assert<TArgument>(T a, TArgument b, object expected) {
            var x = varX(a);
            var y = varY(b);
            var name = $"((x={a}) {operation} (y={b}))";
            var z = function(x, y);
            Assert.IsNotNull(z);
            Assert.AreEqual(name, z.Name);
            validate(expected, z.GetValue());
        }
        private void validate(object expected, object actual) {
            if (expected is double) validateByChars(expected, actual);
            else if (expected is decimal) validateByChars(expected, actual);
            else if (expected is DateTime) validateByChars(expected, actual);
            else Assert.AreEqual(expected, actual);
        }
        protected virtual void assert<TArgument>(T a, TArgument b, TArgument c, object expected) {
            var x = varX(a);
            var y = varY(b);
            var z = varZ(c);
            var name = $"((x={a}) {operation} (y={b},z={c}))";
            var r = tripleFunction(x, y, z);
            Assert.IsNotNull(r);
            Assert.AreEqual(name, r.Name);
            validate(expected, r.GetValue());
        }

        protected virtual void assert(T a, T b, object expected) {
            var x = varX(a);
            var y = varY(b);
            var name = $"((x={a}) {operation} (y={b}))";
            var z = function(x, y);
            Assert.IsNotNull(z);
            Assert.AreEqual(name, z.Name);
            validate(expected, z.GetValue());
        }
        protected virtual void assert(T a, object expected) {
            var x = varX(a);
            var name = $"({operation} (x={a}))";
            var z = unaryFunction(x);
            Assert.IsNotNull(z);
            Assert.AreEqual(name, z.Name);
            validate(expected, z.GetValue());
        }

        private static void validateByChars(object expected, object actual) {
            var a = actual?.ToString() ?? string.Empty;
            var e = expected?.ToString() ?? string.Empty;
            var count = a.Length < e.Length ? a.Length : e.Length;

            for (var i = 0; i < count; i++) {
                if (a[i] == e[i]) continue;
                if (i < count / 2)
                    Assert.AreEqual(expected, actual);
            }
        }

        protected void errorBinary() {
            var x = varX(GetRandom.ObjectOf<T>());
            var y = varY(GetRandom.ObjectOf<T>());
            var z = function(x, y);
            Assert.IsNotNull(z);
            Assert.IsInstanceOfType(z, typeof(RuleError));
        }
        protected void errorTriple() {
            var x = varX(GetRandom.ObjectOf<T>());
            var y = varY(GetRandom.ObjectOf<T>());
            var z = varZ(GetRandom.ObjectOf<T>());
            var r = tripleFunction(x, y, z);
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(RuleError));
        }
        protected void errorUnary() {
            var x = varX(GetRandom.ObjectOf<T>());
            var z = unaryFunction(x);
            Assert.IsNotNull(z);
            Assert.IsInstanceOfType(z, typeof(RuleError));
        }

        [TestMethod]
        public void AndTest() {
            function = (x, y) => x.And(y);
            operation = "&";

            andTest();
        }

        protected virtual void andTest() => errorBinary();


        [TestMethod]
        public void OrTest() {
            function = (x, y) => x.Or(y);
            operation = "|";

            orTest();
        }
        protected virtual void orTest() => errorBinary();

        [TestMethod]
        public void XorTest() {
            function = (x, y) => x.Xor(y);
            operation = "^";

            xorTest();
        }
        protected virtual void xorTest() => errorBinary();

        [TestMethod]
        public void NotTest() {
            unaryFunction = (x) => x.Not();
            operation = "!";

            notTest();
        }
        protected virtual void notTest() => errorUnary();

        [TestMethod]
        public void IsEqualTest() {
            function = (x, y) => x.IsEqual(y);
            operation = "==";

            isEqualTest();
        }
        protected virtual void isEqualTest() => errorBinary();

        [TestMethod]
        public void IsGreaterTest() {
            function = (x, y) => x.IsGreater(y);
            operation = ">";

            isGreaterTest();
        }
        protected virtual void isGreaterTest() => errorBinary();

        [TestMethod]
        public void IsLessTest() {
            function = (x, y) => x.IsLess(y);
            operation = "<";

            isLessTest();
        }
        protected virtual void isLessTest() => errorBinary();

        [TestMethod]
        public void AddTest() {
            function = (x, y) => x.Add(y);
            operation = "+";

            addTest();
        }
        protected virtual void addTest() => errorBinary();

        [TestMethod]
        public void SubtractTest() {
            function = (x, y) => x.Subtract(y);
            operation = "-";

            subtractTest();
        }
        protected virtual void subtractTest() => errorBinary();

        [TestMethod]
        public void MultiplyTest() {
            function = (x, y) => x.Multiply(y);
            operation = "*";

            multiplyTest();
        }
        protected virtual void multiplyTest() => errorBinary();

        [TestMethod]
        public void DivideTest() {
            function = (x, y) => x.Divide(y);
            operation = ":";

            divideTest();
        }
        protected virtual void divideTest() => errorBinary();

        [TestMethod]
        public void PowerTest() {
            function = (x, y) => x.Power(y);
            operation = "^";

            powerTest();
        }
        protected virtual void powerTest() => errorBinary();

        [TestMethod]
        public void InverseTest() {
            unaryFunction = (x) => x.Inverse();
            operation = "¹/ₓ";

            inverseTest();
        }
        protected virtual void inverseTest() => errorUnary();

        [TestMethod]
        public void OppositeTest() {
            unaryFunction = (x) => x.Opposite();
            operation = "-";

            oppositeTest();
        }
        protected virtual void oppositeTest() => errorUnary();

        [TestMethod]
        public void SquareTest() {
            unaryFunction = (x) => x.Square();
            operation = "²";

            squareTest();
        }
        protected virtual void squareTest() => errorUnary();

        [TestMethod]
        public void SqrtTest() {
            unaryFunction = (x) => x.Sqrt();
            operation = "√";

            sqrtTest();
        }
        protected virtual void sqrtTest() => errorUnary();

        [TestMethod]
        public void GetSecondTest() {
            unaryFunction = (x) => x.GetSecond();
            operation = "s";

            getSecondTest();
        }
        protected virtual void getSecondTest() => errorUnary();

        [TestMethod]
        public void GetMinuteTest() {
            unaryFunction = (x) => x.GetMinute();
            operation = "m";

            getMinuteTest();
        }
        protected virtual void getMinuteTest() => errorUnary();

        [TestMethod]
        public void GetHourTest() {
            unaryFunction = (x) => x.GetHour();
            operation = "h";

            getHourTest();
        }
        protected virtual void getHourTest() => errorUnary();

        [TestMethod]
        public void GetDayTest() {
            unaryFunction = (x) => x.GetDay();
            operation = "d";

            getDayTest();
        }
        protected virtual void getDayTest() => errorUnary();

        [TestMethod]
        public void GetMonthTest() {
            unaryFunction = (x) => x.GetMonth();
            operation = "M";

            getMonthTest();
        }
        protected virtual void getMonthTest() => errorUnary();

        [TestMethod]
        public void GetYearTest() {
            unaryFunction = (x) => x.GetYear();
            operation = "y";

            getYearTest();
        }
        protected virtual void getYearTest() => errorUnary();

        [TestMethod]
        public void GetIntervalTest() {
            function = (x, y) => x.GetInterval(y);
            operation = "←→";

            intervalTest();
        }
        protected virtual void intervalTest() => errorBinary();

        [TestMethod]
        public void GetAgeTest() {
            unaryFunction = x => x.GetAge();
            operation = "a";

            getAgeTest();
        }
        protected virtual void getAgeTest() => errorUnary();

        [TestMethod]
        public void ToSecondsTest() {
            unaryFunction = x => x.ToSeconds();
            operation = "→s";

            toSecondsTest();
        }
        protected virtual void toSecondsTest() => errorUnary();

        [TestMethod]
        public void ToMinutesTest() {
            unaryFunction = x => x.ToMinutes();
            operation = "→m";

            toMinutesTest();
        }
        protected virtual void toMinutesTest() => errorUnary();

        [TestMethod]
        public void ToHoursTest() {
            unaryFunction = x => x.ToHours();
            operation = "→h";

            toHoursTest();
        }
        protected virtual void toHoursTest() => errorUnary();

        [TestMethod]
        public void ToDaysTest() {
            unaryFunction = x => x.ToDays();
            operation = "→d";

            toDaysTest();
        }
        protected virtual void toDaysTest() => errorUnary();

        [TestMethod]
        public void ToMonthsTest() {
            unaryFunction = x => x.ToMonths();
            operation = "→M";

            toMonthsTest();
        }
        protected virtual void toMonthsTest() => errorUnary();


        [TestMethod]
        public void ToYearsTest() {
            unaryFunction = x => x.ToYears();
            operation = "→y";

            toYearsTest();
        }
        protected virtual void toYearsTest() => errorUnary();


        [TestMethod]
        public void AddSecondsTest() {
            function = (x, y) => x.AddSeconds(y);
            operation = "+s";

            addSecondsTest();
        }
        protected virtual void addSecondsTest() => errorBinary();

        [TestMethod]
        public void AddMinutesTest() {
            function = (x, y) => x.AddMinutes(y);
            operation = "+m";

            addMinutesTest();
        }
        protected virtual void addMinutesTest() => errorBinary();

        [TestMethod]
        public void AddHoursTest() {
            function = (x, y) => x.AddHours(y);
            operation = "+h";

            addHoursTest();
        }
        protected virtual void addHoursTest() => errorBinary();

        [TestMethod]
        public void AddDaysTest() {
            function = (x, y) => x.AddDays(y);
            operation = "+d";

            addDaysTest();
        }
        protected virtual void addDaysTest() => errorBinary();

        [TestMethod]
        public void AddMonthsTest() {
            function = (x, y) => x.AddMonths(y);
            operation = "+M";

            addMonthsTest();
        }
        protected virtual void addMonthsTest() => errorBinary();

        [TestMethod]
        public void AddYearsTest() {
            function = (x, y) => x.AddYears(y);
            operation = "+y";

            addYearsTest();
        }
        protected virtual void addYearsTest() => errorBinary();

        [TestMethod]
        public void GetLengthTest() {
            unaryFunction = x => x.GetLength();
            operation = "←→";

            getLengthTest();
        }
        protected virtual void getLengthTest() => errorUnary();


        [TestMethod]
        public void ToUpperTest() {
            unaryFunction = x => x.ToUpper();
            operation = "↑";

            toUpperTest();
        }
        protected virtual void toUpperTest() => errorUnary();


        [TestMethod]
        public void ToLowerTest() {
            unaryFunction = x => x.ToLower();
            operation = "↓";

            toLowerTest();
        }
        protected virtual void toLowerTest() => errorUnary();


        [TestMethod]
        public void TrimTest() {
            unaryFunction = x => x.Trim();
            operation = "→←";

            trimTest();
        }
        protected virtual void trimTest() => errorUnary();


        [TestMethod]
        public void SubstringToEndTest() {
            function = (x, y) => x.Substring(y);
            operation = "→←";

            substringToEndTest();
        }
        protected virtual void substringToEndTest() => errorBinary();

        [TestMethod]
        public void SubstringTest() {
            tripleFunction = (x, y, z) => x.Substring(y, z);
            operation = "→←";

            substringTest();
        }
        protected virtual void substringTest() => errorTriple();

        [TestMethod]
        public void ContainsTest() {
            function = (x, y) => x.Contains(y);
            operation = "←→";

            containsTest();
        }
        protected virtual void containsTest() => errorBinary();

        [TestMethod]
        public void EndsWithTest() {
            function = (x, y) => x.EndsWith(y);
            operation = "→";

            endsWithTest();
        }
        protected virtual void endsWithTest() => errorBinary();

        [TestMethod]
        public void StartsWithTest() {
            function = (x, y) => x.StartsWith(y);
            operation = "←";

            startsWithTest();
        }
        protected virtual void startsWithTest() => errorBinary();

    }

}