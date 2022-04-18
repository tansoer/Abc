using Abc.Data.Parties;
using Abc.Domain.Orders.Parties;

namespace Abc.Domain.Roles {
    public static class PartySummaryFactory {
        public static IPartySummary Create(PartySummaryData d) {
            if (d?.IsPartyRoleInOrder??false) return PartyInOrderFactory.Create(d);
            return new PartySummary(d);
        }
    }
}
