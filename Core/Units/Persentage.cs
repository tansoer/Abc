using System.Collections.Generic;

namespace Abc.Core.Units {

    public static class Persentage {

        public static UnitInfo Measure = new UnitInfo(percentageName);

        public static List<UnitInfo> Units =>
            new List<UnitInfo> {
                new UnitInfo(percentageName, "%", 1)
            };
        internal const string percentageName = "Percentage";

    }

}
