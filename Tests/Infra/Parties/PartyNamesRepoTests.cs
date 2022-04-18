using System;
using Abc.Aids.Enums;
using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {
    [TestClass]
    public class PartyNamesRepoTests : PartyRepoTests<PartyNamesRepo, PartyName,
        PartyNameData> {
        protected override Type getBaseClass() => typeof(PagedRepo<PartyName, PartyNameData>);
        protected override PartyNamesRepo getObject(PartyDb c) => new PartyNamesRepo(c);
        protected override DbSet<PartyNameData> getSet(PartyDb c) => c.PartyNames;
        protected override PartyName getDomainObject(PartyNameData d) {
            if (d.PartyType == PartyType.Unspecified) d.PartyType = PartyType.Organization;
            return obj.toDomainObject(d);
        }
    }
}