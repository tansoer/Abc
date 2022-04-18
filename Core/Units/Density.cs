using System.Collections.Generic;

namespace Abc.Core.Units {

    public static class Density {

        public static UnitInfo Measure = new UnitInfo("Density", "ρ", "Volumetric mass density",
            "The density (more precisely, the volumetric mass density; " +
            "also known as specific mass), of a substance is its mass " +
            "per unit volume. The symbol most often used for density is ρ " +
            "(the lower case Greek letter rho), although the Latin letter " +
            "D can also be used. Mathematically, density is defined as mass divided by volume",
            new TermInfo(Mass.Measure.Id, 1),
            new TermInfo(Volume.Measure.Id, -1));

        public static List<UnitInfo> Units =>
            new List<UnitInfo> {
                new UnitInfo(gramsPerLitreName, "g/L", "Grams per Litre",
                    new TermInfo(Mass.gramsName, 1),
                    new TermInfo(Volume.litersName, -1)),
                new UnitInfo(milligramsPerLitreName, "mg/L", "Milligrams per Litre",
                    new TermInfo(Mass.milligramsName, 1),
                    new TermInfo(Volume.litersName, -1)),
            };

        internal const string gramsPerLitreName = "GramsPerLitre";
        internal const string milligramsPerLitreName = "MilligramsPerLitre";
    }

}
