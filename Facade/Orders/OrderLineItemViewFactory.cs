using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Common;

namespace Abc.Facade.Orders {
    public sealed class OrderLineItemViewFactory
        :AbstractViewFactory<OrderLineItemData, IOrderLineItem, OrderLineItemView> {
        protected internal override IOrderLineItem toObject(OrderLineItemData d) =>
            OrderLineItemFactory.Create(d);

    }
}