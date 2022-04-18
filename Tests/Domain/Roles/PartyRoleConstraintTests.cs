using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Parties;
using Abc.Domain.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Roles {
    [TestClass]
    public class PartyRoleConstraintTests : SealedTests<PartyRoleConstraint, Entity<PartyRoleConstraintData>> {
        protected override PartyRoleConstraint createObject() =>
            new (GetRandom.ObjectOf<PartyRoleConstraintData>());

        [DataRow(PartyType.Person)]
        [DataRow(PartyType.Organization)]
        [TestMethod]
        public void CanPlayRoleTest(PartyType partyType) {
            var party = createPersonAndConstraintType(partyType);

            isTrue(obj.CanPlayRole(party));
            isFalse(obj.CanPlayRole(null));
        }
        private IParty createPersonAndConstraintType(PartyType partyType) {
            var constraintTypeData = GetRandom.ObjectOf<PartyRoleConstraintTypeData>();
            constraintTypeData.Id = obj.Data.RoleConstraintTypeId;
            constraintTypeData.PartyType = partyType;
            constraintTypeData.OrganizationTypeId = rndStr;
            var partyData = GetRandom.ObjectOf<PartyData>();
            partyData.PartyType = partyType;
            partyData.OrganizationTypeId = constraintTypeData.OrganizationTypeId;

            GetRepo.Instance<IPartyRoleConstraintTypesRepo>().Add(new PartyRoleConstraintType(constraintTypeData));
            return PartyFactory.Create(partyData);
        }

        [TestMethod]
        public async Task TypeTest() {
            await testItemAsync<PartyRoleConstraintTypeData, PartyRoleConstraintType, IPartyRoleConstraintTypesRepo>(
                obj.typeId, () => obj.Type.Data, d => new PartyRoleConstraintType(d));
            obj = createObject();
            isNotNull(obj.Type);
            isNotNull(obj.Type.Data);
        }
    }
}
