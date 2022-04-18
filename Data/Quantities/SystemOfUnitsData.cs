using Abc.Core.Units;
using Abc.Data.Common;

namespace Abc.Data.Quantities {

    public sealed class SystemOfUnitsData : EntityBaseData {
        public SystemOfUnitsData() { }
        public SystemOfUnitsData(UnitInfo u) {
            Id = u?.Id;
            Code = u?.Code;
            Name = u?.Name;
            Details = u?.Definition;
        }
    }
}
