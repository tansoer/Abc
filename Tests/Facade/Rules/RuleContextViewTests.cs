using Abc.Aids.Random;
using Abc.Facade.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Rules {

    [TestClass]
    public class RuleContextViewTests : SealedTests<RuleContextView, CommonSetIdView> {

        protected override RuleContextView createObject() => GetRandom.ObjectOf<RuleContextView>();

        [TestMethod] public void RuleIdTest() => isNullable<string>();

    }

}