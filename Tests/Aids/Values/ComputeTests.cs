using System;
using System.Linq.Expressions;
using Abc.Aids.Constants;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Aids.Values;
using Abc.Domain.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doubles = Abc.Aids.Extensions.Doubles;

namespace Abc.Tests.Aids.Values {

    [TestClass]
    public class ComputeTests :TestsBase {

        private object o1;
        private object o2;
        private DateTime dt1;
        private DateTime dt2;
        private object do1;
        private object do2;
        private object i1;
        private object i2;
        private string s1;
        private string s2;
        private bool b1;
        private bool b2;
        private decimal de1;
        private decimal de2;
        private TimeSpan ts1;
        private TimeSpan ts2;
        [TestInitialize] public void TestInitialize() {
            type = typeof(Compute);
            o1 = GetRandom.AnyValue();
            o2 = GetRandom.AnyValue();
            while (o1.ToString() == o2.ToString()) o2 = GetRandom.AnyValue();
            dt1 = GetRandom.DateTime(DateTime.Now.AddYears(-100), DateTime.Now).AddYears(100);
            dt2 = GetRandom.DateTime(dt1, dt1.AddYears(100));
            ts1 = GetRandom.TimeSpan();
            ts2 = GetRandom.TimeSpan();
            do1 = GetRandom.Double(-1000000, 1000000);
            do2 = GetRandom.Double(-1000000, 1000000);
            i1 = rndInt;
            i2 = rndInt;
            s1 = rndStr;
            s2 = rndStr;
            b1 = rndBool;
            b2 = rndBool;
            de1 = GetRandom.Decimal(-1000000, 1000000);
            de2 = GetRandom.Decimal(-1000000, 1000000);
        }
        [TestMethod] public void AddDaysTest() {
            isNull(Compute.AddDays(o1, o1));
            do1 = GetRandom.Double(-10000, 10000);
            var result = Compute.AddDays(dt1, do1);
            isNotNull(result);
            var d = Doubles.ToDouble(do1);
            areEqual(result, dt1.AddDaysSafe(d));
            areEqual(result, dt1.AddDays(d));
        }
        [TestMethod] public void AddTest() {
            AddBooleansTests();
            AddStringsTests();
            AddDatesTests();
            AddDoublesTests();
            AddDecimalsTests();
            AddObjectsTests();
        }
        [TestMethod] public void AddStringsTests() {
            static void test(string sum, string x, string y) {
                areEqual(Compute.Add(x, y), Strings.Add(x, y));
                areEqual(sum, Strings.Add(x, y));
            }
            test(string.Empty, string.Empty, string.Empty);
            test("AB", "A", "B");
            test("   ", " ", "  ");
            test(s1 + s2, s1, s2);
        }
        [TestMethod] public void AddDatesTests() => areEqual(Compute.Add(dt1, dt2), dt1.AddSafe(dt2));
        [TestMethod] public void AddBooleansTests() => areEqual(Compute.Add(b1, b2), b1.Add(b2));
        [TestMethod] public void AddDoublesTests() {
            var d1 = Doubles.ToDouble(do1);
            var d2 = Doubles.ToDouble(do2);
            var r = Compute.Add(do1, do2);
            areEqual(r, d1.Add(d2));
            areEqual(r, d1 + d2);
        }
        [TestMethod] public void AddDecimalsTests() {
            var r = Compute.Add(de1, de2);
            areEqual(r, de1.Add(de2));
            areEqual(r, de1 + de2);
        }
        [TestMethod] public void AddObjectsTests() {
            var r = Compute.Add(o1, new object());
            isNull(r);
        }
        [TestMethod] public void AndTest() {
            testAllNotBothBooleanIsNull(Compute.And);
            areEqual(Compute.And(b1, b2), b1.And(b2));
            areEqual(true, Compute.And(true, true));
            areEqual(false, Compute.And(false, true));
            areEqual(false, Compute.And(true, false));
            areEqual(false, Compute.And(false, false));
        }
        private void testAllNotBooleanIsNull(Func<object, object> function) {
            isNull(function(do1));
            isNull(function(dt1));
            isNull(function(ts1));
            isNull(function(null));
            isNull(function(i1));
            while (TypeIs.Bool(o1)) o1 = GetRandom.AnyValue();
            isNull(function(o1));
            isNotNull(function(b1));
        }
        private void testAnyOtherIsNull(Func<object, object, object> function, object o) {
            isNull(function(do1, o));
            isNull(function(dt1, o));
            isNull(function(ts1, o));
            isNull(function(null, o));
            isNull(function(i1, o));
            isNull(function(o1, o));
            isNull(function(b1, o));
            isNull(function(o, do1));
            isNull(function(o, dt1));
            isNull(function(o, ts1));
            isNull(function(o, null));
            isNull(function(o, i1));
            isNull(function(o, o1));
            isNull(function(o, b1));
        }
        private static void testAnyIsNull(Func<object, object> function, object o) => Assert.IsNull(function(o));
        private void testAllNotBothBooleanIsNull(Func<object, object, object> function) {
            isNull(function(do1, do2));
            isNull(function(dt1, dt2));
            isNull(function(ts1, ts2));
            isNull(function(null, null));
            isNull(function(i1, i2));
            isNull(function(do1, b2));
            isNull(function(dt1, b2));
            isNull(function(ts1, b2));
            isNull(function(null, null));
            isNull(function(i1, b2));
            while (TypeIs.Bool(o1)) o1 = GetRandom.AnyValue();
            isNull(function(o1, o2));
            isNull(function(o1, b2));
            isNotNull(function(b1, b2));
        }
        [TestMethod] public void MultiplyTest() {
            testAnyOtherIsNull(Compute.Multiply, dt1);
            testAnyOtherIsNull(Compute.Multiply, s1);
            areEqual(10.0, Compute.Multiply(5.0, 2));
            areEqual(10M, Compute.Multiply(5M, 2));
            var d1 = Doubles.ToDouble(do1);
            var d2 = Doubles.ToDouble(do2);
            var r = Compute.Multiply(do1, do2);
            areEqual(r, d1.Multiply(d2));
            areEqual(r, d1 * d2);
            var m1 = ConvertTo.Decimal(de1);
            var m2 = ConvertTo.Decimal(de2);
            r = Compute.Multiply(de1, de2);
            areEqual(r, m1.Multiply(m2));
            areEqual(r, m1 * m2);
            areEqual(true, Compute.Multiply(true, true));
            areEqual(false, Compute.Multiply(false, false));
            areEqual(false, Compute.Multiply(true, false));
            areEqual(false, Compute.Multiply(false, true));
        }
        [TestMethod] public void SubtractTest() {
            testAnyOtherIsNull(Compute.Subtract, b1);
            testAnyOtherIsNull(Compute.Subtract, s1);
            areEqual(3.0, Compute.Subtract(5.0, 2));
            areEqual(3M, Compute.Subtract(5M, 2));
            var d1 = Doubles.ToDouble(do1);
            var d2 = Doubles.ToDouble(do2);
            var r = Compute.Subtract(do1, do2);
            areEqual(r, d1.Subtract(d2));
            areEqual(r, d1 - d2);
            var m1 = ConvertTo.Decimal(de1);
            var m2 = ConvertTo.Decimal(de2);
            r = Compute.Subtract(de1, de2);
            areEqual(r, m1.Subtract(m2));
            areEqual(r, m1 - m2);
            r = Compute.Subtract(dt2, dt1);
            areEqual(r, dt2.SubtractSafe(dt1));
            areEqual(r, dt2.Subtract(TimeSpan.FromTicks(dt1.Ticks)));
        }
        [TestMethod] public void OrTest() {
            testAllNotBothBooleanIsNull(Compute.Or);
            areEqual(Compute.Or(b1, b2), b1.Or(b2));
            areEqual(true, Compute.Or(true, true));
            areEqual(true, Compute.Or(false, true));
            areEqual(true, Compute.Or(true, false));
            areEqual(false, Compute.Or(false, false));
        }
        [TestMethod] public void XorTest() {
            testAllNotBothBooleanIsNull(Compute.Xor);
            areEqual(Compute.Xor(b1, b2), b1.Xor(b2));
            areEqual(false, Compute.Xor(true, true));
            areEqual(true, Compute.Xor(false, true));
            areEqual(true, Compute.Xor(true, false));
            areEqual(false, Compute.Xor(false, false));
        }
        [TestMethod] public void NotTest() {
            testAllNotBooleanIsNull(Compute.Not);
            areEqual(Compute.Not(b1), b1.Not());
            areEqual(false, Compute.Not(true));
            areEqual(true, Compute.Not(false));
        }
        [TestMethod] public void IsEqualTest() {
            areEqual(Compute.IsEqual(dt1, dt2), dt1.IsEqual(dt2));
            areEqual(Compute.IsEqual(do1, do2), Doubles.ToDouble(do1).IsEqual(Doubles.ToDouble(do2)));
            areEqual(Compute.IsEqual(de1, de2), de1.IsEqual(de2));
            isNull(Compute.IsEqual(b1, s1));
        }
        [TestMethod] public void IsGreaterTest() {
            areEqual(Compute.IsGreater(b1, b2), b1.IsGreater(b2));
            areEqual(Compute.IsGreater(s1, s2), s1.IsGreater(s2));
            areEqual(Compute.IsGreater(dt1, dt2), dt1.IsGreater(dt2));
            areEqual(Compute.IsGreater(do1, do2), Doubles.ToDouble(do1).IsGreater(Doubles.ToDouble(do2)));
            areEqual(Compute.IsGreater(de1, de2), de1.IsGreater(de2));
            isNull(Compute.IsGreater(b1, s1));
            isNull(Compute.IsGreater(s1, b1));
            isNull(Compute.IsGreater(dt1, s1));
            isNull(Compute.IsGreater(do1, s1));
            isNull(Compute.IsGreater(de1, s1));
        }
        [TestMethod] public void IsLessTest() {
            areEqual(Compute.IsLess(b1, b2), b1.IsLess(b2));
            areEqual(Compute.IsLess(s1, s2), s1.IsLess(s2));
            areEqual(Compute.IsLess(dt1, dt2), dt1.IsLess(dt2));
            areEqual(Compute.IsLess(do1, do2), Doubles.ToDouble(do1).IsLess(Doubles.ToDouble(do2)));
            areEqual(Compute.IsLess(de1, de2), de1.IsLess(de2));
            isNull(Compute.IsLess(b1, s1));
            isNull(Compute.IsLess(s1, b1));
            isNull(Compute.IsLess(dt1, s1));
            isNull(Compute.IsLess(do1, s1));
            isNull(Compute.IsLess(de1, s1));
        }
        [TestMethod] public void GetSecondTest() {
            Assert.IsNull(Compute.GetSecond(do1));
            Assert.IsNull(Compute.GetSecond(s1));
            Assert.IsNull(Compute.GetSecond(b1));
            var result = Compute.GetSecond(dt1);
            Assert.IsNotNull(result);
            Assert.AreEqual(result, dt1.GetSecond());
            Assert.AreEqual(result, dt1.Second);
            result = Compute.GetSecond(ts1);
            Assert.AreEqual(result, ts1.Seconds);
        }
        [TestMethod] public void GetMinuteTest() {
            isNull(Compute.GetMinute(do1));
            isNull(Compute.GetMinute(s1));
            isNull(Compute.GetMinute(b1));
            var result = Compute.GetMinute(dt1);
            isNotNull(result);
            areEqual(result, dt1.GetMinute());
            areEqual(result, dt1.Minute);
            result = Compute.GetMinute(ts1);
            areEqual(result, ts1.Minutes);
        }
        [TestMethod] public void GetHourTest() {
            isNull(Compute.GetHour(do1));
            isNull(Compute.GetHour(s1));
            isNull(Compute.GetHour(b1));
            var result = Compute.GetHour(dt1);
            isNotNull(result);
            areEqual(result, dt1.GetHour());
            areEqual(result, dt1.Hour);
            result = Compute.GetHour(ts1);
            areEqual(result, ts1.Hours);
        }
        [TestMethod] public void GetDayTest() {
            isNull(Compute.GetDay(do1));
            isNull(Compute.GetDay(s1));
            isNull(Compute.GetDay(b1));
            var result = Compute.GetDay(dt1);
            isNotNull(result);
            areEqual(result, dt1.GetDay());
            areEqual(result, dt1.Day);
            result = Compute.GetDay(ts1);
            areEqual(result, ts1.Days);
        }
        [TestMethod] public void GetMonthTest() {
            isNull(Compute.GetMonth(do1));
            isNull(Compute.GetMonth(s1));
            isNull(Compute.GetMonth(b1));
            var result = Compute.GetMonth(dt1);
            isNotNull(result);
            areEqual(result, dt1.GetMonth());
            areEqual(result, dt1.Month);
        }
        [TestMethod] public void GetYearTest() {
            isNull(Compute.GetYear(do1));
            isNull(Compute.GetYear(s1));
            isNull(Compute.GetYear(b1));
            var result = Compute.GetYear(dt1);
            isNotNull(result);
            areEqual(result, dt1.GetYear());
            areEqual(result, dt1.Year);
        }
        [TestMethod] public void AddSecondsTest() {
            isNull(Compute.AddSeconds(o1, o1));
            var result = Compute.AddSeconds(dt1, do1);
            isNotNull(result);
            var d = Doubles.ToDouble(do1);
            areEqual(result, dt1.AddSecondsSafe(d));
            areEqual(result, dt1.AddSeconds(d));
        }
        [TestMethod] public void AddMinutesTest() {
            isNull(Compute.AddMinutes(o1, o1));
            var result = Compute.AddMinutes(dt1, do1);
            isNotNull(result);
            var d = Doubles.ToDouble(do1);
            areEqual(result, dt1.AddMinutesSafe(d));
            areEqual(result, dt1.AddMinutes(d));
        }
        [TestMethod] public void AddHoursTest() {
            isNull(Compute.AddHours(o1, o1));
            var result = Compute.AddHours(dt1, i1);
            isNotNull(result);
            var d = Doubles.ToDouble(i1);
            areEqual(result, dt1.AddHoursSafe(d));
            areEqual(result, dt1.AddHours(d));
        }
        [TestMethod] public void AddMonthsTest() {
            isNull(Compute.AddMonths(o1, o1));
            var result = Compute.AddMonths(dt1, i1);
            isNotNull(result);
            var i = Integers.ToInteger(i1);
            areEqual(result, dt1.AddMonthsSafe(i));
            areEqual(result, dt1.AddMonths(i));
        }
        [TestMethod] public void AddYearsTest() {
            isNull(Compute.AddYears(o1, o1));
            var result = Compute.AddYears(dt1, i1);
            isNotNull(result);
            var i = Integers.ToInteger(i1);
            areEqual(result, dt1.AddYearsSafe(i));
            areEqual(result, dt1.AddYears(i));
        }
        [TestMethod] public void GetIntervalTest() {
            isNull(Compute.GetInterval(s1, dt1));
            isNull(Compute.GetInterval(b1, dt1));
            isNull(Compute.GetInterval(do1, dt1));
            isNull(Compute.GetInterval(null, dt1));
            var result = dt1.GetInterval(dt2).Ticks;
            areEqual(result, Compute.GetInterval(dt1, dt2));
            areEqual(result, dt1.Subtract(dt2).Ticks);
        }
        [TestMethod] public void GetAgeTest() {
            isNull(Compute.GetAge(s1));
            isNull(Compute.GetAge(b1));
            isNull(Compute.GetAge(do1));
            isNull(Compute.GetAge(null));
            var age = GetRandom.Int32(10, 100);
            areEqual(age, Compute.GetAge(DateTime.Now.AddYears(-age)));
        }
        [TestMethod] public void ToSecondsTest() => testToTotalTime(Compute.ToSeconds, ts1.TotalSeconds);
        [TestMethod] public void ToMinutesTest() => testToTotalTime(Compute.ToMinutes, ts1.TotalMinutes);
        [TestMethod] public void ToHoursTest() => testToTotalTime(Compute.ToHours, ts1.TotalHours);
        [TestMethod] public void ToDaysTest() => testToTotalTime(Compute.ToDays, ts1.TotalDays);
        [TestMethod] public void ToMonthsTest() => testToTotalTime(Compute.ToMonths, ts1.TotalDays / Astronomical.DaysPerMonth);
        [TestMethod] public void ToYearsTest() => testToTotalTime(Compute.ToYears, ts1.TotalDays / Astronomical.DaysPerYear);
        private void testToTotalTime(Func<object, object> f, double expected) {
            isNull(f(s1));
            isNull(f(b1));
            isNull(f(do1));
            isNull(f(null));
            areEqual(expected, f(ts1));
        }
        [TestMethod] public void GetLengthTest() {
            testAllNotStringIsNull(Compute.GetLength);
            areEqual(s1.Length, Compute.GetLength(s1));
        }
        [TestMethod] public void ToUpperTest() {
            testAllNotStringIsNull(Compute.ToUpper);
            areEqual(s1.ToUpper(), Compute.ToUpper(s1));
        }
        [TestMethod] public void ToLowerTest() {
            testAllNotStringIsNull(Compute.ToLower);
            areEqual(s1.ToLower(), Compute.ToLower(s1));
        }
        private void testAllNotStringIsNull(Func<object, object> f) {
            isNull(f(b1));
            isNull(f(do1));
            isNull(f(dt1));
            isNull(f(ts1));
            isNull(f(null));
            isNull(f(i1));
            while (TypeIs.String(o1)) o1 = GetRandom.AnyValue();
            isNull(f(o1));
            isNotNull(f(s2));
        }
        [TestMethod] public void TrimTest() {
            testAllNotStringIsNull(Compute.Trim);
            areEqual(s1, Compute.Trim("   " + s1 + "         "));
        }
        [TestMethod] public void SubstringTest() {
            isNull(Compute.Substring(i1, i2));
            isNull(Compute.Substring(do1, do2));
            isNull(Compute.Substring(null, null));
            isNull(Compute.Substring(string.Empty, string.Empty));
            var length = (byte)((s1.Length - 1) / 2);
            var len = GetRandom.UInt8(1, length);
            var idx = GetRandom.UInt8(len, length);
            areEqual(s1.Substring(idx), Compute.Substring(s1, idx));
            areEqual(s1.Substring(idx, len), Compute.Substring(s1, idx, len));
        }
        [TestMethod] public void ContainsTest() {
            isNull(Compute.Contains(i1, i2));
            isNull(Compute.Contains(do1, do2));
            isNull(Compute.Contains(null, null));
            areEqual(false, Compute.Contains(string.Empty, string.Empty));
            var length = (byte)((s1.Length - 1) / 2);
            var len = GetRandom.UInt8(1, length);
            var idx = GetRandom.UInt8(len, length);
            var x = s1.Substring(idx, len);
            areEqual(true, Compute.Contains(s1, x), $"string:{s1} substring:{x} idx:{idx} len:{len}");
        }
        [TestMethod] public void EndsWithTest() {
            isNull(Compute.EndsWith(i1, i2));
            isNull(Compute.EndsWith(do1, do2));
            isNull(Compute.EndsWith(null, null));
            areEqual(false, Compute.EndsWith(string.Empty, string.Empty));
            var length = (byte)((s1.Length - 1) / 2);
            var len = GetRandom.UInt8(1, length);
            var idx = GetRandom.UInt8(len, length);
            areEqual(true, Compute.EndsWith(s1, s1.Substring(idx)));
        }
        [TestMethod] public void StartsWithTest() {
            isNull(Compute.StartsWith(11, 12));
            isNull(Compute.StartsWith(do1, do2));
            isNull(Compute.StartsWith(null, null));
            areEqual(false, Compute.StartsWith(string.Empty, string.Empty));
            var length = (byte)((s1.Length - 1) / 2);
            var len = GetRandom.UInt8(length, (byte)s1.Length);
            areEqual(true, Compute.StartsWith(s1, s1.Substring(0, len)));
        }
        [TestMethod] public void DivideTest() {
            isNull(Compute.Divide(s1, s2));
            isNull(Compute.Divide(dt1, dt2));
            isNull(Compute.Divide(b1, b2));
            areEqual(5.0M, Compute.Divide(10, 2));
            areEqual(5M, Compute.Divide(10M, 2));
            var d1 = Doubles.ToDouble(do1);
            var d2 = Doubles.ToDouble(do2);
            areEqual(d1 / d2, Compute.Divide(do1, do2));
        }
        [TestMethod] public void PowerTest() {
            testAnyOtherIsNull(Compute.Power, b1);
            testAnyOtherIsNull(Compute.Power, dt1);
            testAnyOtherIsNull(Compute.Power, s1);
            areEqual(25.0, Compute.Power(5.0, 2.0));
            var d1 = Doubles.ToDouble(do1);
            var d2 = Doubles.ToDouble(do2);
            areEqual(Math.Pow(d1, d2), Compute.Power(do1, do2));
            d1 = Doubles.ToDouble(de1);
            d2 = Doubles.ToDouble(de2);
            areEqual(Math.Pow(d1, d2), Compute.Power(de1, de2));
        }
        [TestMethod] public void OppositeTest() {
            testAnyIsNull(Compute.Opposite, b1);
            testAnyIsNull(Compute.Opposite, dt1);
            testAnyIsNull(Compute.Opposite, s1);
            areEqual(-5.0, Compute.Opposite(5.0));
            areEqual(-5M, Compute.Opposite(5M));
            areEqual(-Doubles.ToDouble(do1), Compute.Opposite(do1));
            areEqual(-ConvertTo.Decimal(de1), Compute.Opposite(de1));
        }
        [TestMethod] public void InverseTest() {
            testAnyIsNull(Compute.Inverse, b1);
            testAnyIsNull(Compute.Inverse, dt1);
            testAnyIsNull(Compute.Inverse, s1);
            areEqual(1.0 / 5.0, Compute.Inverse(5.0));
            areEqual(1M / 5M, Compute.Inverse(5M));
            areEqual(1.0 / Doubles.ToDouble(do1), Compute.Inverse(do1));
            areEqual(1M / ConvertTo.Decimal(de1), Compute.Inverse(de1));
        }
        [TestMethod] public void SquareTest() {
            testAnyIsNull(Compute.Square, b1);
            testAnyIsNull(Compute.Square, dt1);
            testAnyIsNull(Compute.Square, s1);
            areEqual(25.0, Compute.Square(5.0));
            areEqual(25D, Compute.Square(5M));
            var d = Doubles.ToDouble(do1);
            var m = Doubles.ToDouble(de1);
            areEqual(d * d, Compute.Square(do1));
            areEqual(m * m, Compute.Square(de1));
        }
        [TestMethod] public void SqrtTest() {
            testAnyIsNull(Compute.Sqrt, b1);
            testAnyIsNull(Compute.Sqrt, dt1);
            testAnyIsNull(Compute.Sqrt, s1);
            areEqual(5.0, Compute.Sqrt(25.0));
            var d = Doubles.ToDouble(do1);
            areEqual(Math.Sqrt(d), Compute.Sqrt(do1));
            d = Doubles.ToDouble(de1);
            areEqual(Math.Sqrt(d), Compute.Sqrt(de1));
        }

        [DataRow("AAA", "A", "Contains", true)]
        [DataRow("AAA", "B", "Contains", false)]
        [DataRow("BBB", "A", "Contains", false)]
        [DataRow("BBB", "B", "Contains", true)]
        [DataRow("ABC", "A", "StartsWith", true)]
        [DataRow("ABC", "B", "StartsWith", false)]
        [DataRow("ABC", "C", "EndsWith", true)]
        [DataRow("ABC", "B", "EndsWith", false)]
        [TestMethod] public void ToExpressionTest(string x, string y, string operation, bool expected) {
            var s = random<string>();
            var v = ValueFactory.Create(s, nameof(ExprTests.testClass.String));
            var vy = ValueFactory.Create(s, y);
            var param = Expression.Parameter(typeof(ExprTests.testClass), "x");
            var expression = Compute.ToExpression(v, vy, operation, param) as MethodCallExpression;
            isNotNull(expression);
            var o = new ExprTests.testClass() { String = x };
            var actual = expression.Method.Invoke(o.String, new[] { y });
            areEqual(actual, expected);
            actual = expression.Method.Invoke(x, new[] { y });
            areEqual(actual, expected);
        }
        [TestMethod] public void DummyTest() => isNull(Compute.Dummy());
    }
}
