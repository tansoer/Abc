using System;
using Abc.Aids.Random;
using Abc.Aids.Values;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Rules {

    [TestClass]
    public class DateTimeVariableTests : BaseVariableTests<DateTimeVariable, DateTime> {

        private DateTime f;
        private DateTime t;
        private int i;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            f = GetRandom.DateTime(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(100));
            t = GetRandom.DateTime(f, DateTime.Now.AddYears(200));
            i = GetRandom.Int32(-100, 100);
        }

        protected override void isEqualTest() {
            assert(f, f, true);
            assert(f, t, false);
            assert(t, f, false);
            assert(f, f, true);
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
        protected override void getSecondTest() => assert(f, (double) f.Second);
        protected override void getMinuteTest() => assert(f, (double) f.Minute);
        protected override void getHourTest() => assert(f, (double) f.Hour);
        protected override void getDayTest() => assert(f, (double) f.Day);
        protected override void getMonthTest() => assert(f, (double) f.Month);
        protected override void getYearTest() => assert(f, (double) f.Year);
        protected override void toSecondsTest() => assert(f, toTimeSpan(f).Seconds());
        protected override void toMinutesTest() => assert(f, toTimeSpan(f).Minutes());
        protected override void toHoursTest() => assert(f, toTimeSpan(f).Hours());
        protected override void toDaysTest() => assert(f, toTimeSpan(f).Days());
        protected override void toMonthsTest() => assert(f, toTimeSpan(f).Months());
        protected override void toYearsTest() => assert(f, toTimeSpan(f).Years());
        protected override void addSecondsTest() => assert(f, i, f.AddSeconds(i));
        protected override void addMinutesTest() => assert(f, i, f.AddMinutes(i));
        protected override void addHoursTest() => assert(f, i, f.AddHours(i));
        protected override void addDaysTest() => assert(f, i, f.AddDays(i));
        protected override void addMonthsTest() => assert(f, i, f.AddMonths(i));
        protected override void addYearsTest() => assert(f, i, f.AddYears(i));
        protected override void subtractTest() => assert(f.Add(TimeSpan.FromTicks(t.Ticks)), t, f);
        protected override void addTest() => assert(t.Add(-TimeSpan.FromTicks(f.Ticks)), f, t);
        protected override void intervalTest() => assert(f.Add(TimeSpan.FromTicks(t.Ticks)), t, (decimal) f.Ticks);
        protected override void getAgeTest() => assert(DateTime.Now.AddYears(-Math.Abs(i)), Math.Abs(i));
        private static TimeSpan toTimeSpan(in DateTime d) => new TimeSpan(d.Ticks);

    }

}