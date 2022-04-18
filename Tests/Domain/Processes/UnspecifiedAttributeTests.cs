using Abc.Data.Processes;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Processes {

    [TestClass]
    public class UnspecifiedAttributeTests :SealedTests<UnspecifiedAttribute, AttributeValue> {
        protected override UnspecifiedAttribute createObject() {
            var d = random<AttributeValueData>();
            d.Type = AttributeValueType.Unspecified;
            return AttributeValuesFactory.Create(d) as UnspecifiedAttribute;
        }
    }
}