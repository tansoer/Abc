using Abc.Data.Orders;

namespace Abc.Domain.Orders.Statuses {
    public abstract class LifecycleEvent :OrderEvent, ILifecycleEvent {
        protected LifecycleEvent() : this(null) { }
        protected LifecycleEvent(OrderEventData d) : base(d) { }
    }

    public interface ILifecycleEvent :IOrderEvent {
    }
}
