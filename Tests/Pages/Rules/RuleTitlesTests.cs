using Abc.Pages.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Rules {
    [TestClass]
    public class RuleTitlesTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(RuleTitles);
        [TestMethod] public void RuleSetsTest() => areEqual("Rule sets", RuleTitles.RuleSets);
        [TestMethod] public void RulesTest() => areEqual("Rules", RuleTitles.Rules);
        [TestMethod] public void RuleElementsTest() => areEqual("Rule elements", RuleTitles.RuleElements);
        [TestMethod] public void UsagesTest() => areEqual("Rule usages", RuleTitles.Usages);
        [TestMethod] public void ContextsTest() => areEqual("Rule contexts", RuleTitles.Contexts);
        [TestMethod] public void OverridesTest() => areEqual("Rule overrides", RuleTitles.Overrides);
    }
}
