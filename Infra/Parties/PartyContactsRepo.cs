using Abc.Data.Parties;
using Abc.Domain.Parties.Contacts;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {

    public sealed class PartyContactsRepo : PagedRepo<IPartyContact, PartyContactData>,
        IPartyContactsRepo {

        public PartyContactsRepo(PartyDb c = null) : base(c, c?.PartyContacts) { }

        protected internal override IPartyContact toDomainObject(PartyContactData d) => PartyContactFactory.Create(d);

    }

}
