using Abc.Aids.Calculator;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Rules {
    [TestClass] public class VariableCalculatorTests : SealedTests<VariableCalculator, Calculate> {
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            obj = new VariableCalculator();
        }
        [TestMethod] public void AddTest() {
            test(Operation.Add, rndStr, rndStr);
            test(Operation.Add, rndInt, rndInt);
            test(Operation.Add, rndDbl, rndDbl);
            test(Operation.Add, rndBool, rndBool);
        }
        [TestMethod] public void SubtractTest() {
            test(Operation.Subtract, rndInt, rndInt);
            test(Operation.Subtract, rndDbl, rndDbl);
        }
        [TestMethod] public void MultiplyTest() {
            test(Operation.Multiply, rndInt, rndInt);
            test(Operation.Multiply, rndDbl, rndDbl);
        }
        [TestMethod] public void DivideTest() {
            test(Operation.Divide, rndInt, rndInt);
            test(Operation.Divide, rndDbl, rndDbl);
        }
        [TestMethod] public void PowerTest() {
            test(Operation.Power, rndInt, rndInt);
            test(Operation.Power, rndDbl, rndDbl);
        }
        [TestMethod] public void OppositeTest() {
            test(Operation.Opposite, rndInt);
            test(Operation.Opposite, rndDbl);
        }
        [TestMethod] public void SqrtTest() {
            test(Operation.Sqrt, rndInt);
            test(Operation.Sqrt, rndDbl);
        }
        [TestMethod] public void SquareTest() {
            test(Operation.Square, rndInt);
            test(Operation.Square, rndDbl);
        }
        [TestMethod] public void InverseTest() {
            test(Operation.Inverse, rndInt);
            test(Operation.Inverse, rndDbl);
        }
        [TestMethod] public void AndTest()  => test(Operation.And, rndBool, rndBool);
        [TestMethod] public void OrTest() => test(Operation.Or, rndBool, rndBool);
        [TestMethod] public void XorTest() => test(Operation.Xor, rndBool, rndBool);
        [TestMethod] public void NotTest() => test(Operation.Not, rndBool);
        [TestMethod] public void IsEqualTest() {
            test(Operation.IsEqual, rndStr, rndStr);
            test(Operation.IsEqual, rndInt, rndInt);
            test(Operation.IsEqual, rndDbl, rndDbl);
            test(Operation.IsEqual, rndBool, rndBool);
            test(Operation.IsEqual, rndDt, rndDt);
        }
        [TestMethod] public void IsGreaterTest() {
            test(Operation.IsGreater, rndStr, rndStr);
            test(Operation.IsGreater, rndInt, rndInt);
            test(Operation.IsGreater, rndDbl, rndDbl);
            test(Operation.IsGreater, rndBool, rndBool);
            test(Operation.IsGreater, rndDt, rndDt);
        }
        [TestMethod] public void IsLessTest() {
            test(Operation.IsLess, rndStr, rndStr);
            test(Operation.IsLess, rndInt, rndInt);
            test(Operation.IsLess, rndDbl, rndDbl);
            test(Operation.IsLess, rndBool, rndBool);
            test(Operation.IsLess, rndDt, rndDt);
        }
        [TestMethod] public void GetYearTest() => test(Operation.GetYear, rndDt);
        [TestMethod] public void GetMonthTest() => test(Operation.GetMonth, rndDt);
        [TestMethod] public void GetDayTest() => test(Operation.GetDay, rndDt);
        [TestMethod] public void GetHourTest() => test(Operation.GetHour, rndDt);
        [TestMethod] public void GetMinuteTest() => test(Operation.GetMinute, rndDt);
        [TestMethod] public void GetSecondTest() => test(Operation.GetSecond, rndDt);
        [TestMethod] public void AddDaysTest() => test(Operation.AddDays, rndDt, rndInt);
        [TestMethod] public void AddHoursTest() => test(Operation.AddHours, rndDt, rndInt);
        [TestMethod] public void AddMinutesTest() => test(Operation.AddMinutes, rndDt, rndInt);
        [TestMethod] public void AddMonthsTest() => test(Operation.AddMonths, rndDt, rndInt);
        [TestMethod] public void AddSecondsTest() => test(Operation.AddSeconds, rndDt, rndInt);
        [TestMethod] public void AddYearsTest() => test(Operation.AddYears, rndDt, rndInt);
        [TestMethod] public void GetAgeTest() => test(Operation.GetAge, rndDt);
        [TestMethod] public void GetIntervalTest() => test(Operation.GetInterval, rndDt, rndDt);
        [TestMethod] public void ToYearsTest() => test(Operation.ToYears, rndDt);
        [TestMethod] public void ToMonthsTest() => test(Operation.ToMonths, rndDt);
        [TestMethod] public void ToDaysTest() => test(Operation.ToDays, rndDt);
        [TestMethod] public void ToHoursTest() => test(Operation.ToHours, rndDt);
        [TestMethod] public void ToMinutesTest() => test(Operation.ToMinutes, rndDt);
        [TestMethod] public void ToSecondsTest() => test(Operation.ToSeconds, rndDt);
        [TestMethod] public void ContainsTest() {
            var t = rndStr;
            test(Operation.Contains, rndStr + t, t);
            test(Operation.Contains, t + rndStr, t);
        }
        [TestMethod] public void EndsWithTest() {
            var t = rndStr;
            test(Operation.EndsWith, rndStr + t, t);
            test(Operation.EndsWith, t + rndStr, t);
        }
        [TestMethod] public void GetLengthTest() => test(Operation.GetLength, rndStr);
        [TestMethod] public void StartsWithTest() {
            var t = rndStr;
            test(Operation.StartsWith, t + rndStr, t);
            test(Operation.StartsWith, rndStr + t, t);
        }
        [TestMethod] public void ToLowerTest() => test(Operation.ToLower, rndStr);
        [TestMethod] public void ToUpperTest() => test(Operation.ToUpper, rndStr);
        [TestMethod] public void TrimTest() => test(Operation.Trim, "    " + rndStr + "    ");
        [TestMethod] public void SubstringTest() {
            var s = rndStr;
            var t = rndStr;
            test(Operation.Substring, t + s + rndStr, t.Length, s.Length);
        }
        [TestMethod] public void SubstringToEndTest() {
            var t = rndStr;
            test(Operation.Substring, t + rndStr, t.Length);
        }
        private void test(Operation op, object vA, object vB = null, object vC = null) {
            void doTest(object a, object b, object c = null) {
                var varA = VariableFactory.Create("a", a);
                var varB = VariableFactory.Create("b", b);
                var varC = VariableFactory.Create("c", c);
                obj.Set(varA);
                if (vB is not null) obj.Set(varB);
                if (vC is not null) obj.Set(varC);
                var expected = vC == null ? varA.Calculate(op, varB) : varA.Calculate(op, varB, varC);
                obj.Perform(op);
                var actual = obj.Get() as IVariable;
                Assert.IsNotNull(actual);
                Assert.IsNotInstanceOfType(actual, typeof(RuleError));
                Assert.AreEqual(expected.GetValue(), actual.GetValue());
                Assert.AreEqual(expected.Name, actual.Name);
            }
            doTest(vA, vB, vC);
        }
    }
}
