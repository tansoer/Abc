using Abc.Data.Parties;

namespace Abc.Domain.Orders.Parties {
    public sealed class OrderInitiator :PartyInOrder {
        public OrderInitiator() : this(null) { }
        public OrderInitiator(PartySummaryData d) : base(d) { }
    }
}