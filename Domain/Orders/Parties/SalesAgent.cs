using Abc.Data.Parties;

namespace Abc.Domain.Orders.Parties {
    public sealed class SalesAgent :PartyInOrder {
        public SalesAgent() : this(null) { }
        public SalesAgent(PartySummaryData d) : base(d) { }
    }
}