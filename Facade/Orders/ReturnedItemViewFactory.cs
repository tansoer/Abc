using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Facade.Common;

namespace Abc.Facade.Orders {
    public sealed class ReturnedItemViewFactory
        :AbstractViewFactory<ReturnedItemData, ReturnedItem, ReturnedItemView> {
        protected internal override ReturnedItem toObject(ReturnedItemData d) =>
            new(d);
    }
}