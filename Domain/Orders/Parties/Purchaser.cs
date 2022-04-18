using Abc.Data.Parties;

namespace Abc.Domain.Orders.Parties {
    public sealed class Purchaser :PartyInOrder {
        public Purchaser() : this(null) { }
        public Purchaser(PartySummaryData d) : base(d) { }
    }
}