using System;
using System.Collections.Generic;
using Abc.Aids.Enums;
using Abc.Aids.Methods;
using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Rules;
using Abc.Infra.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Abc.Aids.Calculator;

namespace Abc.Tests.Domain.Rules {

    [TestClass]
    public class RuleContextTests :SealedTests<RuleContext, Entity<RuleContextData>> {

        private RuleElementsRepo r;
        private BaseRule x;
        protected override RuleContext createObject()
            => new (GetRandom.ObjectOf<RuleContextData>());
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            r = GetRepo.Instance<IRuleElementsRepo>() as RuleElementsRepo;
            Assert.IsNotNull(r);
            clearDatabase();
            x = getRule();
        }
        private void clearDatabase() {
            r.dbSet.RemoveRange(r.dbSet);
            r.db.SaveChanges();
        }

        [TestMethod]
        public void AddTest() {
            test(Operation.Add, rndStr, rndStr);
            test(Operation.Add, rndInt, rndInt);
            test(Operation.Add, rndDbl, rndDbl);
            test(Operation.Add, rndBool, rndBool);
        }
        [TestMethod]
        public void AddDaysTest() =>
            test(Operation.AddDays, rndDt, rndInt);
        [TestMethod]
        public void AddHoursTest() =>
            test(Operation.AddHours, rndDt, rndInt);

        [TestMethod]
        public void AddMinutesTest() =>
            test(Operation.AddMinutes, rndDt, rndInt);

        [TestMethod]
        public void AddMonthsTest() =>
            test(Operation.AddMonths, rndDt, rndInt);

        [TestMethod]
        public void AddSecondsTest() =>
            test(Operation.AddSeconds, rndDt, rndInt);

        [TestMethod]
        public void AddYearsTest() =>
            test(Operation.AddYears, rndDt, rndInt);

        [TestMethod]
        public void AndTest() =>
            test(Operation.And, rndBool, rndBool);

        [TestMethod]
        public void ContainsTest() {
            var t = rndStr;
            test(Operation.Contains, rndStr + t, t);
            test(Operation.Contains, t + rndStr, t);
        }

        [TestMethod]
        public void DivideTest() {
            test(Operation.Divide, rndInt, rndInt);
            test(Operation.Divide, rndDbl, rndDbl);
        }

        [TestMethod]
        public void DoFindVariableTest() {
            var name = rndStr;
            var ruleId = rndStr;
            var count = GetRandom.Int32(5, 10);
            var idx = GetRandom.Int32(0, count - 1);
            var o = new Operand(name, ruleId);
            IVariable v;
            var vX = VariableFactory.Create(name, GetRandom.AnyValue(), obj.Id, true);

            for (var i = 0; i < count; i++) {
                v = i == idx
                    ? vX
                    : VariableFactory.Create(rndStr, GetRandom.AnyValue(), obj.Id, true);
                add(v);
            }

            v = obj.FindVariable(o);
            Assert.AreEqual(vX.Name, v.Name);
            Assert.AreEqual(vX.GetValue(), v.GetValue());
        }

        [TestMethod]
        public void EndsWithTest() {
            var t = rndStr;
            test(Operation.EndsWith, rndStr + t, t);
            test(Operation.EndsWith, t + rndStr, t);
        }

        [TestMethod]
        public void EqualTest() {
            test(Operation.IsEqual, rndStr, rndStr);
            test(Operation.IsEqual, rndInt, rndInt);
            test(Operation.IsEqual, rndDbl, rndDbl);
            test(Operation.IsEqual, rndBool, rndBool);
            test(Operation.IsEqual, rndDt, rndDt);
        }

        [TestMethod]
        public void FindVariableTest() {
            var name = rndStr;
            var ruleId = rndStr;
            var o = new Operand(name, ruleId);
            var v = obj.FindVariable(o);
            Assert.IsNull(v);
        }

        [TestMethod] public void GetYearTest() => test(Operation.GetYear, rndDt);

        [TestMethod] public void GetMonthTest() => test(Operation.GetMonth, rndDt);

        [TestMethod] public void GetDayTest() => test(Operation.GetDay, rndDt);

        [TestMethod] public void GetHourTest() => test(Operation.GetHour, rndDt);

        [TestMethod] public void GetMinuteTest() => test(Operation.GetMinute, rndDt);

        [TestMethod] public void GetSecondTest() => test(Operation.GetSecond, rndDt);

        [TestMethod] public void GetAgeTest() => test(Operation.GetAge, rndDt);

        [TestMethod]
        public void GetIntervalTest() =>
            test(Operation.GetInterval, rndDt, rndDt);

        [TestMethod]
        public void GreaterTest() {
            test(Operation.IsGreater, rndStr, rndStr);
            test(Operation.IsGreater, rndInt, rndInt);
            test(Operation.IsGreater, rndDbl, rndDbl);
            test(Operation.IsGreater, rndBool, rndBool);
            test(Operation.IsGreater, rndDt, rndDt);
        }

        [TestMethod]
        public void InverseTest() {
            test(Operation.Inverse, rndInt);
            test(Operation.Inverse, rndDbl);
        }

        [TestMethod] public void LengthTest() => test(Operation.GetLength, rndStr);

        [TestMethod]
        public void LessTest() {
            test(Operation.IsLess, rndStr, rndStr);
            test(Operation.IsLess, rndInt, rndInt);
            test(Operation.IsLess, rndDbl, rndDbl);
            test(Operation.IsLess, rndBool, rndBool);
            test(Operation.IsLess, rndDt, rndDt);
        }

        [TestMethod]
        public void LogicOperationsTest() {
            void logicTest(bool isCold, bool isSilver, double carry, double allowed, bool expected) {
                obj = new RuleContext(GetRandom.ObjectOf<RuleContextData>());
                add(new BooleanVariable("IsColdCardHolder", isCold, obj.Id, true));
                add(new BooleanVariable("IsSilverCardHolder", isSilver, obj.Id, true));
                add(new DoubleVariable("CarryOnBaggageKg", carry, obj.Id, true));
                add(new DoubleVariable("AllowedBaggageKg", allowed, obj.Id, true));

                var variable = x.Evaluate(obj);
                var v = variable as BooleanVariable;
                Assert.IsNotNull(v);
                Assert.AreEqual(expected, v.Value);
            }

            add(new Operand("IsColdCardHolder", x.Id));
            add(new Operand("IsSilverCardHolder", x.Id));
            add(new Operator(Operation.Or, x.Id));
            add(new Operand("CarryOnBaggageKg", x.Id));
            add(new Operand("AllowedBaggageKg", x.Id));
            add(new Operator(Operation.IsLess, x.Id));
            add(new Operator(Operation.And, x.Id));

            logicTest(false, false, 4.5, 5.0, false);
            logicTest(true, true, 5.5, 5.0, false);
            logicTest(true, true, 4.5, 5.0, true);
            logicTest(true, false, 4.5, 5.0, true);
            logicTest(false, true, 4.5, 5.0, true);
        }

        [TestMethod]
        public void MathOperationsTest() {
            void mathTest(double a, double b, double expected) {
                obj = new RuleContext(GetRandom.ObjectOf<RuleContextData>());
                add(new DoubleVariable("a", a, obj.Id, true));
                add(new DoubleVariable("b", b, obj.Id, true));

                var variable = x.Evaluate(obj);
                var v = variable as DoubleVariable;
                Assert.IsNotNull(v);
                Assert.AreEqual(expected, v.Value);
            }

            add(new Operand("a", x.Id));
            add(new Operator(Operation.Square, x.Id));
            add(new Operand("b", x.Id));
            add(new Operator(Operation.Square, x.Id));
            add(new Operator(Operation.Add, x.Id));
            add(new Operator(Operation.Sqrt, x.Id));

            mathTest(3.0, 4.0, 5.0);
            mathTest(6.0, 8.0, 10.0);
            mathTest(3.0 * 1.7, 4.0 * 1.7, 5.0 * 1.7);
        }

        [TestMethod]
        public void MultiplyTest() {
            test(Operation.Multiply, rndInt, rndInt);
            test(Operation.Multiply, rndDbl, rndDbl);
        }

        [TestMethod] public void NotTest() => test(Operation.Not, rndBool);

        [TestMethod]
        public void OppositeTest() {
            test(Operation.Opposite, rndInt);
            test(Operation.Opposite, rndDbl);
        }

        [TestMethod]
        public void OrTest() =>
            test(Operation.Or, rndBool, rndBool);

        [TestMethod]
        public void PowerTest() {
            test(Operation.Power, rndInt, rndInt);
            test(Operation.Power, rndDbl, rndDbl);
        }

        [TestMethod]
        public async Task RuleTest() =>
            await testItemAsync<RuleData, BaseRule, IRulesRepo>(
                obj.RuleId, () => obj.Rule.Data, getRule);

        [TestMethod] public void RuleIdTest() => isReadOnly(obj.Data.RuleId);

        [TestMethod]
        public async Task RuleSetTest() =>
            await testItemAsync<RuleSetData, IRuleSet, IRuleSetsRepo>(
                obj.RuleSetId, () => obj.RuleSet.Data, d => new RuleSet(d));

        [TestMethod] public void RuleSetIdTest() => isReadOnly(obj.Data.RuleSetId);

        [TestMethod]
        public void SqrtTest() {
            test(Operation.Sqrt, rndInt);
            test(Operation.Sqrt, rndDbl);
        }

        [TestMethod]
        public void SquareTest() {
            test(Operation.Square, rndInt);
            test(Operation.Square, rndDbl);
        }

        [TestMethod]
        public void StartsWithTest() {
            var t = rndStr;
            test(Operation.StartsWith, t + rndStr, t);
            test(Operation.StartsWith, rndStr + t, t);
        }

        [TestMethod]
        public void SubtractTest() {
            test(Operation.Subtract, rndInt, rndInt);
            test(Operation.Subtract, rndDbl, rndDbl);
        }

        [TestMethod]
        public void SubstringTest() {
            var s = rndStr;
            var t = rndStr;
            test(Operation.Substring, t + s + rndStr, t.Length, s.Length);
        }

        [TestMethod]
        public void SubstringToEndTest() {
            var t = rndStr;
            test(Operation.Substring, t + rndStr, t.Length);
        }

        [TestMethod] public void ToDaysTest() => test(Operation.ToDays, rndDt);

        [TestMethod] public void ToHoursTest() => test(Operation.ToHours, rndDt);

        [TestMethod] public void ToLowerTest() => test(Operation.ToLower, rndStr);

        [TestMethod] public void ToMinutesTest() => test(Operation.ToMinutes, rndDt);

        [TestMethod] public void ToMonthsTest() => test(Operation.ToMonths, rndDt);

        [TestMethod] public void ToSecondsTest() => test(Operation.ToSeconds, rndDt);

        [TestMethod] public void ToUpperTest() => test(Operation.ToUpper, rndStr);

        [TestMethod] public void TrimTest() => test(Operation.Trim, "    " + rndStr + "    ");

        [TestMethod] public void ToYearsTest() => test(Operation.ToYears, rndDt);

        [TestMethod]
        public void VariablesTest() {
            var count = GetRandom.UInt8(10, 30);

            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<RuleElementData>();
                if (d.RuleElementType is RuleElementType.Error
                    or RuleElementType.Unspecified
                    or RuleElementType.Operand
                    or RuleElementType.Operator)
                    d.RuleElementType = RuleElementType.String;
                if (i % 4 == 0) d.RuleContextId = obj.Id;
                add(VariableFactory.Create(d));
            }

            var t = isReadOnly() as IReadOnlyList<IVariable>;
            Assert.IsNotNull(t);
            Assert.AreEqual(Math.Ceiling(count / 4.0), t.Count);
        }

        [TestMethod]
        public void XorTest() =>
            test(Operation.Xor, rndBool, rndBool);

        private static BaseRule getRule(RuleData d = null) {
            d ??= GetRandom.ObjectOf<RuleData>();
            if (d.RuleKind == RuleKind.Unspecified) d.RuleKind = RuleKind.ActivityRule;
            if (d.RuleKind == RuleKind.Rule) d.RuleId = null;
            return RuleFactory.Create(d);
        }

        private void test(Operation op, object vA, object vB = null, object vC = null) {
            void doTest(object a, object b, object c = null) {
                obj = new RuleContext(GetRandom.ObjectOf<RuleContextData>());
                var varA = VariableFactory.Create("a", a, obj.Id, true);
                var varB = VariableFactory.Create("b", b, obj.Id, true);
                var varC = VariableFactory.Create("c", c, obj.Id, true);
                add(varA);
                if (!(vB is null)) add(varB);
                if (!(vC is null)) add(varC);

                var expected = vC == null ? varA.Calculate(op, varB) : varA.Calculate(op, varB, varC);

                var actual = x.Evaluate(obj);
                Assert.IsNotNull(actual);
                Assert.IsNotInstanceOfType(actual, typeof(RuleError));
                Assert.AreEqual(expected.GetValue(), actual.GetValue());
                Assert.AreEqual(expected.Name, actual.Name);
            }
            clearDatabase();
            x = getRule();
            add(new Operand("a", x.Id));
            if (!(vB is null)) add(new Operand("b", x.Id));
            if (!(vC is null)) add(new Operand("c", x.Id));
            add(new Operator(op, x.Id));

            doTest(vA, vB, vC);
        }

        private void add(IRuleElement e) {
            var idx = r.GetNextElementIndex(e.IsRuleElement, e.MasterId);
            var d = new RuleElementData();
            Copy.Members(e.Data, d);
            d.Index = idx;
            r.dbSet.Add(d);
            r.db.SaveChanges();
        }
    }
}