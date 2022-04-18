using Abc.Data.Quantities;
using Abc.Domain.Common;

namespace Abc.Domain.Quantities {
    public sealed class SystemOfUnits : Entity<SystemOfUnitsData> {
        public SystemOfUnits() : this(null) { }
        public SystemOfUnits(SystemOfUnitsData d) : base(d) { }
    }
}