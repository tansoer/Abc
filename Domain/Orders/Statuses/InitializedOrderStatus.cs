using Abc.Data.Orders;

namespace Abc.Domain.Orders.Statuses {
    public sealed class InitializedOrderStatus:OrderStatus {
        public InitializedOrderStatus() : this(null) { }
        public InitializedOrderStatus(OrderStatusData d) : base(d) { }
        protected internal override bool open(IOpenOrderEvent e)
            => changeStatus(e, OrderLifecycleStatus.Open);
        protected internal override bool cancel(ICancelOrderEvent e) 
            => changeStatus(e, OrderLifecycleStatus.Cancelled);
        protected internal override bool close(ICloseOrderEvent e)
            => changeStatus(e, OrderLifecycleStatus.Cancelled);
    }
}