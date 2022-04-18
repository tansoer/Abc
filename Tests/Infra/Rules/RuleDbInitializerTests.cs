using System.Linq;
using Abc.Infra.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Rules {
    [TestClass] public class RuleDbInitializerTests :DbInitializerTests<RuleDb> {
        public RuleDbInitializerTests() {
            type = typeof(RuleDbInitializer);
            db = new RuleDb(options);
            removeAll(db.Rules);
            removeAll(db.RuleContexts);
            removeAll(db.RuleElements);
            removeAll(db.RuleOverrides);
            removeAll(db.RuleSets);
            removeAll(db.RuleUsages);
            RuleDbInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void RulesTest() => areEqual(6, db.Rules.Count());
        [TestMethod] public void RuleContextsTest() => areEqual(0, db.RuleContexts.Count());
        [TestMethod] public void RuleElementsTest() => areEqual(22, db.RuleElements.Count());
        [TestMethod] public void RuleOverridesTest() => areEqual(0, db.RuleOverrides.Count());
        [TestMethod] public void RuleSetsTest() => areEqual(1, db.RuleSets.Count());
        [TestMethod] public void RuleUsagesTest() => areEqual(6, db.RuleUsages.Count());
    }
}
