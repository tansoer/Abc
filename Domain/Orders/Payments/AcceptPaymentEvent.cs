using Abc.Data.Orders;

namespace Abc.Domain.Orders.Payments {
    public sealed class AcceptPaymentEvent :PaymentEvent {
        public AcceptPaymentEvent() : this(null) { }
        public AcceptPaymentEvent(OrderEventData d) : base(d) { }
    }

}
