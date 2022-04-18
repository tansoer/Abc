using Abc.Aids.Calculator;
using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Rules {
    [TestClass] public class ActivityRuleTests :SealedTests<ActivityRule, BaseRule> {

        protected override ActivityRule createObject() {
            var d = GetRandom.ObjectOf<RuleData>();
            d.RuleKind = RuleKind.ActivityRule;

            return new ActivityRule(d);
        }

        [DataTestMethod] [DataRow("PerformDefault")] [DataRow("PerformWithContext")] [DataRow("PerformConditional")]
        public async Task PerformTest(string s) {
            await createTestElements();

            switch (s) {
                case "PerformDefault":
                    await performDefaultTest();

                    break;
                case "PerformWithContext":
                    await performWithContextTest();

                    break;
                case "PerformConditional":
                    await performConditional();

                    break;
            }
        }

        private async Task performConditional() {
            async Task evaluate(bool isCold, bool isSilver, double carry, double allowed, bool expected,
                bool ruleResult) {
                var cRule = new RuleContext(GetRandom.ObjectOf<RuleContextData>());
                var cActivity = new RuleContext(GetRandom.ObjectOf<RuleContextData>());
                await addElement(new BooleanVariable("IsColdCardHolder", isCold, cRule.Id, true));
                await addElement(new BooleanVariable("IsSilverCardHolder", isSilver, cRule.Id, true));
                await addElement(new DoubleVariable("CarryOnBaggageKg", carry, cActivity.Id, true));
                await addElement(new DoubleVariable("AllowedBaggageKg", allowed, cActivity.Id, true));

                var variable = obj.Perform(cRule, new BooleanVariable("result", ruleResult), cActivity);
                var v = variable as BooleanVariable;
                Assert.IsNotNull(v);
                Assert.AreEqual(expected, v.Value);
            }

            await evaluate(false, false, 4.5, 5.0, true, false);
            await evaluate(true, true, 5.5, 5.0, false, true);
            await evaluate(true, true, 4.5, 5.0, true, true);
            await evaluate(true, false, 4.5, 5.0, true, true);
            await evaluate(false, true, 4.5, 5.0, true, true);
            await evaluate(false, true, 4.5, 5.0, false, false);
            await evaluate(true, true, 4.5, 5.0, false, false);

        }

        private async Task performWithContextTest() {
            async Task perform(double carry, double allowed, bool expected) {
                var d = GetRandom.ObjectOf<RuleContextData>();
                await addElement(new DoubleVariable("CarryOnBaggageKg", carry, d.Id, true));
                await addElement(new DoubleVariable("AllowedBaggageKg", allowed, d.Id, true));

                var variable = obj.Perform(new RuleContext(d));
                var v = variable as BooleanVariable;
                Assert.IsNotNull(v);
                Assert.AreEqual(expected, v.Value);
            }

            await perform(4.5, 5.0, true);
            await perform(5.5, 5.0, false);
        }

        private async Task performDefaultTest() {
            async Task perform(double carry, double allowed, bool expected) {
                obj = createObject();
                await createTestElements();
                await addElement(new DoubleVariable("CarryOnBaggageKg", carry, obj.Id, true));
                await addElement(new DoubleVariable("AllowedBaggageKg", allowed, obj.Id, true));

                var variable = obj.Perform();
                var v = variable as BooleanVariable;
                Assert.IsNotNull(v);
                Assert.AreEqual(expected, v.Value);
            }

            await perform(5.5, 5.0, false);
            await perform(4.5, 5.0, true);
        }

        [TestMethod] public async Task ActivityTest() =>
            await testListAsync<IRuleElement, RuleElementData, IRuleElementsRepo>(
                d => d.ActivityId = obj.Id,
                RuleElementFactory.Create);

        [TestMethod] public async Task ContextTest() =>
            await testListAsync<IRuleElement, RuleElementData, IRuleElementsRepo>(
                d => d.RuleContextId = obj.Id,
                RuleElementFactory.Create);

        private async Task createTestElements() {
            await addElement(new Operand(new RuleElementData
                {Name = "IsColdCardHolder", RuleId = obj.Id, RuleElementType = RuleElementType.Operand}));
            await addElement(new Operand(new RuleElementData
                {Name = "IsSilverCardHolder", RuleId = obj.Id, RuleElementType = RuleElementType.Operand}));
            await addElement(new Operator(new RuleElementData
                {Operation = Operation.Or, RuleId = obj.Id, RuleElementType = RuleElementType.Operator}));
            await addElement(new Operand(new RuleElementData
                {Name = "CarryOnBaggageKg", ActivityId = obj.Id, RuleElementType = RuleElementType.Operand}));
            await addElement(new Operand(new RuleElementData
                {Name = "AllowedBaggageKg", ActivityId = obj.Id, RuleElementType = RuleElementType.Operand}));
            await addElement(new Operator(new RuleElementData
                {Operation = Operation.IsLess, ActivityId = obj.Id, RuleElementType = RuleElementType.Operator}));
        }

        private static async Task addElement(IRuleElement e) => await addAsync<IRuleElementsRepo, IRuleElement>(e);
    }
}
