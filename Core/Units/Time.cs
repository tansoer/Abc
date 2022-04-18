using System.Collections.Generic;
using Abc.Aids.Constants;

namespace Abc.Core.Units {

    public static class Time {

        public static UnitInfo Measure =
            new UnitInfo
            (
                "Time",
                "t",
                "Time",
                "In physical science, time is defined as a measurement, " +
                "or as what the clock face reads."
            );

        public static List<UnitInfo> Units =>
            new List<UnitInfo> {
                new UnitInfo(centuriesName, Astronomical.SecondsPerCentury),
                new UnitInfo(decadesName, Astronomical.SecondsPerDecade),
                new UnitInfo(daysName, Astronomical.SecondsPerDay),
                new UnitInfo(fortnightsName, Astronomical.SecondsPerFortnight),
                new UnitInfo(hoursName, "h", Astronomical.SecondsPerHour),
                new UnitInfo(microsecondsName, Factors.Micro),
                new UnitInfo(millenniumName, Astronomical.SecondsPerMillennium),
                new UnitInfo(millisecondsName, Factors.Milli),
                new UnitInfo(minutesName, "min", Astronomical.MinutesPerHour),
                new UnitInfo(monthsName, Astronomical.SecondsPerMonth),
                new UnitInfo(nanosecondsName, Factors.Nano),
                new UnitInfo(secondsName, "sec", 1),
                new UnitInfo(weeksName, Astronomical.SecondsPerWeek),
                new UnitInfo(yearsName, Astronomical.SecondsPerYear)

            };

        internal const string nanosecondsName = "Nanoseconds";
        internal const string microsecondsName = "Microseconds";
        internal const string millisecondsName = "Milliseconds";
        internal const string secondsName = "Seconds";
        internal const string minutesName = "Minutes";
        internal const string hoursName = "Hours";
        internal const string daysName = "Days";
        internal const string weeksName = "Weeks";
        internal const string fortnightsName = "Fortnights";
        internal const string monthsName = "Months";
        internal const string yearsName = "Years";
        internal const string decadesName = "Decades";
        internal const string centuriesName = "Centuries";
        internal const string millenniumName = "Millennium";

    }

}
