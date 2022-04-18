using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Infra.Common;
using Abc.Infra.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Rules {

    [TestClass]
    public class RuleUsagesRepoTests 
        :RuleRepoTests<RuleUsagesRepo, RuleUsage, RuleUsageData> {
        protected override Type getBaseClass() => typeof(EntityRepo<RuleUsage, RuleUsageData>);
        protected override RuleUsagesRepo getObject(RuleDb c) => new RuleUsagesRepo(c);
        protected override DbSet<RuleUsageData> getSet(RuleDb c) => c.RuleUsages;
    }
}