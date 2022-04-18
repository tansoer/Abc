using Abc.Aids.Enums;
using Abc.Aids.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Enums {

    [TestClass]
    public class MonthNameTests : TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(MonthName);
        [TestMethod] public void CountTest() => areEqual(13, GetEnum.Count<MonthName>());
        [TestMethod] public void UnknownTest() => areEqual(0, (int) MonthName.Unknown);
        [TestMethod] public void JanuaryTest() => areEqual(1, (int) MonthName.January);
        [TestMethod] public void FebruaryTest() => areEqual(2, (int) MonthName.February);
        [TestMethod] public void MarchTest() => areEqual(3, (int) MonthName.March);
        [TestMethod] public void AprilTest() => areEqual(4, (int) MonthName.April);
        [TestMethod] public void MayTest() => areEqual(5, (int) MonthName.May);
        [TestMethod] public void JuneTest() => areEqual(6, (int) MonthName.June);
        [TestMethod] public void JulyTest() => areEqual(7, (int) MonthName.July);
        [TestMethod] public void AugustTest() => areEqual(8, (int) MonthName.August);
        [TestMethod] public void SeptemberTest() => areEqual(9, (int) MonthName.September);
        [TestMethod] public void OctoberTest() => areEqual(10, (int) MonthName.October);
        [TestMethod] public void NovemberTest() => areEqual(11, (int) MonthName.November);
        [TestMethod] public void DecemberTest() => areEqual(12, (int) MonthName.December);
    }
}
