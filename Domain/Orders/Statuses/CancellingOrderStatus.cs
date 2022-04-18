using Abc.Data.Orders;

namespace Abc.Domain.Orders.Statuses {
    public sealed class CancellingOrderStatus :OrderStatus {
        public CancellingOrderStatus() : this(null) { }
        public CancellingOrderStatus(OrderStatusData d) :base(d) { }

        private bool hasOrder => Order is not null && !Order.IsUnspecified ;
        private bool isCancellable => Order?.IsCancellable() ?? false;

        protected internal override bool cancel(ICancelOrderEvent e)
            => isCancellable && changeStatus(e, OrderLifecycleStatus.Cancelled);
        protected internal override bool close(ICloseOrderEvent e)
            => isCancellable && changeStatus(e, OrderLifecycleStatus.Cancelled);
        protected internal override bool open(IOpenOrderEvent e)
            => hasOrder && (!isCancellable) && changeStatus(e, OrderLifecycleStatus.Open);
    }
}