using Abc.Data.Parties;
using Abc.Domain.Parties.Contacts;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {

    public sealed class PartyContactUsagesRepo :EntityRepo<PartyContactUsage, PartyContactUsageData>,
        IPartyContactUsagesRepo {
        public PartyContactUsagesRepo(PartyDb c = null) : base(c, c?.PartyContactUsages) { }
    }
}