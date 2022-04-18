using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Facade.Common;

namespace Abc.Facade.Inventory {
    public sealed class RestockPolicyViewFactory :AbstractViewFactory<RestockPolicyData, 
        RestockPolicy, RestockPolicyView> {
        protected internal override RestockPolicy toObject(RestockPolicyData d) => new(d);
    }
}
