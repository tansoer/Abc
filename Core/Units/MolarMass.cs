using System.Collections.Generic;

namespace Abc.Core.Units {
    public static class MolarMass {

        public static UnitInfo Measure = new UnitInfo("MolarMass", "M", "Molar Mass",
            "In chemistry, the molar mass of a chemical compound is defined as " +
            "the mass of a sample of that compound divided by the amount of substance " +
            "in that sample, measured in moles. The molar mass is a bulk, not molecular, " +
            "property of a substance. The molar mass is an average of many instances of the " +
            "compound, which often vary in mass due to the presence of isotopes. Most " +
            "commonly, the molar mass is computed from the standard atomic weights and is " +
            "thus a terrestrial average and a function of the relative abundance of the " +
            "isotopes of the constituent atoms on earth. The molar mass is appropriate " +
            "for converting between the mass of a substance " +
            "and the amount of a substance for bulk quantities",
            new TermInfo(Mass.Measure.Id, 1),
            new TermInfo(Substance.Measure.Id, -1));

        public static List<UnitInfo> Units =>
            new List<UnitInfo> {
                new UnitInfo(gramsPerMillimole, "gr/mmol", "Grams per millimole",
                    new TermInfo(Mass.gramsName, 1),
                    new TermInfo(Substance.milliMoleName, -1)),
            };

        internal const string gramsPerMillimole = "GramsPerMilliMole";
    }

}
