using Abc.Data.Parties;

namespace Abc.Domain.Orders.Parties {
    public sealed class PaymentReceiver :PartyInOrder {
        public PaymentReceiver() : this(null) { }
        public PaymentReceiver(PartySummaryData d) : base(d) { }
    }
}