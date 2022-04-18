using System.Collections.Generic;

namespace Abc.Core.Units {
    public static class Acceleration {
        public static UnitInfo Measure = new (
            acceleratorName,
            acceleratorCode,
            acceleratorName,
            acceleratorDef,
            new TermInfo(Distance.Measure.Id, 1), 
            new TermInfo(Time.Measure.Id, -2)
        );
        public static List<UnitInfo> Units => new() {
                new UnitInfo(metersPerSquareSecondName
                    , metersPerSquareSecondCode
                    , metersPerSquareSecondName
                    , metersPerSquareSecondDef
                    , new TermInfo(Distance.metersName, 1)
                    , new TermInfo(Time.secondsName, -2) 
        ) };
        internal const string acceleratorName = "Acceleration";
        internal const string acceleratorCode = "a";
        internal const string acceleratorDef = "Acceleration is the change of velocity divided by the change of time.";
        internal const string metersPerSquareSecondName = "Meters per square second";
        internal const string metersPerSquareSecondCode = "m/s^2";
        internal const string metersPerSquareSecondDef = "The meter per second squared is the Standard International"
            +" ( SI ) unit of acceleration vector magnitude.";
    }
}