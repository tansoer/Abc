using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Rules {

    [TestClass]
    public class VariableFactoryTests : TestsBase {

        private RuleElementData d;

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(VariableFactory);
            d = GetRandom.ObjectOf<RuleElementData>();
        }
        [TestMethod]
        public void CreateTest() {
            var o = VariableFactory.Create(null);
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, typeof(UnspecifiedVariable));
            Assert.IsTrue(o.IsUnspecified);
        }
        [TestMethod] public void CreateStringTest() => testCreate<StringVariable>(RuleElementType.String);
        [TestMethod] public void CreateBooleanTest() => testCreate<BooleanVariable>(RuleElementType.Boolean);
        [TestMethod] public void CreateDateTimeTest() => testCreate<DateTimeVariable>(RuleElementType.DateTime);
        [TestMethod] public void CreateDecimalTest() => testCreate<DecimalVariable>(RuleElementType.Decimal);
        [TestMethod] public void CreateDoubleTest() => testCreate<DoubleVariable>(RuleElementType.Double);
        [TestMethod] public void CreateMoneyTest() => testCreate<MoneyVariable>(RuleElementType.Money);
        [TestMethod] public void CreateQuantityTest() => testCreate<QuantityVariable>(RuleElementType.Quantity);
        [TestMethod] public void CreateErrorTest() => testCreate<RuleError>(RuleElementType.Error);
        [TestMethod] public void CreateUnspecifiedTest() => testCreate<UnspecifiedVariable>(RuleElementType.Unspecified);

        private void testCreate<T>(RuleElementType t) where T : IVariable {
            d.RuleElementType = t;
            var o = VariableFactory.Create(d);
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, typeof(T));
            allPropertiesAreEqual(d, o.Data);
        }

    }

}
