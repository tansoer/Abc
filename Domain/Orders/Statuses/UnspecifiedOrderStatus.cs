using Abc.Data.Orders;

namespace Abc.Domain.Orders.Statuses
{
    public sealed class UnspecifiedOrderStatus :OrderStatus {
        public UnspecifiedOrderStatus() : this(null) { }
        public UnspecifiedOrderStatus(OrderStatusData d) : base(d) { }
    }
}