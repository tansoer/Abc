using System.Collections.Generic;

namespace Abc.Core.Units {

    public static class Area {
        public static UnitInfo Measure = new(areaName, areaCode, null, areaDef,
            new TermInfo(Distance.Measure.Id, 2) );

        public static List<UnitInfo> Units =>
            new List<UnitInfo> {
                new UnitInfo(acresName,new TermInfo(Distance.furlongsName), new TermInfo(Distance.chainsName)),
                new UnitInfo(aresName,new TermInfo(Distance.decametersName, 2)),
                new UnitInfo(centiaresName,new TermInfo(Distance.metersName, 2)),
                new UnitInfo(hectaresName,new TermInfo(Distance.hectometersName, 2)),
                new UnitInfo(squareCentimetersName,new TermInfo(Distance.centimetersName, 2)),
                new UnitInfo(squareDecametersName,new TermInfo(Distance.decametersName, 2)),
                new UnitInfo(squareDecimetersName,new TermInfo(Distance.decimetersName, 2)),
                new UnitInfo(squareFeetName,new TermInfo(Distance.feetName, 2)),
                new UnitInfo(squareHectometersName,new TermInfo(Distance.hectometersName, 2)),
                new UnitInfo(squareInchesName,new TermInfo(Distance.inchesName, 2)),
                new UnitInfo(squareKilometersName,new TermInfo(Distance.kilometersName, 2)),
                new UnitInfo(squareMetersName, new TermInfo(Distance.metersName, 2)),
                new UnitInfo(squareMilesName,new TermInfo(Distance.milesName, 2)),
                new UnitInfo(squareMillimetersName,new TermInfo(Distance.millimetersName, 2)),
                new UnitInfo(squareRodsName,new TermInfo(Distance.rodsName, 2)),
                new UnitInfo(squareYardsName, new TermInfo(Distance.yardsName, 2))
            };
        internal const string areaName = "Area";
        internal const string areaCode = "A";
        internal const string areaDef = "Area is the quantity that expresses the extent of a " +
                         "two-dimensional figure or shape or planar lamina, in the plane.";
        internal const string acresName = "Acres";
        internal const string aresName = "Ares";
        internal const string centiaresName = "Centiares";
        internal const string hectaresName = "Hectares";
        internal const string squareCentimetersName = "SquareCentimeters";
        internal const string squareDecimetersName = "SquareDecimeters";
        internal const string squareDecametersName = "SquareDecameters";
        internal const string squareFeetName = "SquareFeet";
        internal const string squareHectometersName = "SquareHectometers";
        internal const string squareInchesName = "SquareInches";
        internal const string squareKilometersName = "SquareKilometers";
        internal const string squareMetersName = "SquareMeters";
        internal const string squareMilesName = "SquareMiles";
        internal const string squareMillimetersName = "SquareMillimeters";
        internal const string squareRodsName = "SquareRods";
        internal const string squareYardsName = "SquareYards";
    }
}
