using Abc.Data.Orders;
using Abc.Domain.Orders.Payments;

namespace Abc.Domain.Orders.Payments {
    public sealed class MakePaymentEvent :PaymentEvent {
        public MakePaymentEvent() : this(null) { }
        public MakePaymentEvent(OrderEventData d) : base(d) { }
    }

}
