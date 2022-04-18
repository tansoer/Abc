using Abc.Aids.Reflection;
using Abc.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Common {
    [TestClass]
    public class ValueTypeTests : TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(ValueType);
        [TestMethod] public void CountTest() => areEqual(10, GetEnum.Count<ValueType>());
        [TestMethod]  public void UnspecifiedTest() => areEqual(0, (int) ValueType.Unspecified);
        [TestMethod] public void BooleanTest() => areEqual(1, (int) ValueType.Boolean);
        [TestMethod] public void StringTest() => areEqual(2, (int) ValueType.String);
        [TestMethod] public void IntegerTest() => areEqual(3, (int) ValueType.Integer);
        [TestMethod] public void DecimalTest() => areEqual(4, (int) ValueType.Decimal);
        [TestMethod] public void DoubleTest() => areEqual(5, (int) ValueType.Double);
        [TestMethod] public void DateTimeTest() => areEqual(6, (int) ValueType.DateTime);
        [TestMethod] public void QuantityTest() => areEqual(7, (int) ValueType.Quantity);
        [TestMethod] public void MoneyTest() => areEqual(8, (int) ValueType.Money);
        [TestMethod] public void ErrorTest() => areEqual(9, (int) ValueType.Error);
    }
}
