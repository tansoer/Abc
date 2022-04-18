using Abc.Pages.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Rules {

    [TestClass]
    public class RuleUrlsTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(RuleUrls);
        [TestMethod] public void RuleSetsTest() => Assert.AreEqual("/Rules/Sets", RuleUrls.RuleSets);
        [TestMethod] public void RulesTest() => Assert.AreEqual("/Rules/Rules", RuleUrls.Rules);
        [TestMethod] public void RuleElementsTest() => Assert.AreEqual("/Rules/Elements", RuleUrls.RuleElements);
        [TestMethod] public void UsagesTest() => Assert.AreEqual("/Rules/Usages", RuleUrls.Usages);
        [TestMethod] public void ContextsTest() => Assert.AreEqual("/Rules/Contexts", RuleUrls.Contexts);
        [TestMethod] public void OverridesTest() => Assert.AreEqual("/Rules/Overrides", RuleUrls.Overrides);
     

    }

}