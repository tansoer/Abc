using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Rules {

    [TestClass]
    public class RuleErrorTests : BaseVariableTests<RuleError, string> {

        protected override RuleError varX(string v) => new RuleError(randomData());

        protected override RuleError varY(string v) => new RuleError(randomData());

        protected override RuleError varZ(string v) => new RuleError(randomData());

        private static RuleElementData randomData() {
            var d = GetRandom.ObjectOf<RuleElementData>();
            d.RuleElementType = RuleElementType.Error;

            return d;
        }

    }

}