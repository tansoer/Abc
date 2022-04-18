using Abc.Facade.Common;
using Abc.Facade.Products;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValueType = Abc.Data.Common.ValueType;
using Abc.Aids.Extensions;

namespace Abc.Tests.Facade.Products {
    [TestClass] public class FeatureSpecViewTests :  SealedTests<FeatureSpecView, EntityBaseView> {
        [TestMethod] public void UnitIdTest() => isNullableProperty<string>("Unit");
        [TestMethod] public void CurrencyIdTest() => isNullableProperty<string>("Currency");
        [TestMethod] public void ValueTypeTest() => isProperty<ValueType>("Value type");
        [TestMethod] public void StringValueTest() => isNullableProperty<string>("Value");
        [TestMethod] public void IntegerValueTest() => isProperty<int>("Value");
        [TestMethod] public void BooleanValueTest() => isProperty<bool>("Value");
        [TestMethod] public void DateTimeValueTest() => isProperty<DateTime>("Value");
        [TestMethod] public void DoubleValueTest() => isProperty<double>("Value");
        [TestMethod] public void DecimalValueTest() => isProperty<decimal>("Value");
        [TestMethod] public void FeatureValueTest() {
            isReadOnly();
            hasDisplayName(nameof(obj.FeatureValue), "Value");
            areEqual(obj.FeatureValue, obj.ToString());
        }
        [DataRow(ValueType.Boolean)]
        [DataRow(ValueType.String)]
        [DataRow(ValueType.DateTime)]
        [DataRow(ValueType.Decimal)]
        [DataRow(ValueType.Double)]
        [DataRow(ValueType.Integer)]
        [DataRow(ValueType.Quantity)]
        [DataRow(ValueType.Money)]
        [DataRow(ValueType.Unspecified)]
        [DataRow(ValueType.Unspecified)]
        [TestMethod] public void ToStringTest(ValueType t) {
            obj.ValueType = t;
            var expected = t switch {
                ValueType.Boolean => Variable.ToString(obj.BooleanValue),
                ValueType.String => obj.StringValue,
                ValueType.Integer => Variable.ToString(obj.IntegerValue),
                ValueType.Decimal => Variable.ToString(obj.DecimalValue),
                ValueType.Double => Variable.ToString(obj.DoubleValue),
                ValueType.DateTime => Variable.ToString(obj.DateTimeValue),
                ValueType.Quantity => $"{Variable.ToString(obj.DoubleValue)} {obj.UnitId}",
                ValueType.Money => $"{Variable.ToString(obj.DecimalValue)} {obj.CurrencyId}",
                ValueType.Error => obj.StringValue,
                ValueType.Unspecified => obj.StringValue,
                _ => string.Empty
            };
            areEqual(expected, obj.ToString());
        }
    }
}
