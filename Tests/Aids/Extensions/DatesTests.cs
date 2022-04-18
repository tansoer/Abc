using System;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Extensions {

    [TestClass]
    public class DatesTests : TestsBase {

        private DateTime x;
        private DateTime y;
        private TimeSpan t;
        private int i;
        private double d;
        private readonly DateTime min = DateTime.MinValue;
        private readonly DateTime max = DateTime.MaxValue;

        private DateTime dt;
        private TimeSpan ts;
        private byte b;

        [TestInitialize]
        public void TestInitialize() {
            x = GetRandom.DateTime(DateTime.Now, DateTime.Now.AddYears(100));
            y = GetRandom.DateTime(DateTime.Now.AddYears(-100), DateTime.Now);
            t = TimeSpan.FromTicks(y.Ticks);
            i = GetRandom.Int32(-100, 100);
            d = GetRandom.Double(-100, 100);
            dt = GetRandom.DateTime(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(100));
            ts = new TimeSpan((GetRandom.DateTime(dt, dt.AddYears(100))).Ticks);
            b = GetRandom.UInt8();
            type = typeof(Dates);
        }

        [TestMethod]
        public void AddDaysSafeTest() {
            Assert.AreEqual(max, max.AddDaysSafe(1));
            Assert.AreEqual(max, min.AddDaysSafe(-1));
            Assert.AreEqual(x.AddDays(d), x.AddDaysSafe(d));
            Assert.AreEqual(x.AddDays(-d), x.AddDaysSafe(-d));
        }

        [TestMethod]
        public void AddHoursSafeTest() {
            Assert.AreEqual(max, max.AddHoursSafe(1));
            Assert.AreEqual(max, min.AddHoursSafe(-1));
            Assert.AreEqual(x.AddHours(d), x.AddHoursSafe(d));
            Assert.AreEqual(x.AddHours(-d), x.AddHoursSafe(-d));
        }

        [TestMethod]
        public void AddMinutesSafeTest() {
            Assert.AreEqual(max, max.AddMinutesSafe(1));
            Assert.AreEqual(max, min.AddMinutesSafe(-1));
            Assert.AreEqual(x.AddMinutes(d), x.AddMinutesSafe(d));
            Assert.AreEqual(x.AddMinutes(-d), x.AddMinutesSafe(-d));
        }

        [TestMethod]
        public void AddMonthsSafeTest() {
            Assert.AreEqual(max, max.AddMonthsSafe(1));
            Assert.AreEqual(max, min.AddMonthsSafe(-1));
            Assert.AreEqual(x.AddMonths(i), x.AddMonthsSafe(i));
            Assert.AreEqual(x.AddMonths(-i), x.AddMonthsSafe(-i));
        }

        [TestMethod]
        public void AddSecondsSafeTest() {
            Assert.AreEqual(max, max.AddSecondsSafe(1));
            Assert.AreEqual(max, min.AddSecondsSafe(-1));
            Assert.AreEqual(x.AddSeconds(d), x.AddSecondsSafe(d));
            Assert.AreEqual(x.AddSeconds(-d), x.AddSecondsSafe(-d));
        }

        [TestMethod]
        public void AddYearsSafeTest() {
            Assert.AreEqual(max, max.AddYearsSafe(1));
            Assert.AreEqual(max, min.AddYearsSafe(-1));
            Assert.AreEqual(x.AddYears(i), x.AddYearsSafe(i));
            Assert.AreEqual(x.AddYears(-i), x.AddYearsSafe(-i));
        }

        [TestMethod]
        public void AddSafeTest() {
            Assert.AreEqual(max, max.AddSafe(x));
            Assert.AreEqual(x.Add(t), x.AddSafe(y));
        }

        [TestMethod]
        public void SubtractSafeTest() {
            Assert.AreEqual(max, min.SubtractSafe(x));
            var expected = x.Add(-t);
            var actual = x.SubtractSafe(y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDayTest()
            => Assert.AreEqual(dt.Day, dt.GetDay());

        [TestMethod]
        public void GetHourTest()
            => Assert.AreEqual(dt.Hour, dt.GetHour());

        [TestMethod]
        public void GetMinuteTest()
            => Assert.AreEqual(dt.Minute, dt.GetMinute());

        [TestMethod]
        public void GetMonthTest()
            => Assert.AreEqual(dt.Month, dt.GetMonth());

        [TestMethod]
        public void GetSecondTest()
            => Assert.AreEqual(dt.Second, dt.GetSecond());

        [TestMethod]
        public void GetYearTest()
            => Assert.AreEqual(dt.Year, dt.GetYear());

        [TestMethod]
        public void GetAgeTest()
            => Assert.AreEqual(b, dt.GetAge(dt.AddYears(b)));

        [TestMethod] public void GetIntervalTest() => Assert.AreEqual(ts, dt.AddTicks(ts.Ticks).GetInterval(dt));

        [TestMethod]
        public void IsDateOnlyTest() {
            Assert.AreEqual(false, Dates.IsDateOnly(dt));
            Assert.AreEqual(true, Dates.IsDateOnly(dt.Date));
        }

    }

}