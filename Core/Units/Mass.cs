using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace Abc.Core.Units {
    public static class Mass {

        public static UnitInfo Measure = new UnitInfo(
            measureName,
            "m",
            measureName,
            "The quantity of matter which a body contains, as measured by " +
            "its acceleration under a given force or by the force exerted on " +
            "it by a gravitational field"
        );

        public static List<UnitInfo> Units =>
            new List<UnitInfo> {
                new UnitInfo(centigramsName, centigramsFactor),
                new UnitInfo(decagramsName, Factors.Centi),
                new UnitInfo(decigramsName, decigramsFactor),
                new UnitInfo(dramsName, dramsFactor),
                new UnitInfo(grainsName, grainsFactor),
                new UnitInfo(gramsName, "g", Factors.Milli),
                new UnitInfo(hectogramsName, Factors.Deci),
                new UnitInfo(kilogramsName, "kg", 1),
                new UnitInfo(longTonsName, longTonsFactor),
                new UnitInfo(metricTonsName, "T", Factors.Kilo),
                new UnitInfo(microgramsName, Factors.Nano),
                new UnitInfo(milligramsName, Factors.Micro),
                new UnitInfo(nanogramsName, Factors.Pico),
                new UnitInfo(ouncesName, ouncesFactor),
                new UnitInfo(poundsName, poundFactor),
                new UnitInfo(stonesName, stonesFactor),
                new UnitInfo(tonsName, tonsFactor),
                new UnitInfo(mmHgMassName, mmHgMassFactor)
             };

        internal const string measureName = "Mass";
        internal const string centigramsName = "Centigrams";
        internal const string decagramsName = "Decagrams";
        internal const string decigramsName = "Decigrams";
        internal const string dramsName = "Drams";
        internal const string grainsName = "Grains";
        internal const string gramsName = "Grams";
        internal const string hectogramsName = "Hectograms";
        internal const string kilogramsName = "Kilograms";
        internal const string longTonsName = "LongTons";
        internal const string metricTonsName = "MetricTons";
        internal const string microgramsName = "Micrograms";
        internal const string milligramsName = "Milligrams";
        internal const string nanogramsName = "Nanograms";
        internal const string ouncesName = "Ounces";
        internal const string poundsName = "Pounds";
        internal const string stonesName = "Stones";
        internal const string tonsName = "Tons";
        internal const string mmHgMassName = "mmHgMass";

        internal static double centigramsFactor => Factors.Milli * Factors.Centi;
        internal static double decigramsFactor => Factors.Milli * Factors.Deci;
        internal const double dramsFactor = poundFactor / 256;
        internal const double grainsFactor = poundFactor / 7000;
        internal const double longTonsFactor = poundFactor * 2240;
        internal const double ouncesFactor = poundFactor / 16;
        internal const double poundFactor = 0.45359237;
        internal const double stonesFactor = poundFactor * 14;
        internal const double tonsFactor = poundFactor * 2000;
        internal const double mmHgMassFactor = 0.0075006156130264;
    }
}
