using System;
using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Infra.Common;
using Abc.Infra.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Rules {

    [TestClass]
    public class RuleContextsRepoTests : RuleRepoTests<RuleContextsRepo, RuleContext,
        RuleContextData> {

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            obj = getObject(db);
        }

        protected override Type getBaseClass() => typeof(EntityRepo<RuleContext, RuleContextData>);

        protected override RuleContextsRepo getObject(RuleDb c) => new RuleContextsRepo(c);

        protected override DbSet<RuleContextData> getSet(RuleDb c) => c.RuleContexts;

        [TestMethod]
        public void GetRuleIdTest() {
            var d = GetRandom.ObjectOf<RuleContextData>();
            db.RuleContexts.Add(d);
            db.SaveChanges();
            var ruleId = obj.GetRuleId(d.Id);
            Assert.AreEqual(d.RuleId, ruleId);
        }

    }

}