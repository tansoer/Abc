using Abc.Data.Common;
using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass]
    public class AttributeTypeDataTests :SealedTests<AttributeTypeData, EntityBaseData> {
        [TestMethod] public void ElementTypeIdTest() => isNullable<string>();
        [TestMethod] public void IsMandatoryTest() => isProperty<bool>();
    }
}