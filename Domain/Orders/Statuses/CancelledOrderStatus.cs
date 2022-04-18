using Abc.Data.Orders;

namespace Abc.Domain.Orders.Statuses {
    public sealed class CancelledOrderStatus :OrderStatus {
        public CancelledOrderStatus() : this(null) { }
        public CancelledOrderStatus(OrderStatusData d) : base(d) { }
    }
}