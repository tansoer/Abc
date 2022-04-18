using Abc.Data.Orders;

namespace Abc.Domain.Orders.Statuses
{
    public static class OrderStatusFactory {
        public static IOrderStatus Create(OrderStatusData d)
            => d?.OrderStatus switch {
                OrderLifecycleStatus.Initialized => new InitializedOrderStatus(d),
                OrderLifecycleStatus.Open => new OpenOrderStatus(d),
                OrderLifecycleStatus.Closed => new ClosedOrderStatus(d),
                OrderLifecycleStatus.Cancelling => new CancellingOrderStatus(d),
                OrderLifecycleStatus.Cancelled => new CancelledOrderStatus(d),
                OrderLifecycleStatus.Unspecified => new UnspecifiedOrderStatus(d),
                null => new UnspecifiedOrderStatus(),
                _ => new UnspecifiedOrderStatus()
            };
    }
}