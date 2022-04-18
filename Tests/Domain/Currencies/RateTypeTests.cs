using Abc.Aids.Calculator;
using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Rules;
using Abc.Infra.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Currencies {
    [TestClass] public class RateTypeTests : SealedTests<RateType, Entity<RateTypeData>> {
        private RulesRepo rules;
        private IRuleUsagesRepo ruleUsages;
        private IRuleElementsRepo elements;
        private IRuleSetsRepo ruleSets;
        private IExchangeRulesRepo exchangeRules;
        private BaseRule x;
        private BaseRule y;
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            elements = GetRepo.Instance<IRuleElementsRepo>();
            rules = GetRepo.Instance<IRulesRepo>() as RulesRepo;
            ruleUsages = GetRepo.Instance<IRuleUsagesRepo>();
            ruleSets = GetRepo.Instance<IRuleSetsRepo>();
            exchangeRules = GetRepo.Instance<IExchangeRulesRepo>();
        }
        [TestCleanup]
        public override void TestCleanup() {
            var db = rules?.db as RuleDb;
            removeAll(db?.RuleElements, db);
            removeAll(db?.RuleUsages, db);
            removeAll(db?.Rules, db);
            removeAll(db?.RuleOverrides, db);
            removeAll(db?.RuleSets, db);
            removeAll(db?.RuleContexts, db);
            elements = null;
            rules = null;
            ruleUsages = null;
            ruleSets = null;
            exchangeRules = null;
            x = null;
            y = null;
            base.TestCleanup();
        }

        [TestMethod]
        public async Task ExchangeRateRulesTest()
            => await testListAsync<ExchangeRule, ExchangeRuleData, IExchangeRulesRepo>(
                d => d.RateTypeId = obj.Id,
                d => new ExchangeRule(d));

        [TestMethod] public void ApplicabilityRulesTest() =>
            testRelatedList<IRuleSet, RuleSetData, ExchangeRule, IRuleSetsRepo>(
                () => obj.ApplicabilityRules,
                () => obj.ExchangeRateRules,
                (d, e) => {
                    d.Id = e.RuleSetId;
                    return new RuleSet(d);
                }, ExchangeRateRulesTest, (o, r) => o.Id == r.RuleSetId);
        [TestMethod] public void OfficialRateTest() =>
            Assert.AreEqual("OfficialRate", RateType.OfficialRate);

        [TestMethod] public void IsOfficialTest() {
            Assert.IsFalse(obj.IsOfficial);
            obj = new RateType(new RateTypeData { Id = RateType.OfficialRate });
            Assert.IsTrue(obj.IsOfficial);
        }
        [DataTestMethod]
        [DataRow("NoRulesNoContext")]
        [DataRow("NoRulesHasContext")]
        [DataRow("NoRulesHasOverride")]
        [DataRow("HasRulesNoContext")]
        [DataRow("HasRulesHasContext")]
        [DataRow("HasRulesHasSomeOverrides")]
        [DataRow("HasRulesHasAllOverrides")]
        public void IsApplicableTest(string s) {
            switch (s) {
                case "NoRulesNoContext":
                    noRulesNoContext();

                    break;
                case "NoRulesHasContext":
                    noRulesHasContext();

                    break;
                case "NoRulesHasOverride":
                    noRulesHasOverride();

                    break;
                case "HasRulesNoContext":
                    hasRulesNoContext();

                    break;
                case "HasRulesHasContext":
                    hasRulesHasContext();

                    break;
                case "HasRulesHasSomeOverrides":
                    hasRulesHasSomeOverrides();

                    break;
                case "HasRulesHasAllOverrides":
                    hasRulesHasAllOverrides();

                    break;
            }
        }
        private void hasRulesHasAllOverrides() {
            createTestRuleSet();
            void evaluate(bool firstRuleIs, bool otherRuleIs, bool expected) {

                var d = GetRandom.ObjectOf<RuleElementData>();
                d.RuleElementType = RuleElementType.Boolean;
                d.Value = firstRuleIs.ToString();
                var firstVariable = new BooleanVariable(d);
                elements.Add(firstVariable);

                d = GetRandom.ObjectOf<RuleElementData>();
                d.RuleElementType = RuleElementType.Boolean;
                d.Value = otherRuleIs.ToString();
                var otherVariable = new BooleanVariable(d);
                elements.Add(otherVariable);

                var od = GetRandom.ObjectOf<RuleOverrideData>();
                od.VariableId = firstVariable.Id;
                od.RuleId = x.Id;

                var overrideFirstRule = new RuleOverride(od);

                od = GetRandom.ObjectOf<RuleOverrideData>();
                od.VariableId = otherVariable.Id;
                od.RuleId = y.Id;
                var overrideOtherRule = new RuleOverride(od);

                var actual = obj.IsApplicable(null, overrideFirstRule, overrideOtherRule);
                Assert.AreEqual(expected, actual);
            }

            evaluate(true, true, true);
            evaluate(false, false, false);
            evaluate(false, true, false);
            evaluate(true, false, false);
        }
        private void hasRulesHasSomeOverrides() {
            createTestRuleSet();

            void addFirstRuleElements(RuleContext c, in bool isCold = true, in bool isSilver = true) {
                elements.Add(new BooleanVariable("IsColdCardHolder", isCold, c.Id, true));
                elements.Add(new BooleanVariable("IsSilverCardHolder", isSilver, c.Id, true));
            }

            void evaluate(bool otherRuleIs, bool expected) {
                var c = new RuleContext(GetRandom.ObjectOf<RuleContextData>());
                addFirstRuleElements(c);

                var d = GetRandom.ObjectOf<RuleElementData>();
                d.RuleElementType = RuleElementType.Boolean;
                d.Value = otherRuleIs.ToString();
                var otherVariable = new BooleanVariable(d);
                elements.Add(otherVariable);

                var od = GetRandom.ObjectOf<RuleOverrideData>();
                od.VariableId = otherVariable.Id;
                od.RuleId = y.Id;
                var overrideOtherRule = new RuleOverride(od);

                var actual = obj.IsApplicable(c, overrideOtherRule);
                Assert.AreEqual(expected, actual);
            }

            evaluate(true, true);
            evaluate(false, false);
        }
        private void hasRulesHasContext() {
            createTestRuleSet();

            void addSecondRuleElements(RuleContext c, in double carry = 4.5, in double allowed = 5.0) {
                elements.Add(new DoubleVariable("CarryOnBaggageKg", carry, c.Id, true));
                elements.Add(new DoubleVariable("AllowedBaggageKg", allowed, c.Id, true));
            }

            void addFirstRuleElements(RuleContext c, in bool isCold = true, in bool isSilver = true) {
                elements.Add(new BooleanVariable("IsColdCardHolder", isCold, c.Id, true));
                elements.Add(new BooleanVariable("IsSilverCardHolder", isSilver, c.Id, true));
            }

            void evaluateFirst(bool isCold, bool isSilver, bool expected) {
                var c = new RuleContext(GetRandom.ObjectOf<RuleContextData>());
                addFirstRuleElements(c, isCold, isSilver);
                addSecondRuleElements(c);

                var actual = obj.IsApplicable(c);
                Assert.AreEqual(expected, actual);
            }

            void evaluateSecond(double carry, double allowed, bool expected) {
                var c = new RuleContext(GetRandom.ObjectOf<RuleContextData>());
                addFirstRuleElements(c);
                addSecondRuleElements(c, carry, allowed);

                var actual = obj.IsApplicable(c);
                Assert.AreEqual(expected, actual);
            }

            evaluateFirst(false, false, false);
            evaluateFirst(true, true, true);
            evaluateFirst(true, false, true);
            evaluateFirst(false, true, true);
            evaluateSecond(4.5, 5.0, true);
            evaluateSecond(5.5, 5.0, false);
        }


        private void hasRulesNoContext() {
            createTestRuleSet();
            Assert.IsFalse(obj.IsApplicable(null));
        }

        private void noRulesHasOverride() {
            void isValid(bool overrideValue, bool expected) {
                var d = GetRandom.ObjectOf<RuleOverrideData>();
                var o = new RuleOverride(d);
                elements.Add(
                    new BooleanVariable(
                        new RuleElementData {
                            Id = o.Id, Value = overrideValue.ToString(), RuleElementType = RuleElementType.Boolean
                        }));

                var actual = obj.IsApplicable(null, o);
                Assert.AreEqual(expected, actual);
            }

            isValid(false, true);
            isValid(true, true);
        }

        private void noRulesHasContext() {
            void isValid(bool isCold, bool isSilver, bool expected) {
                var c = new RuleContext(GetRandom.ObjectOf<RuleContextData>());
                elements.Add(new BooleanVariable("IsColdCardHolder", isCold, c.Id, true));
                elements.Add(new BooleanVariable("IsSilverCardHolder", isSilver, c.Id, true));

                var actual = obj.IsApplicable(c);
                Assert.AreEqual(expected, actual);
            }

            isValid(false, false, true);
            isValid(true, true, true);
            isValid(true, false, true);
            isValid(false, true, true);
        }

        private void noRulesNoContext() => Assert.IsTrue(obj.IsApplicable(null));

        private void createTestRuleSet() {
            var set = new RuleSet(GetRandom.ObjectOf<RuleSetData>());
            ruleSets.Add(set);
            x = createRule(set);
            y = createRule(set);
            elements.Add(new Operand("IsColdCardHolder", x.Id, 1));
            elements.Add(new Operand("IsSilverCardHolder", x.Id, 2));
            elements.Add(new Operator(Operation.Or, x.Id, 3));
            elements.Add(new Operand("CarryOnBaggageKg", y.Id, 1));
            elements.Add(new Operand("AllowedBaggageKg", y.Id, 2));
            elements.Add(new Operator(Operation.IsLess, y.Id, 3));
        }

        private BaseRule createRule(RuleSet set) {
            var e = GetRandom.ObjectOf<ExchangeRuleData>();
            e.RateTypeId = obj.Id;
            e.RuleSetId = set.Id;
            exchangeRules.Add(new ExchangeRule(e));

            var rd = GetRandom.ObjectOf<RuleData>();
            if (rd.RuleKind == RuleKind.Unspecified) rd.RuleKind = RuleKind.ActivityRule;
            var r = RuleFactory.Create(rd);
            rules.Add(r);

            var ud = GetRandom.ObjectOf<RuleUsageData>();
            ud.RuleSetId = set.Id;
            ud.RuleId = rd.Id;
            var u = new RuleUsage(ud);
            ruleUsages.Add(u);

            return r;
        }

    }

}
