using System;
using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class PartyNameUsesRepoTests : PartyRepoTests<PartyNameUsesRepo,
        PartyNameUse, PartyNameUseData> {
        protected override Type getBaseClass()
            => typeof(EntityRepo<PartyNameUse, PartyNameUseData>);
        protected override PartyNameUsesRepo getObject(PartyDb c) =>
            new PartyNameUsesRepo(c);
        protected override DbSet<PartyNameUseData> getSet(PartyDb c) => c.PartyNameUses;
    }
}