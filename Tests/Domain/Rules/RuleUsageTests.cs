using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Rules {
    [TestClass]
    public class RuleUsageTests : SealedTests<RuleUsage, Entity<RuleUsageData>> {

        protected override RuleUsage createObject() => new RuleUsage(GetRandom.ObjectOf<RuleUsageData>());

        [TestMethod] public void RuleIdTest() => isReadOnly(obj?.Data?.RuleId);

        [TestMethod] public void RuleSetIdTest() => isReadOnly(obj?.Data?.RuleSetId);

        [TestMethod]
        public void RuleTest() {
            var d = GetRandom.ObjectOf<RuleData>();
            d.Id = obj.RuleId;
            GetRepo.Instance<IRulesRepo>().Add(new Rule(d));
            var t = isReadOnly() as BaseRule;
            Assert.IsNotNull(t);
            allPropertiesAreEqual(d, t.Data);
        }

        [TestMethod]
        public void RuleSetTest() {
            var d = GetRandom.ObjectOf<RuleSetData>();
            d.Id = obj.RuleSetId;
            GetRepo.Instance<IRuleSetsRepo>().Add(new RuleSet(d));
            var t = isReadOnly() as RuleSet;
            Assert.IsNotNull(t);
            allPropertiesAreEqual(d, t.Data);
        }
    }
}
