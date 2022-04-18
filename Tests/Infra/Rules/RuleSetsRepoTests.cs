using System;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Infra.Common;
using Abc.Infra.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Rules {

    [TestClass]
    public class RuleSetsRepoTests : RuleRepoTests<RuleSetsRepo, IRuleSet,
        RuleSetData> {

        protected override Type getBaseClass() => typeof(PagedRepo<IRuleSet, RuleSetData>);

        protected override RuleSetsRepo getObject(RuleDb c) => new(c);

        protected override DbSet<RuleSetData> getSet(RuleDb c) => c.RuleSets;

    }

}