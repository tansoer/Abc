
using System.Collections.Generic;

namespace Abc.Core.Units {

    public static class Substance {

        public static UnitInfo Measure = new UnitInfo
        (
            "Substance",
            "n",
            "Amount of Substance",
            "In chemistry, the amount of substance in a given " +
            "sample of matter is the number of discrete atomic-scale " +
            "particles in it; where the particles may be molecules, " +
            "atoms, ions, electrons, or other, depending on the context. " +
            "It is sometimes referred to as the chemical amount."
        );

        public static List<UnitInfo> Units =>
            new List<UnitInfo>
            {
                new UnitInfo(moleName, "mol", moleName, "The mole (symbol: mol) "+
                                            "is the base unit of amount of substance "+
                                            "(\"number of substance\") in the International "+
                                            "System of Units or System International (SI), "+
                                            "defined as containing exactly 6.02214076×1023 "+
                                            "particles, e.g., atoms, molecules, ions or electrons.",
                    1),

                new UnitInfo(milliMoleName, "mmole", Factors.Milli),
            };

        internal const string milliMoleName = "MilliMole";
        internal const string moleName = "Mole";
    };

}

