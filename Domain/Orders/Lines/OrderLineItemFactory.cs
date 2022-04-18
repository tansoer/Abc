using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;

namespace Abc.Domain.Orders.Lines {
    public static class OrderLineItemFactory {
        public static IOrderLineItem Create(OrderLineItemData d) =>
             d?.OrderLineType switch {
                 OrderLineType.ProductLine => new OrderedItem(d),
                 OrderLineType.DispatchLine => new RejectedItem(d),
                 OrderLineType.ReceiptLine => new RejectedItem(d),
                 _ => new ErrorOrderLineItem(d)
             };
    }
}
