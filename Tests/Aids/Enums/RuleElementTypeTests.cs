using Abc.Aids.Enums;
using Abc.Aids.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Enums {

    [TestClass]
    public class RuleElementTypeTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(RuleElementType);

        [TestMethod]
        public void CountTest()
            => Assert.AreEqual(12, GetEnum.Count<RuleElementType>());

        [TestMethod]
        public void UnspecifiedTest() =>
            Assert.AreEqual(0, (int) RuleElementType.Unspecified);

        [TestMethod]
        public void OperandTest() =>
            Assert.AreEqual(2, (int) RuleElementType.Operand);

        [TestMethod]
        public void OperatorTest() =>
            Assert.AreEqual(1, (int) RuleElementType.Operator);

        [TestMethod]
        public void BooleanTest() =>
            Assert.AreEqual(129, (int) RuleElementType.Boolean);

        [TestMethod]
        public void StringTest() =>
            Assert.AreEqual(130, (int) RuleElementType.String);

        [TestMethod]
        public void IntegerTest() =>
            Assert.AreEqual(131, (int) RuleElementType.Integer);

        [TestMethod]
        public void DecimalTest() =>
            Assert.AreEqual(132, (int) RuleElementType.Decimal);

        [TestMethod]
        public void DoubleTest() =>
            Assert.AreEqual(133, (int) RuleElementType.Double);

        [TestMethod]
        public void DateTimeTest() =>
            Assert.AreEqual(134, (int) RuleElementType.DateTime);

        [TestMethod]
        public void QuantityTest() =>
            Assert.AreEqual(135, (int) RuleElementType.Quantity);

        [TestMethod]
        public void MoneyTest() =>
            Assert.AreEqual(136, (int) RuleElementType.Money);

        [TestMethod]
        public void ErrorTest() =>
            Assert.AreEqual(137, (int) RuleElementType.Error);


    }

}
