using Abc.Data.Common;

namespace Abc.Data.Quantities {

    public abstract class UnitAttributeData :BaseData {
        public string UnitId { get; set; }
        public string SystemOfUnitsId { get; set; }
    }
}