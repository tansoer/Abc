using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;

namespace Abc.Domain.Roles {
    public abstract class BasePartySummary :BasePartyAttribute<PartySummaryData>, IPartySummary {
        protected BasePartySummary() : this(null) { }
        protected BasePartySummary(PartySummaryData d) : base(d) { }
        public string Address => get(Data?.Address);
        public string PhoneNumber => get(Data?.PhoneNumber);
        public string EmailAddress => get(Data?.EmailAddress);
        public PartyRole Role => item<IPartyRolesRepo, PartyRole>(partyRoleId);
        internal string partyRoleId => get(Data?.PartyRoleId);
    }
}


