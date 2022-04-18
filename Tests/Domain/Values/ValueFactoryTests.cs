using Abc.Aids.Calculator;
using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Values {

    [TestClass]
    public class ValueFactoryTests : TestsBase {

        private ValueData d;

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(ValueFactory);
            d = GetRandom.ObjectOf<ValueData>();
        }
        [TestMethod]
        public void CreateTest() {
            var o = ValueFactory.Create(null) as BaseCommonValue;
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, typeof(ErrorValue));
            Assert.IsTrue(o.IsUnspecified);
        }
        [TestMethod] public void CreateStringTest() => testCreate<StringValue>(ValueType.String);
        [TestMethod] public void CreateBooleanTest() => testCreate<BooleanValue>(ValueType.Boolean);
        [TestMethod] public void CreateDateTimeTest() => testCreate<DateTimeValue>(ValueType.DateTime);
        [TestMethod] public void CreateDecimalTest() => testCreate<DecimalValue>(ValueType.Decimal);
        [TestMethod] public void CreateDoubleTest() => testCreate<DoubleValue>(ValueType.Double);
        [TestMethod] public void CreateMoneyTest() => testCreate<MoneyValue>(ValueType.Money);
        [TestMethod] public void CreateQuantityTest() => testCreate<QuantityValue>(ValueType.Quantity);
        [TestMethod] public void CreateErrorTest() => testCreate<ErrorValue>(ValueType.Error);
        [TestMethod] public void CreateUnspecifiedTest() => testCreate<ErrorValue>(ValueType.Unspecified);

        private void testCreate<T>(ValueType t) where T : IValue {
            d.ValueType = t;
            var o = ValueFactory.Create(d) as BaseCommonValue;
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, typeof(T));
            allPropertiesAreEqual(d, o.Data);
        }

    }

}
