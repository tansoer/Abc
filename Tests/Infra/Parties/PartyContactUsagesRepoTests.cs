using System;
using Abc.Data.Parties;
using Abc.Domain.Parties.Contacts;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class PartyContactUsagesRepoTests : PartyRepoTests<PartyContactUsagesRepo,
        PartyContactUsage, PartyContactUsageData> {

        protected override Type getBaseClass()
            => typeof(EntityRepo<PartyContactUsage, PartyContactUsageData>);

        protected override PartyContactUsagesRepo getObject(PartyDb c) =>
            new PartyContactUsagesRepo(c);

        protected override DbSet<PartyContactUsageData> getSet(PartyDb c) => c.PartyContactUsages;

    }

}