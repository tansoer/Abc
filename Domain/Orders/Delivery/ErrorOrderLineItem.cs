using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;

namespace Abc.Domain.Orders.Delivery {
    public sealed class ErrorOrderLineItem :OrderLineItem {
        public ErrorOrderLineItem() : this(null) { }
        public ErrorOrderLineItem(OrderLineItemData d) : base(d) { }
    }
}