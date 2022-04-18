using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Rules {

    [TestClass]
    public class UnspecifiedVariableTests : BaseVariableTests<UnspecifiedVariable, string> {

        protected override UnspecifiedVariable varX(string v) => new UnspecifiedVariable(randomData());

        protected override UnspecifiedVariable varY(string v) => new UnspecifiedVariable(randomData());

        protected override UnspecifiedVariable varZ(string v) => new UnspecifiedVariable(randomData());

        private static RuleElementData randomData() {
            var d = GetRandom.ObjectOf<RuleElementData>();
            d.RuleElementType = RuleElementType.Unspecified;

            return d;
        }

        protected override void isEqualTest() => isAlwaysThis();

        protected override void isGreaterTest() => isAlwaysThis();

        protected override void isLessTest() => isAlwaysThis();

        protected void isAlwaysThis() {
            var x = varX(rndStr);
            var y = varY(rndStr);
            var z = function(x, y);
            Assert.AreEqual(x, z);
        }

    }

}