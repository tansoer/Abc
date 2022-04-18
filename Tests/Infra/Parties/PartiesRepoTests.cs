using System;
using Abc.Data.Parties;
using Abc.Domain.Parties;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class PartiesRepoTests : PartyRepoTests<PartiesRepo, IParty,
        PartyData> {
        protected override Type getBaseClass()
            => typeof(PagedRepo<IParty, PartyData>);
        protected override PartiesRepo getObject(PartyDb c) =>
            new PartiesRepo(c);
        protected override DbSet<PartyData> getSet(PartyDb c) => c.Parties;
    }
}