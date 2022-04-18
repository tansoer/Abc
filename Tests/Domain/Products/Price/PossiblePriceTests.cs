using Abc.Aids.Calculator;
using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Price;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Price {
    [TestClass]
    public class PossiblePriceTests :SealedTests<PossiblePrice, BasePrice> {

        private IRulesRepo rules;
        private IRuleUsagesRepo ruleUsages;
        private IRuleElementsRepo elements;
        private BaseRule x;
        private BaseRule y;
        private IRuleSetsRepo ruleSets;

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            elements = GetRepo.Instance<IRuleElementsRepo>();
            rules = GetRepo.Instance<IRulesRepo>();
            ruleUsages = GetRepo.Instance<IRuleUsagesRepo>();
            ruleSets = GetRepo.Instance<IRuleSetsRepo>();
        }

        [TestMethod] public void ProductTypeIdTest() => isReadOnly(obj.Data.ProductTypeId);

        [TestMethod] public void RuleSetIdTest() => isReadOnly(obj.Data.RuleSetId);

        [TestMethod]
        public async Task ProductTypeTest() =>
            await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(
                obj.ProductTypeId, () => obj.ProductType.Data, ProductTypeFactory.Create);

        [TestMethod]
        public async Task RuleSetTest() =>
            await testItemAsync<RuleSetData, IRuleSet, IRuleSetsRepo>(
                obj.RuleSetId, () => obj.RuleSet.Data, d => new RuleSet(d));

        [DataTestMethod]
        [DataRow("NoRulesNoContext")]
        [DataRow("NoRulesHasContext")]
        [DataRow("NoRulesHasOverride")]
        [DataRow("HasRulesNoContext")]
        [DataRow("HasRulesHasContext")]
        [DataRow("HasRulesHasSomeOverrides")]
        [DataRow("HasRulesHasAllOverrides")]
        public void IsValidTest(string s) {
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

                var actual = obj.IsValid(null, overrideFirstRule, overrideOtherRule);
                Assert.AreEqual(expected, actual);
            }

            evaluate(false, true, false);
            evaluate(true, false, false);
            evaluate(false, false, false);
            evaluate(true, true, true);
        }

        private void hasRulesHasSomeOverrides() {
            createTestRuleSet();

            void evaluate(bool isCold, bool isSilver, bool otherRuleIs, bool expected) {
                var c = new RuleContext(GetRandom.ObjectOf<RuleContextData>());
                elements.Add(new BooleanVariable("IsColdCardHolder", isCold, c.Id, true));
                elements.Add(new BooleanVariable("IsSilverCardHolder", isSilver, c.Id, true));

                var d = GetRandom.ObjectOf<RuleElementData>();
                d.RuleElementType = RuleElementType.Boolean;
                d.Value = otherRuleIs.ToString();
                var otherVariable = new BooleanVariable(d);
                elements.Add(otherVariable);

                var od = GetRandom.ObjectOf<RuleOverrideData>();
                od.VariableId = otherVariable.Id;
                od.RuleId = y.Id;
                var overrideOtherRule = new RuleOverride(od);

                var actual = obj.IsValid(c, overrideOtherRule);
                Assert.AreEqual(expected, actual);
            }

            evaluate(false, false, true, false);
            evaluate(true, true, false, false);
            evaluate(true, true, true, true);
            evaluate(true, false, true, true);
            evaluate(false, true, true, true);
        }

        private void hasRulesHasContext() {
            createTestRuleSet();

            void evaluate(bool isCold, bool isSilver, double carry, double allowed, bool expected) {
                var c = new RuleContext(GetRandom.ObjectOf<RuleContextData>());
                elements.Add(new BooleanVariable("IsColdCardHolder", isCold, c.Id, true));
                elements.Add(new BooleanVariable("IsSilverCardHolder", isSilver, c.Id, true));
                elements.Add(new DoubleVariable("CarryOnBaggageKg", carry, c.Id, true));
                elements.Add(new DoubleVariable("AllowedBaggageKg", allowed, c.Id, true));

                var actual = obj.IsValid(c);
                Assert.AreEqual(expected, actual);
            }

            evaluate(false, false, 4.5, 5.0, false);
            evaluate(true, true, 5.5, 5.0, false);
            evaluate(true, false, 4.5, 5.0, true);
            evaluate(false, true, 4.5, 5.0, true);
            evaluate(true, true, 4.5, 5.0, true);
        }

        private void hasRulesNoContext() {
            createTestRuleSet();
            Assert.IsFalse(obj.IsValid(null));
        }

        private void noRulesHasOverride() {
            void isValid(bool overrideValue, bool expected) {
                var d = GetRandom.ObjectOf<RuleOverrideData>();
                var o = new RuleOverride(d);
                elements.Add(
                    new BooleanVariable(
                        new RuleElementData {
                            Id = o.Id,
                            Value = overrideValue.ToString(),
                            RuleElementType = RuleElementType.Boolean
                        }));

                var actual = obj.IsValid(null, o);
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

                var actual = obj.IsValid(c);
                Assert.AreEqual(expected, actual);
            }

            isValid(false, false, true);
            isValid(true, true, true);
            isValid(true, false, true);
            isValid(false, true, true);
        }

        private void noRulesNoContext() => Assert.IsTrue(obj.IsValid(null));

        private void createTestRuleSet() {
            x = createRule();
            y = createRule();
            elements.Add(new Operand("IsColdCardHolder", x.Id));
            elements.Add(new Operand("IsSilverCardHolder", x.Id, 1));
            elements.Add(new Operator(Operation.Or, x.Id, 2));
            elements.Add(new Operand("CarryOnBaggageKg", y.Id));
            elements.Add(new Operand("AllowedBaggageKg", y.Id, 1));
            elements.Add(new Operator(Operation.IsLess, y.Id, 2));
        }

        private BaseRule createRule() {
            var d = GetRandom.ObjectOf<RuleSetData>();
            d.Id = obj.RuleSetId;
            var set = new RuleSet(d);
            var rd = GetRandom.ObjectOf<RuleData>();
            rd.RuleKind = random<bool>() ? RuleKind.ActivityRule : RuleKind.Rule;
            var ud = GetRandom.ObjectOf<RuleUsageData>();
            ud.RuleSetId = set.Id;
            ud.RuleId = rd.Id;
            var r = RuleFactory.Create(rd);
            var u = new RuleUsage(ud);
            ruleSets.Add(set);
            rules.Add(r);
            ruleUsages.Add(u);

            return r;
        }

    }
}