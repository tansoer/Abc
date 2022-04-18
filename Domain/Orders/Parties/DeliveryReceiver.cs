using Abc.Data.Parties;

namespace Abc.Domain.Orders.Parties {
    public abstract class DeliveryReceiver :PartyInOrder {
        protected DeliveryReceiver() : this(null) { }
        protected DeliveryReceiver(PartySummaryData d) : base(d) { }
    }
}