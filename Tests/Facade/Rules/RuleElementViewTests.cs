using Abc.Aids.Calculator;
using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Facade.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DateTime = System.DateTime;

namespace Abc.Tests.Facade.Rules {

    [TestClass]
    public class RuleElementViewTests : SealedTests<RuleElementView, CommonRuleIdView> {

        [TestInitialize]
        public void TestInitailize() {
            base.TestInitialize();
            obj = GetRandom.ObjectOf<RuleElementView>();
        }

        [TestMethod] public void RuleElementTypeTest() => isProperty<RuleElementType>("Element Type");

        [TestMethod] public void UnitIdTest() => isNullableProperty<string>("Unit");

        [TestMethod] public void CurrencyIdTest() => isNullableProperty<string>("Currency");

        [TestMethod] public void RuleContextIdTest() => isNullableProperty<string>("Rule Context");

        [TestMethod] public void IndexTest() => isProperty<int>();
        
        [TestMethod] public void StringValueTest() => isNullableProperty<string>("Value");

        [TestMethod] public void BooleanValueTest() => isProperty<bool>("Value");

        [TestMethod] public void IntegerValueTest() => isProperty<int>("Value");

        [TestMethod] public void DoubleValueTest() => isProperty<double>("Value");

        [TestMethod] public void DecimalValueTest() => isProperty<decimal>("Value");

        [TestMethod] public void DateTimeValueTest() => isProperty<DateTime>("Value");

        [TestMethod] public void OperationTest() => isProperty<Operation>(nameof(obj.Operation));

        [TestMethod]
        public void ValueTest() {
            testValue(RuleElementType.Boolean, Variable.ToString(obj.BooleanValue));
            testValue(RuleElementType.String, obj.StringValue);
            testValue(RuleElementType.DateTime, Variable.ToString(obj.DateTimeValue));
            testValue(RuleElementType.Integer, Variable.ToString(obj.IntegerValue));
            testValue(RuleElementType.Decimal, Variable.ToString(obj.DecimalValue));
            testValue(RuleElementType.Double, Variable.ToString(obj.DoubleValue));
            testValue(RuleElementType.Quantity, $"{Variable.ToString(obj.DoubleValue)} {obj.UnitId}");
            testValue(RuleElementType.Money, $"{Variable.ToString(obj.DecimalValue)} {obj.CurrencyId}");
            testValue(RuleElementType.Error, obj.StringValue);
            testValue(RuleElementType.Unspecified, string.Empty);
        }

        private void testValue(RuleElementType t, string expected) {
            obj.RuleElementType = t;
            isReadOnly(obj, nameof(obj.Value), expected);
        }

    }

}