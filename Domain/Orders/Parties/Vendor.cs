using Abc.Data.Parties;

namespace Abc.Domain.Orders.Parties {
    public sealed class Vendor: PartyInOrder {
        public Vendor() : this(null) { }
        public Vendor(PartySummaryData d) : base(d){}
    }
}