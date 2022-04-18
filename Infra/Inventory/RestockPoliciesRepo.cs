using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Infra.Common;

namespace Abc.Infra.Inventory {
    public sealed class RestockPoliciesRepo:
        PagedRepo<RestockPolicy, RestockPolicyData>, IRestockPoliciesRepo{
        public RestockPoliciesRepo(InventoryDb c = null) : base(c, c?.RestockPolicies) { }
        protected internal override RestockPolicy toDomainObject(RestockPolicyData d)
            => new RestockPolicy(d);
    }
}