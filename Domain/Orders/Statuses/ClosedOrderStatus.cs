using Abc.Data.Orders;

namespace Abc.Domain.Orders.Statuses {
    public sealed class ClosedOrderStatus :OrderStatus {
        public ClosedOrderStatus() : this(null) { }
        public ClosedOrderStatus(OrderStatusData d) : base(d) { }
    }
}