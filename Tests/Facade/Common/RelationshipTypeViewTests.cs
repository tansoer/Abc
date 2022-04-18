using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Common {
    [TestClass] public class RelationshipTypeViewTests :AbstractTests<RelationshipTypeView, EntityTypeView> {
        private class testClass :RelationshipTypeView { }
        protected override RelationshipTypeView createObject() => random<testClass>();
        [TestMethod] public void ConsumerTypeIdTest() => isNullableProperty<string>("Consumer type");
        [TestMethod] public void ProviderTypeIdTest() => isNullableProperty<string>("Provider type");
    }
}