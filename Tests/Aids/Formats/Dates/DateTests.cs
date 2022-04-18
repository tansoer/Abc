using System.Collections.Generic;
using System.Globalization;
using Abc.Aids.Formats.Dates;
using Abc.Aids.Random;
using Abc.Aids.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Formats.Dates {

    [TestClass]
    public class DateTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Date);

        [TestMethod]
        public void LongTest()
            => Assert.AreEqual("yyyy-MM-dd", Date.Long);

        [TestMethod]
        public void ShortTest()
            => Assert.AreEqual("yyyy-MM-dd", Date.Short);

        [TestMethod]
        public void ShortAndLongAreSameTest()
            => Assert.AreEqual(Date.Long, Date.Short);

        [TestMethod]
        public void DayFirstTest()
            => Assert.AreEqual("dd-MM-yyyy", Date.DayFirst);

        [TestMethod]
        public void MonthFirstTest()
            => Assert.AreEqual("yyyy-MM-dd", Date.MonthFirst);

        [TestMethod]
        public void AllTest() {
            foreach (var f in Date.All) {
                var expected = rndDt;
                var s = expected.ToString(f);
                var b = System.DateTime.TryParseExact(s, f,
                    UseCulture.Current, DateTimeStyles.None, out var actual);
                Assert.AreEqual(true, b);
                Assert.AreEqual(expected.ToString(f), actual.ToString(f));
            }
        }

        [TestMethod]
        public void AllMonthFirstTest() {
            var l = new List<string>();
            l.AddRange(Date.AllMonthFirst);

            for (var i = Date.AllMonthFirst.Count; i > 0; i--) {
                var f = Date.AllMonthFirst[i - 1];
                l.Remove(f);
                Assert.IsFalse(l.Contains(f));
            }

            Assert.AreEqual(0, l.Count);
        }

        [TestMethod]
        public void AllDayFirstTest() {
            var l = new List<string>();
            l.AddRange(Date.AllDayFirst);

            for (var i = Date.AllDayFirst.Count; i > 0; i--) {
                var f = Date.AllDayFirst[i - 1];
                l.Remove(f);
                Assert.IsFalse(l.Contains(f));
            }

            Assert.AreEqual(0, l.Count);
        }

        [TestMethod]
        public void NumberOfAllTest() {
            var expected = Date.AllDayFirst.Count + Date.AllMonthFirst.Count;
            Assert.AreEqual(expected, Date.All.Count);
        }

        [TestMethod]
        public void IsDateTest() {
            foreach (var f in Date.All) Assert.IsTrue(Date.IsDate(f));
            foreach (var f in Time.All) Assert.IsFalse(Date.IsDate(f));
            Assert.IsFalse(Date.IsDate(null));
            Assert.IsFalse(Date.IsDate(""));
            Assert.IsFalse(Date.IsDate("  "));
            Assert.IsFalse(Date.IsDate(rndStr));
        }

    }

}
