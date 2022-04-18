using System.Collections.Generic;

namespace Abc.Core.Units {

    public static class Counter {

        public static UnitInfo Measure = new UnitInfo("Counter");

        public static List<UnitInfo> Units =>
            new List<UnitInfo> {
                new UnitInfo(unitName, 1),
                new UnitInfo(decaUnitName, Factors.Deca),
                new UnitInfo(hectoUnitName, Factors.Hecto),
                new UnitInfo(kiloUnitName, Factors.Kilo),
                new UnitInfo(megaUnitName, Factors.Mega),
                new UnitInfo(gigaUnitName, Factors.Giga),
                new UnitInfo(teraUnitName, Factors.Tera)
            };
        internal const string unitName = "Units";
        internal const string decaUnitName = "DecaUnits";
        internal const string hectoUnitName = "HectoUnits";
        internal const string kiloUnitName = "KiloUnits";
        internal const string megaUnitName = "MegaUnits";
        internal const string gigaUnitName = "GigaUnits";
        internal const string teraUnitName = "TeraUnits";

    }

}
