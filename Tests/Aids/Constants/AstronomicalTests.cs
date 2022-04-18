using Abc.Aids.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Constants {

    [TestClass]
    public class AstronomicalTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Astronomical);

        [TestMethod] public void HoursPerDayTest() => Assert.AreEqual(24.0, Astronomical.HoursPerDay);

        [TestMethod] public void MinutesPerHourTest() => Assert.AreEqual(60.0, Astronomical.MinutesPerHour);

        [TestMethod] public void SecondsPerMinuteTest() => Assert.AreEqual(60.0, Astronomical.SecondsPerMinute);

        [TestMethod] public void SecondsPerHourTest() => Assert.AreEqual(60 * 60, Astronomical.SecondsPerHour);

        [TestMethod] public void SecondsPerDayTest() => Assert.AreEqual(24 * 60 * 60, Astronomical.SecondsPerDay);

        [TestMethod] public void DaysPerWeekTest() => Assert.AreEqual(7.0, Astronomical.DaysPerWeek);

        [TestMethod]
        public void SecondsPerWeekTest() =>
            Assert.AreEqual(7 * 24 * 60 * 60, Astronomical.SecondsPerWeek);

        [TestMethod] public void WeeksPerFortnightTest() => Assert.AreEqual(2.0, Astronomical.WeeksPerFortnight);

        [TestMethod] public void DaysPerMonthTest() => Assert.AreEqual(30.4375, Astronomical.DaysPerMonth);

        [TestMethod]
        public void SecondsPerMonthTest() =>
            Assert.AreEqual(30.4375 * 24 * 60 * 60, Astronomical.SecondsPerMonth);

        [TestMethod] public void DaysPerYearTest() => Assert.AreEqual(365.25, Astronomical.DaysPerYear);

        [TestMethod]
        public void SecondsPerYearTest() =>
            Assert.AreEqual(365.25 * 24 * 60 * 60, Astronomical.SecondsPerYear);

        [TestMethod] public void YearsPerDecadeTest() => Assert.AreEqual(10.0, Astronomical.YearsPerDecade);

        [TestMethod] public void YearsPerCenturyTest() => Assert.AreEqual(100.0, Astronomical.YearsPerCentury);

        [TestMethod] public void YearsPerMillenniumTest() => Assert.AreEqual(1000.0, Astronomical.YearsPerMillennium);

        [TestMethod]
        public void SecondsPerFortnightTest() =>
            Assert.AreEqual(2 * 7 * 24 * 60 * 60, Astronomical.SecondsPerFortnight);

        [TestMethod]
        public void SecondsPerDecadeTest() =>
            Assert.AreEqual(10 * 365.25 * 24 * 60 * 60, Astronomical.SecondsPerDecade);

        [TestMethod]
        public void SecondsPerCenturyTest() =>
            Assert.AreEqual(100 * 365.25 * 24 * 60 * 60, Astronomical.SecondsPerCentury);

        [TestMethod]
        public void SecondsPerMillenniumTest() =>
            Assert.AreEqual(1000 * 365.25 * 24 * 60 * 60, Astronomical.SecondsPerMillennium);

    }

}
