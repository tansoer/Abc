using Abc.Data.Common;
using Abc.Data.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Roles {
    [TestClass]
    public class PartyRelationshipBaseDataTests :AbstractTests<PartyRelationshipBaseData, RelationshipData> {
        private class testClass : PartyRelationshipBaseData { };
        protected override PartyRelationshipBaseData createObject() => new testClass(); 
    }
}
