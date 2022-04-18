using Abc.Core.Units;
using Abc.Data.Common;

namespace Abc.Data.Quantities {
    public sealed class UnitData : MetricBaseData {
        public UnitData() { }
        public UnitData(string measureId, string n = null, string c = null, string d = null, UnitType t = UnitType.Unspecified) {
            MeasureId = measureId;
            Name = n;
            Code = c;
            Details = d;
            UnitType = t;
        }
        public UnitData(UnitInfo u, string measureId)
            : this(measureId, u?.Name, u?.Code, u?.Definition, getUnitType(u)) 
            => Id = (u is not null) ? u.Id : Id;
        public string MeasureId { get; set; }
        public UnitType UnitType { get; set; }
        internal static UnitType getUnitType(UnitInfo unit)
           => (unit?.Terms?.Count > 0) ? UnitType.Derived 
            : (unit?.Factor > 0) ? UnitType.Factored 
            : UnitType.Functioned;
    }
}
