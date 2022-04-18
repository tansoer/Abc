using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;

namespace Abc.Domain.Orders.Delivery {
    public sealed class RejectedItem :OrderLineItem {
        public RejectedItem() : this(null) { }
        public RejectedItem(OrderLineItemData d) : base(d) { }
    }
}