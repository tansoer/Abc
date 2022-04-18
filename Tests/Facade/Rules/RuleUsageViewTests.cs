using Abc.Aids.Random;
using Abc.Facade.Common;
using Abc.Facade.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Rules {

    [TestClass]
    public class RuleUsageViewTests : SealedTests<RuleUsageView, EntityBaseView> {
        protected override RuleUsageView createObject() => GetRandom.ObjectOf<RuleUsageView>();
        [TestMethod] public void RuleIdTest() => isNullableProperty<string>("Rule");
        [TestMethod] public void RuleSetIdTest() => isNullableProperty<string>("Rule Set");
    }
}