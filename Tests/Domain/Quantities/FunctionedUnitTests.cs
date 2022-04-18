using System;
using System.Collections.Generic;
using Abc.Aids.Calculator;
using Abc.Aids.Constants;
using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Abc.Domain.Rules;
using Abc.Infra.Quantities;
using Abc.Infra.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SystemOfUnits = Abc.Core.Units.SystemOfUnits;

namespace Abc.Tests.Domain.Quantities {
    [TestClass] public class FunctionedUnitTests : SealedTests<FunctionedUnit, Unit> {
        private MeasuresRepo measures;
        private UnitsRepo units;
        private RulesRepo rules;
        private RuleElementsRepo elements;
        private Unit o;
        private string expectedShort;
        private string expectedLong;
        private string expectedMeasure;
        private byte rulesCount;
        private UnitRules siSystemRules;
        [TestMethod] public void DivideTest() {
            addMeasure(obj);
            var u = createObject();
            addMeasure(u);
            o = obj.Divide(u);
            expectedShort = $"{obj.Code} * {u.Code}^-1";
            expectedLong = $"{obj.Name} * {u.Name}^-1";
            expectedMeasure = $"{obj.Measure.Code} * {u.Measure.Code}^-1";
        }
        [TestMethod] public void DivideWithDerivedTest() {
            addMeasure(obj);
            var u = createDerived();
            o = obj.Divide(u);
            expectedShort = $"{obj.Code} * ub * ud^2 * ua^-1 * uc^-2";
            expectedLong = $"{obj.Name} * uub * uud^2 * uua^-1 * uuc^-2";
            expectedMeasure = $"{obj.Measure.Code} * b * d^2 * a^-1 * c^-2";
        }
        [TestMethod] public void DivideWithFactoredTest() {
            addMeasure(obj);
            var u = createFactored();
            addMeasure(u);
            o = obj.Divide(u);
            expectedShort = $"{obj.Code} * {u.Code}^-1";
            expectedLong = $"{obj.Name} * {u.Name}^-1";
            expectedMeasure = $"{obj.Measure.Code} * {u.Measure.Code}^-1";
        }
        [TestMethod] public void FormulaTest() {
            Assert.AreEqual("1", obj.Formula());
            Assert.AreEqual("1", obj.Formula(true));
        }
        [TestMethod] public void FromBaseTest() {
            addRules(obj);
            var x = GetRandom.Double(-1000, 1000);
            var d = obj.FromBase(x);
            Assert.AreEqual(x + 274.15, d);
        }
        [TestMethod] public void FromBaseUnitRuleTest() {
            addRulesData(obj);
            Assert.AreEqual(siSystemRules.FromBaseUnitRuleId, obj.FromBaseUnitRule.Id);
        }
        [TestMethod] public void HasRulesTest() {
            addRulesData(obj);
            var t = isReadOnly(obj, nameof(obj.Rules)) as IReadOnlyList<UnitRules>;
            Assert.IsNotNull(t);
            Assert.AreEqual(Math.Ceiling(rulesCount / 4.0), t.Count);
        }
        [TestMethod] public void InverseTest() {
            addMeasure(obj);
            o = obj.Inverse();
            expectedShort = $"{obj.Code}^-1";
            expectedLong = $"{obj.Name}^-1";
            expectedMeasure = $"{obj.Measure.Code}^-1";
        }
        [TestMethod] public void MultiplyTest() {
            addMeasure(obj);
            var u = createObject();
            addMeasure(u);
            o = obj.Multiply(u);
            expectedShort = $"{obj.Code} * {u.Code}";
            expectedLong = $"{obj.Name} * {u.Name}";
            expectedMeasure = $"{obj.Measure.Code} * {u.Measure.Code}";
        }
        [TestMethod] public void MultiplyWithDerivedTest() {
            addMeasure(obj);
            var u = createDerived();
            o = obj.Multiply(u);
            expectedShort = $"{obj.Code} * ua * uc^2 * ub^-1 * ud^-2";
            expectedLong = $"{obj.Name} * uua * uuc^2 * uub^-1 * uud^-2";
            expectedMeasure = $"{obj.Measure.Code} * a * c^2 * b^-1 * d^-2";
        }
        [TestMethod] public void MultiplyWithFactoredTest() {
            addMeasure(obj);
            var u = createFactored();
            addMeasure(u);
            o = obj.Multiply(u);
            expectedShort = $"{obj.Code} * {u.Code}";
            expectedLong = $"{obj.Name} * {u.Name}";
            expectedMeasure = $"{obj.Measure.Code} * {u.Measure.Code}";
        }
        [TestMethod] public void PowerTest() {
            addMeasure(obj);
            var i = GetRandom.Int32(-10, 10);
            o = obj.Power(i);
            expectedShort = i == 0 ? Word.Unspecified : i == 1 ? obj.Code : $"{obj.Code}^{i}";
            expectedLong = i == 0 ? Word.Unspecified : i == 1 ? obj.Name : $"{obj.Name}^{i}";
            expectedMeasure = i == 0 ? Word.Unspecified : i == 1 ? obj.Measure.Code : $"{obj.Measure.Code}^{i}";
        }
        [TestMethod] public void RulesTest() {
            Assert.IsNotNull(obj.Rules);
            Assert.AreEqual(0, obj.Rules.Count);
        }
        [TestMethod] public void SiSystemRulesTest() {
            addRulesData(obj);
            Assert.IsNotNull(obj.SiSystemRules);
        }
        [TestCleanup] public override void TestCleanup() {
            validate(o);
            var db = measures?.db as QuantityDb;
            removeAll(db?.Units, db);
            removeAll(db?.SystemsOfUnits, db);
            removeAll(db?.UnitFactors, db);
            removeAll(db?.CommonTerms, db);
            removeAll(db?.Measures, db);
            removeAll(db?.UnitRules, db);
            removeAll(rules?.dbSet, rules?.db);
            removeAll(elements?.dbSet, elements?.db);

            measures = null;
            units = null;
            rules = null;
            elements = null;
        }
        private void validate(Unit u) {
            if (u is null) return;
            Assert.IsNotNull(u);
            Assert.IsInstanceOfType(u, typeof(DerivedUnit));
            Assert.AreEqual(expectedShort, u.Formula());
            Assert.AreEqual(expectedLong, u.Formula(true));
            Assert.AreEqual(expectedMeasure, u.Measure.Formula());
            Assert.AreEqual(u.Id, u.Formula());
            Assert.AreNotEqual(string.Empty, u.Id);
        }
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            measures = GetRepo.Instance<IMeasuresRepo>() as MeasuresRepo;
            rules = GetRepo.Instance<IRulesRepo>() as RulesRepo;
            elements = GetRepo.Instance<IRuleElementsRepo>() as RuleElementsRepo;
            units = GetRepo.Instance<IUnitsRepo>() as UnitsRepo;
            rulesCount = GetRandom.UInt8(15, 30);
        }
        [TestMethod] public void ToBaseTest() {
            addRules(obj);
            var x = GetRandom.Double(-1000, 1000);
            var d = obj.ToBase(x);
            Assert.AreEqual(x - 274.15, d);
        }
        [TestMethod] public void ToBaseUnitRuleTest() {
            addRulesData(obj);
            Assert.AreEqual(siSystemRules.ToBaseUnitRuleId, obj.ToBaseUnitRule.Id);
        }
        protected FactoredUnit createFactored() {
            var d = GetRandom.ObjectOf<UnitData>();
            d.UnitType = UnitType.Factored;

            return new FactoredUnit(d);
        }
        protected override FunctionedUnit createObject() {
            var d = GetRandom.ObjectOf<UnitData>();
            d.UnitType = UnitType.Functioned;

            return new FunctionedUnit(d);
        }
        private void addFromBaseElements(string ruleId) {
            addOperand("x", ruleId);
            addVariable(274.15, ruleId);
            addOperator(Operation.Add, ruleId);
        }
        private void addMeasure(Unit u) {
            var d = GetRandom.ObjectOf<MeasureData>();
            d.Id = u.MeasureId;
            d.MeasureType = MeasureType.Base;
            measures.Add(new BaseMeasure(d));
        }
        private void addOperand(string s, string ruleId) {
            var d = new RuleElementData {
                Name = s,
                RuleElementType = RuleElementType.Operand,
                RuleId = ruleId
            };
            addRuleElement(d);
        }
        private void addOperator(Operation op, string ruleId) {
            var d = new RuleElementData {
                RuleElementType = RuleElementType.Operator,
                Operation = op,
                RuleId = ruleId
            };
            addRuleElement(d);
        }
        private void addRuleElement(RuleElementData d) {
            var index = elements.GetNextElementIndex(true, d.RuleId);
            d.Index = index;
            elements.Add(RuleElementFactory.Create(d));
        }
        private void addRules(FunctionedUnit u) {
            var unitRule = GetRandom.ObjectOf<UnitRulesData>();
            unitRule.UnitId = u.Id;
            unitRule.SystemOfUnitsId = SystemOfUnits.SiSystemId;
            GetRepo.Instance<IUnitRulesRepo>().Add(new UnitRules(unitRule));
            var fromRule = createRule(unitRule.FromBaseUnitRuleId);
            var toRule = createRule(unitRule.ToBaseUnitRuleId);
            addFromBaseElements(fromRule.Id);
            addToBaseElements(toRule.Id);
        }
        private void addRulesData(Unit u) {
            for (var i = 0; i < rulesCount; i++) {
                var d = GetRandom.ObjectOf<UnitRulesData>();
                if (i % 4 == 0) d.UnitId = u.Id;
                if (i % 8 == 0 && i < 10 && i > 1)
                    d.SystemOfUnitsId = SystemOfUnits.SiSystemId;
                var f = new UnitRules(d);
                GetRepo.Instance<IUnitRulesRepo>().Add(f);

                if (f.SystemOfUnitsId != SystemOfUnits.SiSystemId) continue;
                siSystemRules = f;
                var fromRule = createRule(f.FromBaseUnitRuleId);
                var toRule = createRule(f.ToBaseUnitRuleId);
                addFromBaseElements(fromRule.Id);
                addToBaseElements(toRule.Id);
            }
        }
        private void addToBaseElements(string ruleId) {
            addOperand("x", ruleId);
            addVariable(274.15, ruleId);
            addOperator(Operation.Subtract, ruleId);
        }
        private void addVariable(double v, string ruleId) {
            var d = new RuleElementData {
                Name = "v",
                RuleElementType = RuleElementType.Double,
                Value = Variable.ToString(v),
                RuleId = ruleId
            };
            addRuleElement(d);
        }
        private Unit createDerived() {
            var d = GetRandom.ObjectOf<MeasureData>();
            d.MeasureType = MeasureType.Derived;
            var m = new DerivedMeasure(d);
            new derivedMeasureTestData(m).add();

            var ud = GetRandom.ObjectOf<UnitData>();
            ud.UnitType = UnitType.Derived;
            ud.MeasureId = m.Id;
            var u = new DerivedUnit(ud, m);
            new derivedUnitTestData(u).add();

            return u;
        }
        private Rule createRule(string id) {
            var d = GetRandom.ObjectOf<RuleData>();
            d.Id = id;
            var r = new Rule(d);
            rules.Add(r);
            return r;
        }
    }
}