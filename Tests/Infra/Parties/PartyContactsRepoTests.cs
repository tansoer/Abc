using System;
using Abc.Aids.Enums;
using Abc.Data.Parties;
using Abc.Domain.Parties.Contacts;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class PartyContactsRepoTests : PartyRepoTests<PartyContactsRepo, IPartyContact,
        PartyContactData> {

        protected override Type getBaseClass()
            => typeof(PagedRepo<IPartyContact, PartyContactData>);

        protected override PartyContactsRepo getObject(PartyDb c) =>
            new PartyContactsRepo(c);

        protected override DbSet<PartyContactData> getSet(PartyDb c) => c.PartyContacts;
        protected override IPartyContact getDomainObject(PartyContactData d) {
            if (d.ContactType == ContactType.Unspecified) d.ContactType = ContactType.Email;
            return obj.toDomainObject(d);
        }

    }

}