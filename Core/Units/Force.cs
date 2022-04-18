using System.Collections.Generic;

namespace Abc.Core.Units {
    public static class Force {
        public static UnitInfo Measure = new( forceName
            , forceCode
            , null
            , forceDef
            , new TermInfo(Mass.Measure.Id, 1)
            , new TermInfo(Distance.Measure.Id, 1)
            , new TermInfo(Time.Measure.Id, -2)
        );
        public static List<UnitInfo> Units =>
            new() {
                new UnitInfo(newtonName
                    , newtonCode
                    , null
                    , newtonDef
                    , new TermInfo(Mass.kilogramsName, 1)
                    , new TermInfo(Distance.metersName, 1)
                    , new TermInfo(Time.secondsName, -2)
            ) };
        internal const string forceName = "Force";
        internal const string forceCode = "F";
        internal const string forceDef = "A force is a push or pull upon an object resulting "
            +"from the object's interaction with another object.";
        internal const string newtonName = "Newton";
        internal const string newtonCode = "N";
        internal const string newtonDef = "The newton is the International System of "
            + "Units (SI) derived unit of force. " 
            + "It is defined as 1 kg⋅m/s2, the force which gives a mass of 1 kilogram an " 
            + "acceleration of 1 metre per second per second.";
    }
}