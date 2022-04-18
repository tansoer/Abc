using Abc.Aids.Enums;
using Abc.Data.Common;
using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass] public class AttributeValueDataTests :SealedTests<AttributeValueData, EntityBaseData> {
        protected override AttributeValueData createObject() => random<AttributeValueData>();
        [TestMethod] public void ValueTest() => isNullable<ValueData>();
        [TestMethod] public void AttributeTypeIdTest() => isNullable<string>();
        [TestMethod] public void ProcessElementIdTest() => isNullable<string>();
        [TestMethod] public void EqualityTest() => isProperty<Relation>();
        [TestMethod] public void TypeTest() => isProperty<AttributeValueType>();
    }
}