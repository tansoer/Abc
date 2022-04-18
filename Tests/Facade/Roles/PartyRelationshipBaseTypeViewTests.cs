using Abc.Facade.Common;
using Abc.Facade.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Roles {
    [TestClass] public class PartyRelationshipBaseTypeViewTests 
        :AbstractTests<PartyRelationshipBaseTypeView, RelationshipTypeView> {
        private class testClass :PartyRelationshipBaseTypeView { }
        protected override PartyRelationshipBaseTypeView createObject() => random<testClass>();
        [TestMethod] public void RuleSetIdTest() => isNullableProperty<string>("Rule set");
    }
}