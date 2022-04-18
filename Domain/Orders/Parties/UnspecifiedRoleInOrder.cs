using Abc.Data.Parties;

namespace Abc.Domain.Orders.Parties {
    public sealed class UnspecifiedRoleInOrder :PartyInOrder {
        public UnspecifiedRoleInOrder() : this(null) { }
        public UnspecifiedRoleInOrder(PartySummaryData d) : base(d) { }
    }
}