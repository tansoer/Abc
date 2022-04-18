using System;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Infra.Common;
using Abc.Infra.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Rules {

    [TestClass]
    public class RuleOverridesRepoTests 
        : RuleRepoTests<RuleOverridesRepo, RuleOverride, RuleOverrideData> {
        protected override Type getBaseClass() => typeof(EntityRepo<RuleOverride, RuleOverrideData>);
        protected override RuleOverridesRepo getObject(RuleDb c) => new RuleOverridesRepo(c);
        protected override DbSet<RuleOverrideData> getSet(RuleDb c) => c.RuleOverrides;
    }
}