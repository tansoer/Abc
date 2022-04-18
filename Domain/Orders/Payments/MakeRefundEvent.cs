using Abc.Data.Orders;

namespace Abc.Domain.Orders.Payments {
    public sealed class MakeRefundEvent :PaymentEvent {
        public MakeRefundEvent() : this(null) { }
        public MakeRefundEvent(OrderEventData d) : base(d) { }
    }

}
