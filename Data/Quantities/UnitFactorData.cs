using Abc.Core.Units;
namespace Abc.Data.Quantities {
    public sealed class UnitFactorData : UnitAttributeData {
        public UnitFactorData() {}
        public UnitFactorData(UnitInfo u, string systemOfUnitsId = null) {
            SystemOfUnitsId = systemOfUnitsId;
            UnitId = u?.Id;
            Factor = u?.Factor?? double.NaN;
        }
        public double Factor { get; set; }
    }
}
