using Abc.Data.Quantities;
using Abc.Domain.Common;

namespace Abc.Domain.Quantities {

    public abstract class UnitAttribute<TData> : BaseEntity<TData>
        where TData : UnitAttributeData, new() {
        protected UnitAttribute(TData d = null) : base(d) { }
        public string SystemOfUnitsId => Data?.SystemOfUnitsId ?? Unspecified.String;
        public SystemOfUnits SystemOfUnits => new GetFrom<ISystemsOfUnitsRepo, SystemOfUnits>().ById(SystemOfUnitsId);
        public string UnitId => Data?.UnitId ?? Unspecified.String;
        public Unit Unit => new GetFrom<IUnitsRepo, Unit>().ById(UnitId);
    }
}