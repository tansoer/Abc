using Abc.Aids.Reflection;
using Abc.Data.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders {
    [TestClass] public class DiscountsTypeTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(DiscountsType);

        [TestMethod] public void CountTest() => areEqual(3, GetEnum.Count<DiscountsType>());

        [TestMethod] public void UnspecifiedTest() => areEqual(0, (int)DiscountsType.Unspecified);

        [TestMethod] public void MonetaryTest() => areEqual(1, (int)DiscountsType.Monetary);

        [TestMethod] public void PercentageTest() => areEqual(2, (int)DiscountsType.Percentage);
    }
}
