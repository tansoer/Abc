using Abc.Aids.Random;
using Abc.Facade.Common;
using Abc.Facade.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Rules {

    [TestClass] public class CommonRuleIdViewTests: 
        AbstractTests<CommonRuleIdView, EntityBaseView> {
        private class testClass : CommonRuleIdView { }
        protected override CommonRuleIdView createObject() 
            => GetRandom.ObjectOf<testClass>();
        [TestMethod] public void RuleIdTest() => isNullableProperty<string>("Rule");
    }
}