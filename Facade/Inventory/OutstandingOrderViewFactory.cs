using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Facade.Common;

namespace Abc.Facade.Inventory {
    public sealed class OutstandingOrderViewFactory :AbstractViewFactory<OutstandingOrderData, 
        OutstandingOrder, OutstandingOrderView> {
        protected internal override OutstandingOrder toObject(OutstandingOrderData d) => new(d);
    }
}
