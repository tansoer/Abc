using Abc.Core.Units;
using Abc.Data.Common;

namespace Abc.Data.Quantities {
    public sealed class MeasureData : MetricBaseData {
        public MeasureData() { }
        public MeasureData(UnitInfo u) : this(u?.Name, u?.Code, u?.Definition, getMeasureType(u))
            => Id = (u is not null) ? u.Id : Id;
        public MeasureData(string n = null, string c = null, string d = null, MeasureType t = MeasureType.Unspecified) {
            Name = n;
            Code = c;
            Details = d;
            MeasureType = t;
        }
        internal static MeasureType getMeasureType(UnitInfo u)
            => (u is null)? MeasureType.Unspecified
            : (u?.Terms?.Count > 0) ? MeasureType.Derived 
            : MeasureType.Base;
        public MeasureType MeasureType { get; set; }
    }
}
