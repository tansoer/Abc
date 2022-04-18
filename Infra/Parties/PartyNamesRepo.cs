using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {

    public sealed class PartyNamesRepo: PagedRepo<PartyName, PartyNameData>,
        IPartyNamesRepo {
        public PartyNamesRepo(PartyDb c = null) : base(c, c?.PartyNames) { }
        protected internal override PartyName toDomainObject(PartyNameData d) 
            => PartyNameFactory.Create(d);
    }
}

