using System;
using System.Linq.Expressions;
using Abc.Aids.Calculator;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Aids.Values;
using Abc.Tests.Aids.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Calculator {

    [TestClass] public class CalculateTests :BaseClassTests<Calculate, object> {
        protected override Calculate createObject() => new ();
        [TestMethod] public void GetTest() { }
        [TestMethod] public void SetTest() { }
        [TestMethod] public void PeekTest() {
            Assert.IsNull(obj.Peek());
            var s = rndStr;
            obj.Set(s);
            Assert.AreEqual(s, obj.Peek());
            Assert.AreEqual(s, obj.Get());
            Assert.IsNull(obj.Peek());
        }
        [TestMethod] public void AddTest() {
            addTest(false);
        }
        private void addTest(bool perform) {
            void test(object x, object y, object z) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(Operation.Add);
                else obj.Add();
                Assert.AreEqual(z, obj.Peek());
            }
            test("A", "B", "AB");
            test(true, false, true);
            test(false, false, false);
            test(new DateTime(2000, 1, 31), new DateTime(1, 1, 2), new DateTime(2000, 2, 1));
            test(new DateTime(2000, 1, 31), new DateTime(2, 1, 1), new DateTime(2001, 1, 30));
            test(new DateTime(2000, 1, 31), new DateTime(1, 2, 1), new DateTime(2000, 3, 2));
            test((long)2, (long)5, 7.0M);
            test(2.0, 5.0, 7.0);
            test(2, 5.0, 7.0);
        }
        [TestMethod] public void SubtractTest() => subtractTest(false);
        private void subtractTest(bool perform) {
            void test(object x, object y, object z) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(Operation.Subtract);
                else obj.Subtract();
                Assert.AreEqual(z, obj.Peek());
            }

            test(2, 5.0, -3.0);
            test(rndStr, rndStr, null);
            test(rndBool, rndBool, null);
            test(new DateTime(2000, 1, 31), new DateTime(1, 1, 2), new DateTime(2000, 1, 30));
        }
        [TestMethod] public void PerformTest() {
            dummyTest(true);
            addTest(true);
            subtractTest(true);
            multiplyTest(true);
            divideTest(true);

            powerTest(true);
            inverseTest(true);
            oppositeTest(true);
            squareTest(true);
            sqrtTest(true);

            andTest(true);
            orTest(true);
            xorTest(true);
            notTest(true);
            equalTest(true);
            greaterTest(true);
            lessTest(true);

            getSecondTest(true);
            getMinuteTest(true);
            getHourTest(true);
            getDayTest(true);
            getMonthTest(true);
            getYearTest(true);

            getIntervalTest(true);
            getAgeTest(true);
            toSecondsTest(true);
            toMinutesTest(true);
            toHoursTest(true);
            toDaysTest(true);
            toMonthsTest(true);
            toYearsTest(true);

            addSecondsTest(true);
            addMinutesTest(true);
            addHoursTest(true);
            addDaysTest(true);
            addMonthsTest(true);
            addYearsTest(true);

            lengthTest(true);
            substringTest(true);
            containsTest(true);
            endsWithTest(true);
            startsWithTest(true);
            toUpperTest(true);
            toLowerTest(true);
            trimTest(true);
        }
        [TestMethod] public void ToYearsTest() => toYearsTest(false);
        [TestMethod] public void ToMonthsTest() => toMonthsTest(false);
        [TestMethod] public void ToDaysTest() => toDaysTest(false);
        [TestMethod] public void ToHoursTest() => toHoursTest(false);
        [TestMethod] public void ToMinutesTest() => toMinutesTest(false);
        [TestMethod] public void ToSecondsTest() => toSecondsTest(false);
        private void toYearsTest(bool perform)
            => testConvertTimeSpanTo(
                () => obj.ToYears()
                , Operation.ToYears
                , (x) => x.Years()
                , perform);
        private void testConvertTimeSpanTo(Action a, Operation o, Func<TimeSpan, double> expected, bool perform) {
            void test(object x, object y) {
                obj.Set(x);
                if (perform) obj.Perform(o);
                else a();
                Assert.AreEqual(y, obj.Peek());
            }
            test(rndStr, null);
            test(rndBool, null);
            test(GetRandom.Double(), null);
            test(GetRandom.Int64(), null);
            var d = GetRandom.TimeSpan();
            test(d, expected(d));
        }
        private void toMonthsTest(bool perform) => testConvertTimeSpanTo(
            () => obj.ToMonths()
            , Operation.ToMonths
            , (x) => x.Months()
            , perform);
        private void toDaysTest(bool perform) => testConvertTimeSpanTo(
            () => obj.ToDays()
            , Operation.ToDays
            , (x) => x.Days()
            , perform);
        private void toHoursTest(bool perform) => testConvertTimeSpanTo(
            () => obj.ToHours()
            , Operation.ToHours
            , (x) => x.Hours()
            , perform);
        private void toMinutesTest(bool perform) => testConvertTimeSpanTo(
            () => obj.ToMinutes()
            , Operation.ToMinutes
            , (x) => x.Minutes()
            , perform);
        private void toSecondsTest(bool perform) => testConvertTimeSpanTo(
            () => obj.ToSeconds()
            , Operation.ToSeconds
            , (x) => x.Seconds()
            , perform);
        [TestMethod] public void SetVariableTest() {
            setVariableIsSafeTest(obj);
            setBooleanVariableTest(obj);
            setStringVariableTest(obj);
            setDateTimeVariableTest(obj);
            setDecimalVariableTest(obj);
            setDoubleVariableTest(obj);
            setIntegerVariableTest(obj);
        }
        private static void setVariableIsSafeTest(Calculate c) { c.Set(null); }
        private static void setBooleanVariableTest(Calculate c) {
            var b = rndBool;
            c.Set(b);
            Assert.AreEqual(b, c.Get());
        }
        private static void setStringVariableTest(Calculate c) {
            var s = rndStr;
            c.Set(s);
            Assert.AreEqual(s, c.Get());
        }
        private static void setIntegerVariableTest(Calculate c) {
            var i = GetRandom.Int32();
            c.Set(i);
            Assert.AreEqual(i, c.Get());
        }
        private static void setDoubleVariableTest(Calculate c) {
            var d = GetRandom.Double();
            c.Set(d);
            Assert.AreEqual(d, c.Get());
        }
        private static void setDecimalVariableTest(Calculate c) {
            var m = GetRandom.Decimal();
            c.Set(m);
            Assert.AreEqual(m, c.Get());
        }
        private static void setDateTimeVariableTest(Calculate c) {
            var t = rndDt;
            c.Set(t);
            Assert.AreEqual(t, c.Get());
        }
        [TestMethod] public void AddSecondsTest() => addSecondsTest(false);
        private void addSecondsTest(bool perform) => testAddToDateTime(
            () => obj.AddSeconds()
            , Operation.AddSeconds
            , (x, y) => x.AddSeconds(y),
            perform);
        [TestMethod] public void AddMinutesTest() => addMinutesTest(false);
        private void addMinutesTest(bool perform) => testAddToDateTime(
            () => obj.AddMinutes()
            , Operation.AddMinutes
            , (x, y) => x.AddMinutes(y),
            perform);
        [TestMethod] public void AddHoursTest() => addHoursTest(false);
        private void addHoursTest(bool perform) => testAddToDateTime(
            () => obj.AddHours()
            , Operation.AddHours
            , (x, y) => x.AddHours(y),
            perform);
        [TestMethod] public void AddDaysTest() => addDaysTest(false);
        private void addDaysTest(bool perform) => testAddToDateTime(
            () => obj.AddDays()
            , Operation.AddDays
            , (x, y) => x.AddDays(y),
            perform);
        [TestMethod] public void AddMonthsTest() => addMonthsTest(false);
        private void addMonthsTest(bool perform) => testAddToDateTime(
            () => obj.AddMonths()
            , Operation.AddMonths
            , (x, y) => x.AddMonths(y),
            perform);
        [TestMethod] public void AddYearsTest() => addYearsTest(false);
        private void addYearsTest(bool perform) => testAddToDateTime(
            () => obj.AddYears()
            , Operation.AddYears
            , (x, y) => x.AddYears(y),
            perform);
        private void testAddToDateTime(Action a, Operation o, Func<DateTime, int, DateTime> expected, bool perform) {
            void test(object x, object y, object z) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(o);
                else a();
                Assert.AreEqual(z, obj.Peek());
            }

            var d = GetRandom.Int32(-1000, 1000);
            test(rndStr, d, null);
            test(rndBool, d, null);
            test(GetRandom.Double(), d, null);
            test(GetRandom.Int64(), d, null);
            var dt = GetRandom.DateTime(DateTime.Now.AddYears(-100), DateTime.Now).AddYears(100);
            test(dt, d, expected(dt, d));
        }
        [TestMethod] public void GetAgeTest() => getAgeTest(false);
        private void getAgeTest(bool perform) {
            void test(object x, object y) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.GetAge);
                else obj.GetAge();
                Assert.AreEqual(y, obj.Peek());
            }

            test(rndStr, null);
            test(rndBool, null);
            test(GetRandom.Double(), null);
            test(GetRandom.Int64(), null);
            var d = rndDt;
            test(d, d.GetAge());
        }
        [TestMethod] public void AndTest() => andTest(false);
        private void andTest(bool perform) {
            void test(object x, object y, object z) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(Operation.And);
                else obj.And();
                Assert.AreEqual(z, obj.Peek());
            }

            test(GetRandom.Double(), GetRandom.Double(), null);
            test(rndStr, rndStr, null);
            test(true, true, true);
            test(true, false, false);
            test(false, true, false);
            test(false, false, false);
            test(rndDt, rndDt, null);
            test(GetRandom.Int64(), GetRandom.Int64(), null);
        }
        [TestMethod] public void ClearTest() {
            void test(object x, object y, object z) {
                obj.Set(x);
                obj.Set(y);
                obj.Clear();
                Assert.AreEqual(z, obj.Peek());
            }

            test(GetRandom.Double(), GetRandom.Double(), null);
            test(rndStr, rndStr, null);
            test(rndBool, rndBool, null);
            test(rndDt, rndDt, null);
            test(GetRandom.Int64(), GetRandom.Int64(), null);
        }
        [TestMethod] public void ContainsTest() => containsTest(false);
        private void containsTest(bool perform) {
            void test(object x, object y, object r) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(Operation.Contains);
                else obj.Contains();
                Assert.AreEqual(r, obj.Peek());
            }

            var s = GetRandom.String(10, 100);
            var idx = GetRandom.Int32(5, s.Length / 2);
            var len = GetRandom.Int32(5, s.Length / 2);
            var s1 = s.Substring(idx, len);
            test(s, s1, true);
            test(s, rndStr, false);
            test(s, rndDt, null);
            test(s, GetRandom.Double(), null);
        }
        [TestMethod] public void GetDayTest() => getDayTest(false);
        private void getDayTest(bool perform) {
            void test(object x, object y) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.GetDay);
                else obj.GetDay();
                Assert.AreEqual(y, obj.Peek());
            }

            test(rndStr, null);
            test(rndBool, null);
            test(GetRandom.Double(), null);
            test(GetRandom.Int64(), null);
            var d = rndDt;
            test(d, d.Day);
        }
        [TestMethod] public void DivideTest() => divideTest(false);
        private void divideTest(bool perform) {
            void test(object x, object y, object z) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(Operation.Divide);
                else obj.Divide();
                Assert.AreEqual(z, obj.Peek());
            }

            test(rndStr, rndStr, null);
            test(rndBool, rndBool, null);
            test(rndDt, rndDt, null);
            test(2.0, 5.0, 0.4);
            test(-2, 5.0, -0.4);
        }
        [TestMethod] public void DummyTest() => dummyTest(false);
        private void dummyTest(bool perform) {
            void test(object x) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.Dummy);
                else obj.Dummy();
                Assert.AreEqual(x, obj.Peek());
            }

            test(GetRandom.Double());
            test(rndStr);
            test(rndBool);
            test(rndDt);
            test(GetRandom.Int64());
        }
        [TestMethod] public void EndsWithTest() => endsWithTest(false);
        private void endsWithTest(bool perform) {
            void test(object x, object y, object r) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(Operation.EndsWith);
                else obj.EndsWith();
                Assert.AreEqual(r, obj.Peek());
            }

            var s = GetRandom.String(10, 100);
            var idx = GetRandom.Int32(0, s.Length / 2);
            var s1 = s.Substring(idx);
            test(s, s1, true);
            test(s, rndStr, false);
            test(s, rndDt, null);
            test(s, GetRandom.Double(), null);
        }
        [TestMethod] public void IsEqualTest() => equalTest(false);
        private void equalTest(bool perform) {
            void action(object x, object y, object z) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(Operation.IsEqual);
                else obj.IsEqual();
                Assert.AreEqual(z, obj.Peek());
            }

            void test(object x, object y) {
                action(x, x, true);
                action(x, y, false);
            }

            test(GetRandom.Double(), GetRandom.Double());
            test(rndStr, rndStr);
            test(true, false);
            test(rndDt, rndDt);
            test(GetRandom.Int64(), GetRandom.Int64());
        }
        [TestMethod] public void IsGreaterTest() => greaterTest(false);
        private void greaterTest(bool perform) {
            void action(object x, object y, object z) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(Operation.IsGreater);
                else obj.IsGreater();
                Assert.AreEqual(z, obj.Peek());
            }
            void test(object x, object y) {
                action(x, y, true);
                action(y, x, false);
                action(x, x, false);
            }

            test(GetRandom.Double(1), GetRandom.Double(max: 0));
            test(rndStr, "AAA" + rndStr);
            test(true, false);
            test(GetRandom.DateTime(DateTime.Now),
                GetRandom.DateTime(max: DateTime.Now.AddDays(-1)));
            test((long)GetRandom.Int32(1), (long)GetRandom.Int32(max: 0));
        }
        [TestMethod] public void GetHourTest() => getHourTest(false);
        private void getHourTest(bool perform) {
            void test(object x, object y) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.GetHour);
                else obj.GetHour();
                Assert.AreEqual(y, obj.Peek());
            }

            test(rndStr, null);
            test(rndBool, null);
            test(GetRandom.Double(), null);
            test(GetRandom.Int64(), null);
            var d = rndDt;
            test(d, d.Hour);
        }
        [TestMethod] public void GetIntervalTest() => getIntervalTest(false);
        private void getIntervalTest(bool perform) {
            void test(object x, object y, object z) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(Operation.GetInterval);
                else obj.GetInterval();
                Assert.AreEqual(z, obj.Peek());
            }

            var d = GetRandom.Int32(-1000, 1000);
            test(rndStr, d, null);
            test(rndBool, d, null);
            test(GetRandom.Double(), d, null);
            test(GetRandom.Int64(), d, null);
            var dt = rndDt;
            var dt1 = dt.AddSeconds(d);
            test(dt1, dt, dt1.Subtract(dt).Ticks);
        }
        [TestMethod]  public void OppositeTest() => oppositeTest(false);
        private void oppositeTest(bool perform) {
            void test(object x, object r) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.Opposite);
                else obj.Opposite();
                Assert.AreEqual(r, obj.Peek());
            }

            test(rndStr, null);
            test(rndBool, null);
            test(rndDt, null);
            test(4.0D, -4.0D);
            test(-4D, 4.0D);
            test(4.0M, -4.0M);
            test(-4M, 4.0M);
        }
        [TestMethod] public void GetLengthTest() => lengthTest(false);
        private void lengthTest(bool perform) {
            void test(object x, object y) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.GetLength);
                else obj.GetLength();
                Assert.AreEqual(y, obj.Peek());
            }

            test(rndBool, null);
            test(GetRandom.Double(), null);
            test(GetRandom.Int64(), null);
            test(rndDt, null);
            var s = rndStr;
            test(s, s.Length);
        }
        [TestMethod] public void IsLessTest() => lessTest(false);
        private void lessTest(bool perform) {
            void action(object x, object y, object z) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(Operation.IsLess);
                else obj.IsLess();
                Assert.AreEqual(z, obj.Peek());
            }

            void test(object x, object y) {
                action(x, y, false);
                action(y, x, true);
                action(x, x, false);
            }

            test(GetRandom.Double(1), GetRandom.Double(max: 0));
            test(rndStr, "AAA" + rndStr);
            test(true, false);
            test(GetRandom.DateTime(DateTime.Now),
                GetRandom.DateTime(max: DateTime.Now.AddDays(-1)));
            test((long)GetRandom.Int32(1), (long)GetRandom.Int32(max: 0));
        }
        [TestMethod] public void GetMinuteTest() => getMinuteTest(false);
        private void getMinuteTest(bool perform) {
            void test(object x, object y) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.GetMinute);
                else obj.GetMinute();
                Assert.AreEqual(y, obj.Peek());
            }

            test(rndStr, null);
            test(rndBool, null);
            test(GetRandom.Double(), null);
            test(GetRandom.Int64(), null);
            var d = rndDt;
            test(d, d.Minute);
        }
        [TestMethod] public void GetMonthTest() => getMonthTest(false);
        private void getMonthTest(bool perform) {
            void test(object x, object y) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.GetMonth);
                else obj.GetMonth();
                Assert.AreEqual(y, obj.Peek());
            }

            test(rndStr, null);
            test(rndBool, null);
            test(GetRandom.Double(), null);
            test(GetRandom.Int64(), null);
            var d = rndDt;
            test(d, d.Month);
        }
        [TestMethod] public void MultiplyTest() => multiplyTest(false);
        private void multiplyTest(bool perform) {
            void test(object x, object y, object z) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(Operation.Multiply);
                else obj.Multiply();
                Assert.AreEqual(z, obj.Peek());
            }

            test(rndStr, rndStr, null);
            var b1 = rndBool;
            var b2 = rndBool;
            test(b1, b2, b1.Multiply(b2));
            test(rndDt, rndDt, null);
            test(2.0, 5.0, 10.0);
            test(-2, 5.0, -10.0);
            var d1 = GetRandom.Double();
            var d2 = GetRandom.Double();
            test(d1, d2, d1 * d2);
        }
        [TestMethod] public void NotTest() => notTest(false);
        private void notTest(bool perform) {
            void test(object x, object y) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.Not);
                else obj.Not();
                Assert.AreEqual(y, obj.Peek());
            }

            test(GetRandom.Double(), null);
            test(rndStr, null);
            test(true, false);
            test(false, true);
            test(rndDt, null);
            test(GetRandom.Int64(), null);
        }
        [TestMethod] public void OrTest() => orTest(false);
        private void orTest(bool perform) {
            void test(object x, object y, object z) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(Operation.Or);
                else obj.Or();
                Assert.AreEqual(z, obj.Peek());
            }

            test(GetRandom.Double(), GetRandom.Double(), null);
            test(rndStr, rndStr, null);
            test(true, true, true);
            test(true, false, true);
            test(false, true, true);
            test(false, false, false);
            test(rndDt, rndDt, null);
            test(GetRandom.Int64(), GetRandom.Int64(), null);
        }
        [TestMethod] public void PowerTest() => powerTest(false);
        private void powerTest(bool perform) {
            void test(object x, object y, object z) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(Operation.Power);
                else obj.Power();
                Assert.AreEqual(z, obj.Peek());
            }
            test(rndStr, rndStr, null);
            test(rndBool, rndBool, null);
            test(rndDt, rndDt, null);
            test(4.0, 2.0, 16.0);
            test(16.0, 0.5, 4.0);
        }
        [TestMethod] public void InverseTest() => inverseTest(false);
        private void inverseTest(bool perform) {
            void test(object x, object r) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.Inverse);
                else obj.Inverse();
                Assert.AreEqual(r, obj.Peek());
            }

            test(rndStr, null);
            test(rndBool, null);
            test(rndDt, null);
            test(4.0, 1.0 / 4);
            test(-1.0 / 4, -4.0);
        }
        [TestMethod] public void GetSecondTest() => getSecondTest(false);
        private void getSecondTest(bool perform) {
            void test(object x, object y) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.GetSecond);
                else obj.GetSecond();
                Assert.AreEqual(y, obj.Peek());
            }
            test(rndStr, null);
            test(rndBool, null);
            test(GetRandom.Double(), null);
            test(GetRandom.Int64(), null);
            var d = rndDt;
            test(d, d.Second);
        }
        [TestMethod] public void SqrtTest() => sqrtTest(false);
        private void sqrtTest(bool perform) {
            void test(object x, object r) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.Sqrt);
                else obj.Sqrt();
                Assert.AreEqual(r, obj.Peek());
            }
            test(rndStr, null);
            test(rndBool, null);
            test(rndDt, null);
            test(25.0D, 5.0D);
            test(9D, 3.0D);
            test(25.0M, 5.0D);
            test(9M, 3.0D);
        }
        [TestMethod] public void SquareTest() => squareTest(false);
        private void squareTest(bool perform) {
            void test(object x, object r) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.Square);
                else obj.Square();
                Assert.AreEqual(r, obj.Peek());
            }
            test(rndStr, null);
            test(rndBool, null);
            test(rndDt, null);
            test(5D, 25.0D);
            test(-3.0D, 9.0D);
            test(5M, 25.0D);
            test(-3.0M, 9.0D);
        }
        [TestMethod] public void StartsWithTest() => startsWithTest(false);
        private void startsWithTest(bool perform) {
            void test(object x, object y, object r) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(Operation.StartsWith);
                else obj.StartsWith();
                Assert.AreEqual(r, obj.Peek());
            }
            var s = GetRandom.String(10, 100);
            var len = GetRandom.Int32(5, s.Length / 2);
            var s1 = s.Substring(0, len);
            test(s, s1, true);
            test(s, rndStr, false);
            test(s, rndDt, null);
            test(s, GetRandom.Double(), null);
        }
        [TestMethod] public void SubstringTest() => substringTest(false);
        private void substringTest(bool perform) {
            void test(object x, object y, object z, object r) {
                obj.Set(x);
                obj.Set(y);
                obj.Set(z);
                if (perform) obj.Perform(Operation.Substring);
                else obj.Substring();
                Assert.AreEqual(r, obj.Peek());
            }
            var s = GetRandom.String(10, 100);
            var idx = GetRandom.Int32(0, s.Length / 2);
            var len = GetRandom.Int32(0, s.Length / 2);
            test(s, idx, len, s.Substring(idx, len));
            test(len, s, idx, s.Substring(idx));
            test(len, rndDt, idx, null);
            test(len, GetRandom.Double(), idx, null);
            test(len, idx, s, null);
        }
        [TestMethod] public void ToLowerTest() => toLowerTest(false);
        private void toLowerTest(bool perform) {
            void test(object x, object y) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.ToLower);
                else obj.ToLower();
                Assert.AreEqual(y, obj.Peek());
            }
            test(rndBool, null);
            test(GetRandom.Double(), null);
            test(GetRandom.Int64(), null);
            test(rndDt, null);
            var s = rndStr;
            test(s.ToUpper(), s.ToLower());
        }
        [TestMethod] public void ToUpperTest() => toUpperTest(false);
        private void toUpperTest(bool perform) {
            void test(object x, object y) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.ToUpper);
                else obj.ToUpper();
                Assert.AreEqual(y, obj.Peek());
            }
            test(rndBool, null);
            test(GetRandom.Double(), null);
            test(GetRandom.Int64(), null);
            test(rndDt, null);
            var s = rndStr;
            test(s.ToLower(), s.ToUpper());
        }
        [TestMethod] public void TrimTest() => trimTest(false);
        private void trimTest(bool perform) {
            void test(object x, object y) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.Trim);
                else obj.Trim();
                Assert.AreEqual(y, obj.Peek());
            }
            test(rndBool, null);
            test(GetRandom.Double(), null);
            test(GetRandom.Int64(), null);
            test(rndDt, null);
            var s = rndStr;
            test("    " + s + "    ", s);
        }
        [TestMethod] public void XorTest() => xorTest(false);
        private void xorTest(bool perform) {
            void test(object x, object y, object z) {
                obj.Set(x);
                obj.Set(y);
                if (perform) obj.Perform(Operation.Xor);
                else obj.Xor();
                Assert.AreEqual(z, obj.Peek());
            }
            test(GetRandom.Double(), GetRandom.Double(), null);
            test(rndStr, rndStr, null);
            test(true, true, false);
            test(true, false, true);
            test(false, true, true);
            test(false, false, false);
            test(rndDt, rndDt, null);
            test(GetRandom.Int64(), GetRandom.Int64(), null);
        }
        [TestMethod] public void GetYearTest() => getYearTest(false);
        private void getYearTest(bool perform) {
            void test(object x, object y) {
                obj.Set(x);
                if (perform) obj.Perform(Operation.GetYear);
                else obj.GetYear();
                Assert.AreEqual(y, obj.Peek());
            }
            test(rndStr, null);
            test(rndBool, null);
            test(GetRandom.Double(), null);
            test(GetRandom.Int64(), null);
            var d = rndDt;
            test(d, d.Year);
        }
        [TestMethod] public void ToExpressionTest() {
            foreach (Operation o in Enum.GetValues(typeof(Operation))) {
                var e = Expression.Parameter(typeof(ExprTests.testClass), "x");
                obj.ToExpression(o, e);
                var x = obj.Get();
                isNull(x);
            }
            notTested("Ei saa aru, mida see teeb");
        }
    }
}
