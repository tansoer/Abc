using System.Collections.Generic;

namespace Abc.Core.Units {

    public static class Distance {

        public static UnitInfo Measure = new UnitInfo
        (
            "Distance", "l", "Distance", "The measurement or extent of something from end to end."
        );

        public static List<UnitInfo> Units =>
            new List<UnitInfo> {
                new UnitInfo(angstromsName, Factors.Angstrom),
                new UnitInfo(astronomicalUnitsName, astronomicalUnitsFactor),
                new UnitInfo(centimetersName, "cm", Factors.Centi),
                new UnitInfo(chainsName, chainsFactor),
                new UnitInfo(cubitsName, cubitsFactor),
                new UnitInfo(decametersName, Factors.Deca),
                new UnitInfo(decimetersName, "dm", Factors.Deci),
                new UnitInfo(fathomsName, fathomsFactor),
                new UnitInfo(feetName, feetFactor),
                new UnitInfo(furlongsName, furlongsFactor),
                new UnitInfo(gigametersName, Factors.Giga),
                new UnitInfo(handsName, handsFactor),
                new UnitInfo(hectometersName, Factors.Hecto),
                new UnitInfo(inchesName, inchesFactor),
                new UnitInfo(kilometersName, "km", Factors.Kilo),
                new UnitInfo(lightSecondsName, lightSecondsFactor),
                new UnitInfo(lightYearsName, lightYearsFactor),
                new UnitInfo(linksName, linksFactor),
                new UnitInfo(megametersName, Factors.Mega),
                new UnitInfo(metersName, "m", 1),
                new UnitInfo(micromicronsName, Factors.Micromicro),
                new UnitInfo(micronsName, Factors.Micro),
                new UnitInfo(milesName, milesFactor),
                new UnitInfo(millimetersName, "mm", Factors.Milli),
                new UnitInfo(millimicronsName, Factors.Millimicro),
                new UnitInfo(nanometersName, Factors.Nano),
                new UnitInfo(nauticalMilesName, nauticalMilesFactor),
                new UnitInfo(pacesName, pacesFactor),
                new UnitInfo(parsecsName, parsecsFactor),
                new UnitInfo(picasName, picasFactor),
                new UnitInfo(pointsName, pointsFactor),
                new UnitInfo(rodsName, rodsFactor),
                new UnitInfo(yardsName, yardsFactor)
            };
        internal const string astronomicalUnitsName = "AstronomicalUnits";
        internal const string angstromsName = "Angstroms";
        internal const string centimetersName = "Centimeters";
        internal const string chainsName = "Chains";
        internal const string cubitsName = "Cubits";
        internal const string decametersName = "Decameters";
        internal const string decimetersName = "Decimeters";
        internal const string feetName = "Feet";
        internal const string fathomsName = "Fathoms";
        internal const string furlongsName = "Furlongs";
        internal const string gigametersName = "Gigameters";
        internal const string handsName = "Hands";
        internal const string hectometersName = "Hectometers";
        internal const string inchesName = "Inches";
        internal const string kilometersName = "Kilometers";
        internal const string lightYearsName = "LightYears";
        internal const string lightSecondsName = "LightSeconds";
        internal const string linksName = "Links";
        internal const string metersName = "Meters";
        internal const string micromicronsName = "Micromicrons";
        internal const string megametersName = "Megameters";
        internal const string micronsName = "Microns";
        internal const string milesName = "Miles";
        internal const string millimetersName = "Millimeters";
        internal const string millimicronsName = "Millimicrons";
        internal const string nanometersName = "Nanometers";
        internal const string nauticalMilesName = "NauticalMiles";
        internal const string pacesName = "Paces";
        internal const string parsecsName = "Parsecs";
        internal const string picasName = "Picas";
        internal const string pointsName = "Points";
        internal const string rodsName = "Rods";
        internal const string yardsName = "Yards";

        internal const double astronomicalUnitsFactor = 1.49598E11;
        internal const double chainsFactor = rodsFactor * 4.0;
        internal const double cubitsFactor = 0.4572;
        internal const double fathomsFactor = feetFactor * 6.0;
        internal const double feetFactor = inchesFactor * 12.0;
        internal const double furlongsFactor = yardsFactor * 220.0;
        internal const double handsFactor = inchesFactor * 4.0;
        internal const double inchesFactor = 0.0254;
        internal const double lightSecondsFactor = 2.99792458E8;
        internal const double lightYearsFactor = lightSecondsFactor * 31556925.9747;
        internal const double linksFactor = chainsFactor / 100.0;
        internal const double milesFactor = feetFactor * 5280;
        internal const double nauticalMilesFactor = 1852.0;
        internal const double pacesFactor = inchesFactor * 30.0;
        internal const double parsecsFactor = astronomicalUnitsFactor * 206264.806247096;
        internal const double picasFactor = pointsFactor * 12.0;
        internal const double rodsFactor = feetFactor * 16.5;
        internal const double pointsFactor = inchesFactor * 0.013837;
        internal const double yardsFactor = feetFactor * 3.0;

    }

}
