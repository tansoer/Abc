using Abc.Data.Orders;

namespace Abc.Domain.Orders {
    public interface IAmendEvent :IOrderEvent {
        bool IsNewEvent { get; }
        bool IsRemoveEvent { get; }
    }
    public abstract class AmendEvent :OrderEvent, IAmendEvent {
        protected AmendEvent() : this(null) { }
        protected AmendEvent(OrderEventData d) : base(d) { }
        public abstract bool IsNewEvent { get; }
        public abstract bool IsRemoveEvent { get; }
    }

}
