using System.Collections.Generic;

namespace Abc.Core.Units {
    public static class Acidity {

        public static UnitInfo Measure = new UnitInfo(
            measureName,
            null,
            measureName,
            "The level of acid in substances such as water, soil, or wine."
        );

        public static List<UnitInfo> Units =>
            new List<UnitInfo> {
                new UnitInfo(powerOfHydrogenName, "pH", "Power Of Hydrogen",
                    "In chemistry, pH , denoting 'potential of hydrogen' or 'power of hydrogen') is a scale "+
                    "used to specify the acidity or basicity of an aqueous solution. Lower pH values "+
                    "correspond to solutions which are more acidic in nature, while higher values "+
                    "correspond to solutions which are more basic or alkaline. At room temperature "+
                    "(25 °C or 77 °F), pure water is neutral (neither acidic nor basic) and has a pH of 7", 1)
            };

        internal const string measureName = "Acidity";
        internal const string powerOfHydrogenName = "PowerOfHydrogen";
    }

}
