using Abc.Data.Orders;
using Abc.Domain.Common;
using System;

namespace Abc.Domain.Orders.Statuses {

    public interface IOrderStatus: IEntity<OrderStatusData> {
        public event EventHandler<IOrderStatus> OnStatusChanged;
        public string OrderId { get; }
        public string OrderEventId { get; }
        public OrderLifecycleStatus Status { get; }
        public bool ProcessOrderEvent(ILifecycleEvent e);
        bool IsInitialized();
    }
    public interface IOrderStatusesRepo :IRepo<IOrderStatus> { }
    public abstract class OrderStatus :Entity<OrderStatusData>, IOrderStatus {
        internal IOrder order;
        protected OrderStatus() :this(null) { }
        protected OrderStatus(OrderStatusData d) :base(d) { }
        public OrderLifecycleStatus Status
            => Data?.OrderStatus ?? OrderLifecycleStatus.Unspecified;
        public string OrderId => get(Data?.OrderId);
        public IOrder Order => order ?? item<IOrdersRepo, IOrder>(OrderId);
        public string OrderEventId => get(Data?.OrderEventId);
        public bool ProcessOrderEvent(ILifecycleEvent e)
            => e switch {
                IOpenOrderEvent x => open(x),
                ICloseOrderEvent x => close(x),
                ICancelOrderEvent x => cancel(x),
                _ => error(e)
            };
        public event EventHandler<IOrderStatus> OnStatusChanged;
        protected internal virtual bool error(ILifecycleEvent e) => false;
        protected internal virtual bool open(IOpenOrderEvent e) => error(e);
        protected internal virtual bool close(ICloseOrderEvent e) => error(e);
        protected internal virtual bool cancel(ICancelOrderEvent e) => error(e);

        protected internal void doOnStatusChanged(object sender, IOrderStatus s) 
            => OnStatusChanged?.Invoke(sender, s);
        protected internal bool changeStatus(IOrderEvent e, OrderLifecycleStatus s) {
            var d = new OrderStatusData {
                OrderStatus = s,
                OrderId = OrderId,
                OrderEventId = e.Id,
                ValidFrom = DateTime.Now,
            };
            doOnStatusChanged(this, OrderStatusFactory.Create(d));
            return true;
        }

        public bool IsInitialized() => Status == OrderLifecycleStatus.Initialized;
    }
}