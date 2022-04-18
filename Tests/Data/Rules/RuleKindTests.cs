using Abc.Aids.Reflection;
using Abc.Data.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Rules {

    [TestClass]
    public class RuleKindTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(RuleKind);

        [TestMethod]
        public void CountTest()
            => Assert.AreEqual(3, GetEnum.Count<RuleKind>());

        [TestMethod]
        public void UnspecifiedTest() =>
            Assert.AreEqual(0, (int) RuleKind.Unspecified);

        [TestMethod]
        public void RuleTest() =>
            Assert.AreEqual(1, (int) RuleKind.Rule);

        [TestMethod]
        public void ActivityRuleTest() =>
            Assert.AreEqual(2, (int) RuleKind.ActivityRule);

    }

}
