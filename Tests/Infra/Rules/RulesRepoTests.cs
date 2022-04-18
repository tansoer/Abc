using System;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Infra.Common;
using Abc.Infra.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Rules {

    [TestClass]
    public class RulesRepoTests : RuleRepoTests<RulesRepo, BaseRule,
        RuleData> {
        protected override Type getBaseClass() => typeof(PagedRepo<BaseRule, RuleData>);
        protected override RulesRepo getObject(RuleDb c) => new RulesRepo(c);
        protected override DbSet<RuleData> getSet(RuleDb c) => c.Rules;
    }
}