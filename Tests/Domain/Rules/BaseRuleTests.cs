using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Rules {

    [TestClass] public class BaseRuleTests :AbstractTests<BaseRule, Entity<RuleData>> {
        private class testClass :BaseRule {
            public testClass(RuleData r = null) : base(r) { }
        }
        protected override BaseRule createObject() =>
            new testClass(GetRandom.ObjectOf<RuleData>());
        [TestMethod] public async Task ElementsTest() =>
            await testListAsync<IRuleElement, RuleElementData, IRuleElementsRepo>(d => d.RuleId = obj.Id,
                RuleElementFactory.Create);
        [TestMethod] public void EvaluateTest() {
            isInstanceOfType(obj.Evaluate((RuleContext)null), typeof(RuleError));
            isInstanceOfType(obj.Evaluate((RuleOverride)null), typeof(RuleError));
        }
        [TestMethod] public void FormulaTest() {
            areEqual("", obj.Formula());
        }
    }
}
