using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Facade.Common;

namespace Abc.Facade.Orders {
    public sealed class SalesTaxPolicyViewFactory
        :AbstractViewFactory<SalesTaxPolicyData, SalesTaxPolicy, SalesTaxPolicyView> {
        protected internal override SalesTaxPolicy toObject(SalesTaxPolicyData d) =>
            new(d);
    }
}