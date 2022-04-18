using Abc.Aids.Random;
using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Roles {
    [TestClass] public class PartyBaseRelationshipTests
        :AbstractTests<PartyBaseRelationship<TestClassForPartyRelationshipBaseData>,
            Relationship<TestClassForPartyRelationshipBaseData>> {
        private class testClass :PartyBaseRelationship<TestClassForPartyRelationshipBaseData> {
            public testClass(TestClassForPartyRelationshipBaseData d) :base(d) { }
        }
        protected override PartyBaseRelationship<TestClassForPartyRelationshipBaseData>
            createObject()
            => new testClass(GetRandom.ObjectOf<TestClassForPartyRelationshipBaseData>());
        [TestMethod] public async Task ConsumerTest() {
            await testItemAsync<PartyRoleData, PartyRole, IPartyRolesRepo>(
                obj.ConsumerEntityId, () => obj.Consumer.Data, d => new PartyRole(d));
        }
        [TestMethod] public async Task ProviderTest() {
            await testItemAsync<PartyRoleData, PartyRole, IPartyRolesRepo>(
                obj.ProviderEntityId, () => obj.Provider.Data, d => new PartyRole(d));
        }
    }
}