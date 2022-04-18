using Abc.Data.Roles;
using Abc.Domain.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Roles
{
    [TestClass]
    public class PartyRelationshipTests :SealedTests<PartyRelationship, PartyBaseRelationship<PartyRelationshipData>> {

        [TestMethod]
        public async Task TypeTest() {
            await testItemAsync<PartyRelationshipTypeData, PartyRelationshipType, IPartyRelationshipTypesRepo>(
                obj.RelationshipTypeId, () => obj.Type.Data, d => new PartyRelationshipType(d));
            obj = createObject();
            isNotNull(obj.Type);
            isNotNull(obj.Type.Data);
        }
    }
}