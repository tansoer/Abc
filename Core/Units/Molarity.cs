using System.Collections.Generic;

namespace Abc.Core.Units {
    public static class Molarity {

        public static UnitInfo Measure = new UnitInfo("Molarity", null, "Substance Concentration",
            "Molar concentration (also called molarity, amount concentration or substance concentration) " +
            "is a measure of the concentration of a chemical species, in particular of a solute in a " +
            "solution, in terms of amount of substance per unit volume of solution. In chemistry, the " +
            "most commonly used unit for molarity is the number of moles per litre, having the unit " +
            "symbol mol/L or mol⋅dm−3 in SI unit. A solution with a concentration of 1 mol/L is " +
            "said to be 1 molar, commonly designated as 1 M. To avoid confusion with SI prefix mega, " +
            "which has the same abbreviation, small caps ᴍ or italicized M are also used in journals and textbooks",
            new TermInfo(Substance.Measure.Id, 1),
            new TermInfo(Volume.Measure.Id, -1));

        public static List<UnitInfo> Units =>
            new List<UnitInfo> {
                new UnitInfo(molePerLitreName, "mol/L", "Mole per Litre",
                    new TermInfo(Substance.moleName, 1),
                    new TermInfo(Volume.litersName, -1)),
                new UnitInfo(millimolePerLitreName, "mmol/L", "Millimole per Litre",
                    new TermInfo(Substance.milliMoleName, 1),
                    new TermInfo(Volume.litersName, -1)),
            };

        internal const string molePerLitreName = "MolePerLitre";
        internal const string millimolePerLitreName = "MillimolePerLitre";
    }

}
