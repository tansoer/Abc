using Abc.Facade.Common;
using Abc.Facade.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Roles {
    [TestClass] public class PartyRelationshipBaseViewTests 
        :AbstractTests<PartyRelationshipBaseView, RelationshipView> {
        private class testClass :PartyRelationshipBaseView { }
        protected override PartyRelationshipBaseView createObject() => random<testClass>();
    }
}