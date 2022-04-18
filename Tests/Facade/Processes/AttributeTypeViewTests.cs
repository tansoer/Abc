using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class AttributeTypeViewTests :SealedTests<AttributeTypeView, EntityBaseView> {
        [TestMethod] public void ElementTypeIdTest() => isNullableProperty<string>("Element Type");
        [TestMethod] public void IsMandatoryTest() => isProperty<bool>("Is Mandatory");
    }
}