using System;
using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class BodyMetricsRepoTests : PartyRepoTests<BodyMetricsRepo, IBodyMetric,
        BodyMetricData> {

        protected override Type getBaseClass()
            => typeof(PagedRepo<IBodyMetric, BodyMetricData>);

        protected override BodyMetricsRepo getObject(PartyDb c) =>
            new BodyMetricsRepo(c);

        protected override DbSet<BodyMetricData> getSet(PartyDb c) => c.BodyMetrics;

    }

}