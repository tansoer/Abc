using Abc.Data.Orders;

namespace Abc.Domain.Orders.Payments {
    public sealed class AcceptRefundEvent :PaymentEvent {
        public AcceptRefundEvent() : this(null) { }
        public AcceptRefundEvent(OrderEventData d) : base(d) { }
    }

}
