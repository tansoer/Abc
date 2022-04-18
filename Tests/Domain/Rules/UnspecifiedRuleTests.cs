using Abc.Aids.Random;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Rules {

    [TestClass]
    public class UnspecifiedRuleTests : SealedTests<UnspecifiedRule, BaseRule> {

        protected override UnspecifiedRule createObject() => new UnspecifiedRule();

        [TestMethod]
        public void IsUnspecifiedTest() => Assert.IsTrue(obj.IsUnspecified);

        [TestMethod]
        public void IsUnspecifiedAfterChangigDataTest() {
            obj.Data.Name = rndStr;
            Assert.IsTrue(obj.IsUnspecified);
        }
    }

}