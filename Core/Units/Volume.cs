using System.Collections.Generic;

namespace Abc.Core.Units {

    public static class Volume {

        public static UnitInfo Measure = new UnitInfo("Volume", "V", null,
            "Volume is the quantity of three-dimensional space enclosed by " +
            "a closed surface, for example, the space that a substance " +
            "(solid, liquid, gas, or plasma) or shape occupies or contains. " +
            "Volume is often quantified numerically using the SI derived unit, " +
            "the cubic metre.",
            new TermInfo(Distance.Measure.Id, 3));
        public static List<UnitInfo> Units =>
            new List<UnitInfo> {
                new UnitInfo(cubicCentimetersName, new TermInfo(Distance.centimetersName, 3)),
                new UnitInfo(cubicDecametersName, new TermInfo(Distance.decametersName, 3)),
                new UnitInfo(cubicDecimetersName, new TermInfo(Distance.decimetersName, 3)),
                new UnitInfo(cubicFeetName, new TermInfo(Distance.feetName, 3)),
                new UnitInfo(cubicHectometersName, new TermInfo(Distance.hectometersName, 3)),
                new UnitInfo(cubicInchesName, new TermInfo(Distance.inchesName, 3)),
                new UnitInfo(cubicKilometersName, new TermInfo(Distance.kilometersName, 3)),
                new UnitInfo(cubicMetersName, new TermInfo(Distance.metersName, 3)),
                new UnitInfo(cubicMilesName, new TermInfo(Distance.milesName, 3)),
                new UnitInfo(cubicMillimetersName, new TermInfo(Distance.millimetersName, 3)),
                new UnitInfo(cubicYardsName, new TermInfo(Distance.yardsName, 3)),
                new UnitInfo(kiloLitersName, new TermInfo(Distance.metersName, 3)),
                new UnitInfo(litersName, new TermInfo(Distance.decimetersName, 3)),
                new UnitInfo(milliLitersName, new TermInfo(Distance.centimetersName, 3)),
                new UnitInfo(centiLitersName, new TermInfo(Distance.centimetersName, 2),
                    new TermInfo(Distance.decimetersName)),
                new UnitInfo(decaLitersName, new TermInfo(Distance.decimetersName, 2),
                    new TermInfo(Distance.metersName)),
                new UnitInfo(deciLitersName, new TermInfo(Distance.centimetersName, 2),
                    new TermInfo(Distance.metersName)),
                new UnitInfo(hectoLitersName, new TermInfo(Distance.metersName, 2),
                    new TermInfo(Distance.decimetersName))
            };

        internal const string cubicMillimetersName = "CubicMillimeters";
        internal const string cubicCentimetersName = "CubicCentimeters";
        internal const string cubicDecimetersName = "CubicDecimeters";
        internal const string cubicMetersName = "CubicMeters";
        internal const string cubicDecametersName = "CubicDecameters";
        internal const string cubicHectometersName = "CubicHectometers";
        internal const string cubicKilometersName = "CubicKilometers";
        internal const string cubicInchesName = "CubicInches";
        internal const string cubicFeetName = "CubicFeet";
        internal const string cubicYardsName = "CubicYards";
        internal const string cubicMilesName = "CubicMiles";
        internal const string milliLitersName = "MilliLiters";
        internal const string centiLitersName = "CentiLiters";
        internal const string deciLitersName = "DeciLiters";
        internal const string litersName = "Liters";
        internal const string decaLitersName = "DecaLiters";
        internal const string hectoLitersName = "HectoLiters";
        internal const string kiloLitersName = "KiloLiters";

    }

}
