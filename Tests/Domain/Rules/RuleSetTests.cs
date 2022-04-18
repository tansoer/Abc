using System;
using System.Collections.Generic;
using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Data.Quantities;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Quantities;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Abc.Tests.Domain.Common.EntityTests;
using Abc.Aids.Calculator;

namespace Abc.Tests.Domain.Rules {
    [TestClass] public class RuleSetTests :SealedTests<RuleSet, Entity<RuleSetData>> {

        internal class MockRuleSet :MockEntity<RuleSetData>, IRuleSet {
            public IReadOnlyList<RuleOverride> Overrides => Mock.Func<IReadOnlyList<RuleOverride>>();
            public IReadOnlyList<RuleUsage> RuleUsages => Mock.Func<IReadOnlyList<RuleUsage>>();
            public IReadOnlyList<BaseRule> Rules => Mock.Func<IReadOnlyList<BaseRule>>();
            public IVariable Evaluate(RuleContext c) => Mock.Func<IVariable>(c);
            public IVariable Evaluate(RuleContext c, params RuleOverride[] overrides) =>
                Mock.Func<IVariable>(c, overrides);
            public IVariable Evaluate(RuleOverride o) => Mock.Func<IVariable>(o);
            public Expression ExportExpression<TClass>() {
                throw new NotImplementedException();
            }
        }

        private BaseRule ruleX;
        private BaseRule ruleY;
        protected override RuleSet createObject() => new(GetRandom.ObjectOf<RuleSetData>());

        [TestMethod] public async Task EvaluateTest() {
            await createTestRuleSet();
            await evaluateContextTest();
        }

        [TestMethod] public async Task OverrideAllTest() {
            await createTestRuleSet();
            await evaluateOverrideAllTest();
        }

        [TestMethod] public async Task OverrideAllRulesTest() {
            await createTestRuleSet();
            await evaluateOverrideAllRulesTest();
        }

        [DataTestMethod] 
        [DataRow(true, true, false, false)]
        [DataRow(false, false, true, false)]
        [DataRow(true, true, true, true)]
        [DataRow(true, false, true, true)]
        //TODO: Gunnar
        //[DataRow(false, true, true, true)]
        public async Task OverrideSomeRulesTest(
            bool isCold, bool isSilver, bool otherRuleIs, bool expected) {
            await createTestRuleSet();
            var c = new RuleContext(GetRandom.ObjectOf<RuleContextData>());
            await addElement(new BooleanVariable("IsColdCardHolder", isCold, c.Id, true));
            await addElement(new BooleanVariable("IsSilverCardHolder", isSilver, c.Id, true));
          
            var d = GetRandom.ObjectOf<RuleElementData>();
            d.RuleElementType = RuleElementType.Boolean;
            d.Value = otherRuleIs.ToString();
            var otherVariable = new BooleanVariable(d);
            await addElement(otherVariable);
            
            var od = GetRandom.ObjectOf<RuleOverrideData>();
            od.VariableId = otherVariable.Id;
            od.RuleId = ruleY.Id;
            var overrideOtherRule = new RuleOverride(od);

            var variable = obj.Evaluate(c, overrideOtherRule);
            var v = variable as BooleanVariable;
            Assert.IsNotNull(v);
            Assert.AreEqual(expected, v.Value);
        }

        [TestMethod] public async Task OverridesTest() {
            var count = GetRandom.UInt8(10, 30);
            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<RuleOverrideData>();
                if (i % 4 == 0) d.RuleSetId = obj.Id;
                await addAsync<IRuleOverridesRepo, RuleOverride>(new RuleOverride(d));
            }

            var t = isReadOnly() as IReadOnlyList<RuleOverride>;
            Assert.IsNotNull(t);
            Assert.AreEqual(Math.Ceiling(count / 4.0), t.Count);
        }

        [TestMethod] public async Task RuleUsagesTest() =>
            await testListAsync<RuleUsage, RuleUsageData, IRuleUsagesRepo>(
                d => d.RuleSetId = obj.Id, d => new RuleUsage(d));

        [TestMethod] public void RulesTest() =>
            testRelatedList<BaseRule, RuleData, RuleUsage, IRulesRepo>(
                () => obj.Rules, () => obj.RuleUsages,
                (d, e) => {
                    d.Id = e.RuleId;
                    d.RuleKind = random<bool>() ? RuleKind.Rule : RuleKind.ActivityRule;
                    return RuleFactory.Create(d);
                }, 
                RuleUsagesTest, (x, r) => x.Id == r.RuleId
            );

        private async Task createTestRuleSet() {
            ruleX = await createRule();
            ruleY = await createRule();
            await addElement(new Operand("IsColdCardHolder", ruleX.Id));
            await addElement(new Operand("IsSilverCardHolder", ruleX.Id));
            await addElement(new Operator(Operation.Or, ruleX.Id));
            await addElement(new Operand("CarryOnBaggageKg", ruleY.Id));
            await addElement(new Operand("AllowedBaggageKg", ruleY.Id));
            await addElement(new Operator(Operation.IsLess, ruleY.Id));
        }

        private async Task<BaseRule> createRule() {
            var rd = GetRandom.ObjectOf<RuleData>();
            rd.RuleKind = rndBool ? RuleKind.ActivityRule : RuleKind.Rule;
            var ud = GetRandom.ObjectOf<RuleUsageData>();
            ud.RuleSetId = obj.Id;
            ud.RuleId = rd.Id;
            var r = RuleFactory.Create(rd);
            var u = new RuleUsage(ud);
            await addAsync<IRulesRepo, BaseRule>(r);
            await addAsync<IRuleUsagesRepo, RuleUsage>(u);
            return r;
        }


        private async Task evaluateContextTest() {
            async Task evaluate(bool isCold, bool isSilver, double carry, double allowed, bool expected) {
                var c = new RuleContext(GetRandom.ObjectOf<RuleContextData>());
                await addElement(new BooleanVariable("IsColdCardHolder", isCold, c.Id, true));
                await addElement(new BooleanVariable("IsSilverCardHolder", isSilver, c.Id, true));
                await addElement(new DoubleVariable("CarryOnBaggageKg", carry, c.Id, true));
                await addElement(new DoubleVariable("AllowedBaggageKg", allowed, c.Id, true));
                var variable = obj.Evaluate(c);
                var v = variable as BooleanVariable;
                Assert.IsNotNull(v);
                Assert.AreEqual(expected, v.Value);
            }
            await evaluate(false, false, 4.5, 5.0, false);
            await evaluate(true, true, 5.5, 5.0, false);
            await evaluate(true, true, 4.5, 5.0, true);
            await evaluate(true, false, 4.5, 5.0, true);
            await evaluate(false, true, 4.5, 5.0, true);
        }

        private async Task evaluateOverrideAllTest() {
            Assert.IsInstanceOfType(obj.Evaluate((RuleOverride)null), typeof(RuleError));
            Assert.IsInstanceOfType(obj.Evaluate(new RuleOverride(GetRandom.ObjectOf<RuleOverrideData>())),
                typeof(RuleError));
            var vd = GetRandom.ObjectOf<RuleElementData>();
            await correctAVariable(vd);
            var v = VariableFactory.Create(vd);
            await addElement(v);
            var d = GetRandom.ObjectOf<RuleOverrideData>();
            d.RuleSetId = obj.Id;
            d.VariableId = v.Id;
            var result = obj.Evaluate(new RuleOverride(d));
            var expected = result.GetValue();
            var actual = v.GetValue();
            Assert.AreEqual(expected.ToString(), actual.ToString(), actual.GetType().ToString());
        }

        internal static async Task correctAVariable(RuleElementData d) {
            if (d is null) return;
            switch (d.RuleElementType) {
                case RuleElementType.Boolean:
                    d.Value = Variable.ToString(rndBool);
                    break;
                case RuleElementType.Integer:
                    d.Value = Variable.ToString(GetRandom.Int32());
                    break;
                case RuleElementType.Decimal:
                    d.Value = Variable.ToString(GetRandom.Decimal());
                    break;
                case RuleElementType.Double:
                    d.Value = Variable.ToString(GetRandom.Double());
                    break;
                case RuleElementType.DateTime:
                    d.Value = Variable.ToString(rndDt);
                    break;
                case RuleElementType.Quantity:
                    d.Value = Variable.ToString(GetRandom.Double(-100000, 100000));
                    var u = GetRandom.ObjectOf<UnitData>();
                    u.Id = d.UnitOrCurrencyId;
                    u.UnitType = UnitType.Factored;
                    await addAsync<IUnitsRepo, Unit>(UnitFactory.Create(u));
                    break;
                case RuleElementType.Money:
                    d.Value = Variable.ToString(GetRandom.Decimal(-100000, 100000));
                    var c = GetRandom.ObjectOf<CurrencyData>();
                    c.Id = d.UnitOrCurrencyId;
                    await addAsync<ICurrencyRepo, Currency>(new Currency(c));
                    break;
                default:
                    d.RuleElementType = RuleElementType.String;
                    break;
            }
        }
        private async Task evaluateOverrideAllRulesTest() {
            async Task evaluate(bool firstRuleIs, bool otherRuleIs, bool expected) {
                var d = GetRandom.ObjectOf<RuleElementData>();
                d.RuleElementType = RuleElementType.Boolean;
                d.Value = firstRuleIs.ToString();
                var firstVariable = new BooleanVariable(d);
                await addElement(firstVariable);
     
                d = GetRandom.ObjectOf<RuleElementData>();
                d.RuleElementType = RuleElementType.Boolean;
                d.Value = otherRuleIs.ToString();
                var otherVariable = new BooleanVariable(d);
                await addElement(otherVariable);
                
                var od = GetRandom.ObjectOf<RuleOverrideData>();
                od.VariableId = firstVariable.Id;
                od.RuleId = ruleX.Id;
                var overrideFirstRule = new RuleOverride(od);
                
                od = GetRandom.ObjectOf<RuleOverrideData>();
                od.VariableId = otherVariable.Id;
                od.RuleId = ruleY.Id;
                var overrideOtherRule = new RuleOverride(od);
                
                var variable = obj.Evaluate(null, overrideFirstRule, overrideOtherRule);
                var v = variable as BooleanVariable;
                Assert.IsNotNull(v);
                Assert.AreEqual(expected, v.Value);
            }
            await evaluate(false, true, false);
            await evaluate(true, false, false);
            await evaluate(true, true, true);
            await evaluate(false, false, false);
        }
        private static async Task addElement(IRuleElement e)
            => await addAsync<IRuleElementsRepo, IRuleElement>(e);
    }
}
