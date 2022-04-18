using Abc.Data.Orders;

namespace Abc.Domain.Orders.Statuses {
    public sealed class OpenOrderEvent :LifecycleEvent, IOpenOrderEvent {
        public OpenOrderEvent() : this(null) { }
        public OpenOrderEvent(OrderEventData d) : base(d) { }
    }

    public interface IOpenOrderEvent :ILifecycleEvent {
    }
}
