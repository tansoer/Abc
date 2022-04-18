using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Common {
    [TestClass] public class RelationshipViewTests :AbstractTests<RelationshipView, EntityBaseView> {
        private class testClass :RelationshipView { }
        protected override RelationshipView createObject() => random<testClass>();
        [TestMethod] public void RelationshipTypeIdTest() => isNullableProperty<string>("Relationship type");
        [TestMethod] public void ConsumerEntityIdTest() => isNullableProperty<string>("Consumer");
        [TestMethod] public void ProviderEntityIdTest() => isNullableProperty<string>("Provider");
    }
}