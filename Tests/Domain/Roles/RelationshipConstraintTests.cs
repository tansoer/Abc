using Abc.Aids.Random;
using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Roles
{
    [TestClass]
    public class RelationshipConstraintTests : SealedTests<RelationshipConstraint, Entity<RelationshipConstraintData>>{
        protected override RelationshipConstraint createObject() =>
            new (GetRandom.ObjectOf<RelationshipConstraintData>());

        [TestMethod]
        public void CanFormRelationshipTest() {
            var consumer = new PartyRole(GetRandom.ObjectOf<PartyRoleData>());
            var provider = new PartyRole(GetRandom.ObjectOf<PartyRoleData>());

            addRelationshipConstraint(consumer, provider);
            isTrue(obj.CanFormRelationship(consumer, provider));

            isFalse(obj.CanFormRelationship(new PartyRole(GetRandom.ObjectOf<PartyRoleData>()), provider));
        }

        [TestMethod]
        public async Task TypeTest() {
            await testItemAsync<RelationshipConstraintTypeData, RelationshipConstraintType, IRelationshipConstraintTypesRepo>(
                obj.constraintTypeId, () => obj.Type.Data, d => new RelationshipConstraintType(d));
            obj = createObject();
            isNotNull(obj.Type);
            isNotNull(obj.Type.Data);
        }

        private void addRelationshipConstraint(PartyRole consumer, PartyRole provider) {
            var relationShipConstraintType = new RelationshipConstraintType(new RelationshipConstraintTypeData {
                Id = obj.constraintTypeId,
                ConsumerRoleTypeId = consumer.typeId,
                ProviderRoleTypeId = provider.typeId,
                
            });
            GetRepo.Instance<IRelationshipConstraintTypesRepo>().Add(relationShipConstraintType);
        }
    }
}