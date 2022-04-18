using Abc.Data.Inventory;
using Abc.Domain.Common;

namespace Abc.Domain.Inventory {
    public sealed class RestockPolicy :Entity<RestockPolicyData> {
        public RestockPolicy(): this(null) { }
        public RestockPolicy(RestockPolicyData d): base(d) { }
    }
}
