using Abc.Aids.Random;
using Abc.Facade.Common;
using Abc.Facade.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Rules {

    [TestClass]
    public class RuleOverrideViewTests : SealedTests<RuleOverrideView, PartySignatureBaseView> {

        protected override RuleOverrideView createObject() => GetRandom.ObjectOf<RuleOverrideView>();
        [TestMethod] public void RuleIdTest() => isNullableProperty<string>("Rule");

        [TestMethod] public void RuleSetIdTest() => isNullableProperty<string>("Rule Set");

        [TestMethod] public void RuleContextIdTest() => isNullableProperty<string>("Context");

        [TestMethod] public void VariableIdTest() => isNullableProperty<string>("Variable");

    }

}