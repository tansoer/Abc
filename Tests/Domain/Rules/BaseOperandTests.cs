using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Rules {

    [TestClass] public class BaseOperandTests: 
        AbstractTests<BaseOperand, RuleElement> {
        private class testClass : BaseOperand {
            public testClass(RuleElementData d) : base(d) { }
        }
        protected override BaseOperand createObject() {
            var d = GetRandom.ObjectOf<RuleElementData>();
            d.RuleElementType = RuleElementType.Operand;
            return new testClass(d);
        }
    }
}
