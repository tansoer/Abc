using System;
using Abc.Data.Parties;
using Abc.Domain.Roles;
using Abc.Facade.Parties;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class PartySummariesRepoTests :PartyRepoTests<PartySummariesRepo, IPartySummary, PartySummaryData> {
        protected override Type getBaseClass()
            => typeof(PagedRepo<IPartySummary, PartySummaryData>);

        protected override PartySummariesRepo getObject(PartyDb c) => new(c);
        protected override DbSet<PartySummaryData> getSet(PartyDb c) => c.PartySummaries;
    }
}