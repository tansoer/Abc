using System;
using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class BodyMetricTypesRepoTests : PartyRepoTests<BodyMetricTypesRepo, BodyMetricType,
        BodyMetricTypeData> {

        protected override Type getBaseClass()
            => typeof(EntityRepo<BodyMetricType, BodyMetricTypeData>);

        protected override BodyMetricTypesRepo getObject(PartyDb c) =>
            new BodyMetricTypesRepo(c);

        protected override DbSet<BodyMetricTypeData> getSet(PartyDb c) => c.BodyMetricTypes;

    }

}