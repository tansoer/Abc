using Abc.Aids.Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Calculator {

    [TestClass]
    public class OperationTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Operation);
        [TestMethod] public void DummyTest() => Assert.AreEqual(0, (int)Operation.Dummy);
        [TestMethod] public void AddTest() => Assert.AreEqual(1, (int)Operation.Add);
        [TestMethod] public void SubtractTest() => Assert.AreEqual(2, (int)Operation.Subtract);
        [TestMethod] public void MultiplyTest() => Assert.AreEqual(3, (int)Operation.Multiply);
        [TestMethod] public void DivideTest() => Assert.AreEqual(4, (int)Operation.Divide);
        [TestMethod] public void PowerTest() => Assert.AreEqual(11, (int)Operation.Power);
        [TestMethod] public void InverseTest() => Assert.AreEqual(12, (int)Operation.Inverse);
        [TestMethod] public void OppositeTest() => Assert.AreEqual(13, (int)Operation.Opposite);
        [TestMethod] public void SquareTest() => Assert.AreEqual(14, (int)Operation.Square);
        [TestMethod] public void SqrtTest() => Assert.AreEqual(15, (int)Operation.Sqrt);
        [TestMethod] public void AndTest() => Assert.AreEqual(21, (int)Operation.And);
        [TestMethod] public void OrTest() => Assert.AreEqual(22, (int)Operation.Or);
        [TestMethod] public void XorTest() => Assert.AreEqual(23, (int)Operation.Xor);
        [TestMethod] public void NotTest() => Assert.AreEqual(24, (int)Operation.Not);
        [TestMethod] public void IsEqualTest() => Assert.AreEqual(25, (int)Operation.IsEqual);
        [TestMethod] public void IsGreaterTest() => Assert.AreEqual(26, (int)Operation.IsGreater);
        [TestMethod] public void IsLessTest() => Assert.AreEqual(27, (int)Operation.IsLess);
        [TestMethod] public void GetYearTest() => Assert.AreEqual(31, (int)Operation.GetYear);
        [TestMethod] public void GetMonthTest() => Assert.AreEqual(32, (int)Operation.GetMonth);
        [TestMethod] public void GetDayTest() => Assert.AreEqual(33, (int)Operation.GetDay);
        [TestMethod] public void GetHourTest() => Assert.AreEqual(34, (int)Operation.GetHour);
        [TestMethod] public void GetMinuteTest() => Assert.AreEqual(35, (int)Operation.GetMinute);
        [TestMethod] public void GetSecondTest() => Assert.AreEqual(36, (int)Operation.GetSecond);
        [TestMethod] public void GetAgeTest() => Assert.AreEqual(41, (int)Operation.GetAge);
        [TestMethod] public void GetIntervalTest() => Assert.AreEqual(42, (int)Operation.GetInterval);
        [TestMethod] public void ToYearsTest() => Assert.AreEqual(43, (int)Operation.ToYears);
        [TestMethod] public void ToMonthsTest() => Assert.AreEqual(44, (int)Operation.ToMonths);
        [TestMethod] public void ToDaysTest() => Assert.AreEqual(45, (int)Operation.ToDays);
        [TestMethod] public void ToHoursTest() => Assert.AreEqual(46, (int)Operation.ToHours);
        [TestMethod] public void ToMinutesTest() => Assert.AreEqual(47, (int)Operation.ToMinutes);
        [TestMethod] public void ToSecondsTest() => Assert.AreEqual(48, (int)Operation.ToSeconds);
        [TestMethod] public void AddSecondsTest() => Assert.AreEqual(51, (int)Operation.AddSeconds);
        [TestMethod] public void AddMinutesTest() => Assert.AreEqual(52, (int)Operation.AddMinutes);
        [TestMethod] public void AddHoursTest() => Assert.AreEqual(53, (int)Operation.AddHours);
        [TestMethod] public void AddDaysTest() => Assert.AreEqual(54, (int)Operation.AddDays);
        [TestMethod] public void AddMonthsTest() => Assert.AreEqual(55, (int)Operation.AddMonths);
        [TestMethod] public void AddYearsTest() => Assert.AreEqual(56, (int)Operation.AddYears);
        [TestMethod] public void GetLengthTest() => Assert.AreEqual(61, (int)Operation.GetLength);
        [TestMethod] public void SubstringTest() => Assert.AreEqual(62, (int)Operation.Substring);
        [TestMethod] public void ContainsTest() => Assert.AreEqual(63, (int)Operation.Contains);
        [TestMethod] public void EndsWithTest() => Assert.AreEqual(64, (int)Operation.EndsWith);
        [TestMethod] public void StartsWithTest() => Assert.AreEqual(65, (int)Operation.StartsWith);
        [TestMethod] public void ToUpperTest() => Assert.AreEqual(66, (int)Operation.ToUpper);
        [TestMethod] public void ToLowerTest() => Assert.AreEqual(67, (int)Operation.ToLower);
        [TestMethod] public void TrimTest() => Assert.AreEqual(68, (int)Operation.Trim);
        [TestMethod] public void ToContainsExpressionTest() => Assert.AreEqual(69, (int)Operation.ToContainsExpression);
        [TestMethod] public void ToEqualsExpressionTest() => Assert.AreEqual(70, (int)Operation.ToEqualsExpression);
        [TestMethod] public void ExpressionAndTest() => Assert.AreEqual(71, (int)Operation.ExpressionAnd);
    }
}
