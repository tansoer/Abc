using Abc.Data.Orders;

namespace Abc.Domain.Orders.Statuses {
    public sealed class CloseOrderEvent :LifecycleEvent, ICloseOrderEvent {
        public CloseOrderEvent() : this(null) { }
        public CloseOrderEvent(OrderEventData d) : base(d) { }
    }

    public interface ICloseOrderEvent :ILifecycleEvent {
    }
}
