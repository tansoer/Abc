using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {

    public sealed class PartyNameUsesRepo : EntityRepo<PartyNameUse, PartyNameUseData>,
        IPartyNameUsesRepo {
        public PartyNameUsesRepo(PartyDb c = null) : base(c, c?.PartyNameUses) { }
    }
}