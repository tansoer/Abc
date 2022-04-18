using Abc.Aids.Random;
using Abc.Facade.Common;
using Abc.Facade.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Rules {

    [TestClass] public class CommonSetIdViewTests: 
        AbstractTests<CommonSetIdView, EntityBaseView> {
        private class testClass : CommonSetIdView { }
        protected override CommonSetIdView createObject() 
            => GetRandom.ObjectOf<testClass>();
        [TestMethod] public void RuleSetIdTest() => isNullableProperty<string>("Rule Set");
    }
}