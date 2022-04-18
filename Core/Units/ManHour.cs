using System.Collections.Generic;

namespace Abc.Core.Units {

    public static class ManHour {

        public static UnitInfo Measure = new UnitInfo("ManHour");

        public static List<UnitInfo> Units =>
            new List<UnitInfo> {
                new UnitInfo(manHourName, 1),
                new UnitInfo(decaManHourName, Factors.Deca),
                new UnitInfo(hectoManHourName, Factors.Hecto),
                new UnitInfo(kiloManHourName, Factors.Kilo),
                new UnitInfo(megaManHourName, Factors.Mega),
                new UnitInfo(gigaManHourName, Factors.Giga),
                new UnitInfo(teraManHourName, Factors.Tera),
                new UnitInfo(manDayName, 8),
                new UnitInfo(manWeekName, 5 * 8),
                new UnitInfo(manMonthName, 5 * 8 * 4),
                new UnitInfo(manYearName, 5 * 8 * 4 * 11),
            };
        internal const string manHourName = "ManHour";
        internal const string decaManHourName = "DecaManHour";
        internal const string hectoManHourName = "HectoManHour";
        internal const string kiloManHourName = "KiloManHour";
        internal const string megaManHourName = "MegaManHour";
        internal const string gigaManHourName = "GigaManHour";
        internal const string teraManHourName = "TeraManHour";
        internal const string manDayName = "ManDay";
        internal const string manWeekName = "ManWeek";
        internal const string manMonthName = "ManMonth";
        internal const string manYearName = "ManYear";

    }

}

