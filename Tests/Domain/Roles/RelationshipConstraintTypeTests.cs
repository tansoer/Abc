using Abc.Aids.Random;
using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Roles
{
    [TestClass]
    public class RelationshipConstraintTypeTests :SealedTests<RelationshipConstraintType, Entity<RelationshipConstraintTypeData>> {
        protected override RelationshipConstraintType createObject() =>
            new RelationshipConstraintType(GetRandom.ObjectOf<RelationshipConstraintTypeData>());

        [TestMethod]
        public void CanFormRelationshipTest() {
            isFalse(obj.CanFormRelationship(new PartyRole(GetRandom.ObjectOf<PartyRoleData>()), new PartyRole(GetRandom.ObjectOf<PartyRoleData>())));
            var consumer = new PartyRole(new PartyRoleData {
                PartyRoleTypeId = obj.consumerPartyRoleTypeId
            });
            isFalse(obj.CanFormRelationship(consumer, new PartyRole(GetRandom.ObjectOf<PartyRoleData>())));
            var provider = new PartyRole(new PartyRoleData {
                PartyRoleTypeId = obj.providerPartyRoleTypeId
            });
            isTrue(obj.CanFormRelationship(consumer, provider));
        }
    }
}