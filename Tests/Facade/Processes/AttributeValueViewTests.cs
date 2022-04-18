using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Data.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass]
    public class AttributeValueViewTests :SealedTests<AttributeValueView, EntityBaseView> {
        [TestMethod] public void ProcessElementIdTest() => isNullableProperty<string>("Process Element");
        [TestMethod] public void AttributeTypeIdTest() => isNullableProperty<string>("Attribute Type");
        [TestMethod] public void EqualityTest() => isProperty<Relation>("Equality");
        [TestMethod] public void TypeTest() => isProperty<AttributeValueType>("Type");
        [TestMethod] public void UnitIdTest() => isNullableProperty<string>("Unit");
        [TestMethod] public void CurrencyIdTest() => isNullableProperty<string>("Currency");
        [TestMethod] public void BooleanValueTest() => isProperty<bool>("Value");
        [TestMethod] public void IntegerValueTest() => isProperty<int>("Value");
        [TestMethod] public void DecimalValueTest() => isProperty<decimal>("Value");
        [TestMethod] public void DoubleValueTest() => isProperty<double>("Value");
        [TestMethod] public void DateTimeValueTest() => isProperty<System.DateTime>("Value");
        [TestMethod] public void StringValueTest() => isNullableProperty<string>("Value");
        [TestMethod] public void ValueTypeTest() => isProperty<Abc.Data.Common.ValueType>("Value Type");
        [TestMethod]
        public void ValueTest() {
            testValue(Abc.Data.Common.ValueType.Boolean, Variable.ToString(obj.BooleanValue));
            testValue(Abc.Data.Common.ValueType.String, obj.StringValue);
            testValue(Abc.Data.Common.ValueType.DateTime, Variable.ToString(obj.DateTimeValue));
            testValue(Abc.Data.Common.ValueType.Integer, Variable.ToString(obj.IntegerValue));
            testValue(Abc.Data.Common.ValueType.Decimal, Variable.ToString(obj.DecimalValue));
            testValue(Abc.Data.Common.ValueType.Double, Variable.ToString(obj.DoubleValue));
            testValue(Abc.Data.Common.ValueType.Quantity, $"{Variable.ToString(obj.DoubleValue)} {obj.UnitId}");
            testValue(Abc.Data.Common.ValueType.Money, $"{Variable.ToString(obj.DecimalValue)} {obj.CurrencyId}");
            testValue(Abc.Data.Common.ValueType.Error, obj.StringValue);
            testValue(Abc.Data.Common.ValueType.Unspecified, obj.StringValue);
        }
        private void testValue(Abc.Data.Common.ValueType t, string expected) {
            obj.ValueType = t;
            isReadOnly(obj, nameof(obj.Value), expected);
        }
    }
}