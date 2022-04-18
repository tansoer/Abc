using Abc.Data.Orders;

namespace Abc.Domain.Orders.Statuses {
    public sealed class OpenOrderStatus :OrderStatus {
        public OpenOrderStatus() : this(null) { }
        public OpenOrderStatus(OrderStatusData d) : base(d) { }

        protected internal override bool cancel(ICancelOrderEvent e)
            => changeStatus(e, OrderLifecycleStatus.Cancelling);

        protected internal override bool close(ICloseOrderEvent e) 
            => Order.IsClosable() && changeStatus(e, OrderLifecycleStatus.Closed);
    }
}