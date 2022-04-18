using Abc.Data.Orders;

namespace Abc.Domain.Orders {
    public sealed class UnspecifiedOrderEvent :OrderEvent {
        public UnspecifiedOrderEvent() : this(null) { }
        public UnspecifiedOrderEvent(OrderEventData d) : base(d) { }
    }

}
