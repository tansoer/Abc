using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Infra.Common;

namespace Abc.Infra.Orders {

    public sealed class SalesTaxPoliciesRepo :EntityRepo<SalesTaxPolicy, SalesTaxPolicyData>, ISalesTaxPoliciesRepo {
        public SalesTaxPoliciesRepo(OrderDb c = null) : base(c, c?.SalesTaxPolicies) { }
    }
}