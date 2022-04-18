using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;

namespace Abc.Domain.Orders.Delivery {
    public sealed class OrderedItem :OrderLineItem {
        public OrderedItem() : this(null) { }
        public OrderedItem(OrderLineItemData d) : base(d) { }
    }
}