using Abc.Data.Common;
using Abc.Data.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Roles {
    [TestClass]
    public class PartyRelationshipBaseTypeDataTests :AbstractTests<PartyRelationshipBaseTypeData, RelationshipTypeData> {
        private class testClass : PartyRelationshipBaseTypeData { }
        protected override PartyRelationshipBaseTypeData createObject() => new testClass();
        [TestMethod] public void RuleSetIdTest() => isNullable<string>();
    }
}
