using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = System.Threading.Tasks.Task;
using ValueType = Abc.Data.Common.ValueType;

namespace Abc.Tests.Domain.Processes {
    [TestClass] public class AttributeValueTests: 
        AbstractTests<AttributeValue, Entity<AttributeValueData>> {
        private class testClass :AttributeValue {
            public testClass(AttributeValueData d): base(correctValue(d)) { }
            private static AttributeValueData correctValue(AttributeValueData d) {
                d.Value.Value = correctValue(d.Value.ValueType).ToString();
                return d;
            }
            private static object correctValue(ValueType t) => t switch {
                ValueType.Boolean => rndBool,
                ValueType.Integer => GetRandom.Int32(),
                ValueType.Decimal => GetRandom.Decimal(),
                ValueType.Money => GetRandom.Decimal(),
                ValueType.Double => GetRandom.Double(),
                ValueType.Quantity => GetRandom.Double(),
                ValueType.DateTime => rndDt,
                _ => rndStr
            };
        }
        protected override AttributeValue createObject() {
            var d = random<AttributeValueData>();
            return new testClass(d);
        }
        [TestMethod] public async Task TypeTest() =>
             await testItemAsync<AttributeTypeData, AttributeType, IAttributeTypesRepo>(
                  obj.AttributeTypeId, () => obj.Type.Data, d => new AttributeType(d));
        [TestMethod] public void AttributeTypeIdTest() => isReadOnly(obj.Data.AttributeTypeId);
        [DataRow(ValueType.Unspecified)]
        [DataRow(ValueType.Boolean)]
        [DataRow(ValueType.String)]
        [DataRow(ValueType.Integer)]
        [DataRow(ValueType.Decimal)]
        [DataRow(ValueType.Double)]
        [DataRow(ValueType.DateTime)]
        [DataRow(ValueType.Quantity)]
        [DataRow(ValueType.Money)]
        [DataRow(ValueType.Error)]
        [TestMethod] public void ValueTest(ValueType t) {
            isReadOnly();
            var d = random<AttributeValueData>();
            d.Value = random<ValueData>();
            d.Value.ValueType = t;
            obj = new testClass(d);
            var expected = obj.Data.Value.Value.GetHead(' ');
            var actual = obj?.Value?.GetValue()?.ToString().GetHead(' ');
            areEqual(expected, actual);
        }
    }
}
