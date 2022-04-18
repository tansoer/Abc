using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Parties;
using Abc.Domain.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Roles
{
    [TestClass]
    public class PartyRoleConstraintTypeTests : SealedTests<PartyRoleConstraintType, Entity<PartyRoleConstraintTypeData>> {
        private PartyType _partyType = PartyType.Unspecified;
        protected override PartyRoleConstraintType createObject() {
            var d = GetRandom.ObjectOf<PartyRoleConstraintTypeData>();
            d.PartyType = _partyType;
            return new PartyRoleConstraintType(d);
        }

        [DataRow(PartyType.Organization)]
        [DataRow(PartyType.Person)]
        [TestMethod]
        public void CanPlayRoleTest(PartyType partyType) {
            _partyType = partyType;
            obj = createObject();
            var partyData = GetRandom.ObjectOf<PartyData>();
            partyData.PartyType = partyType;
            partyData.OrganizationTypeId = obj.organizationTypeId;

            isTrue(obj.CanPlayRole(PartyFactory.Create(partyData)));
        }

        [TestMethod]
        public void PartyTypeTest() {
            var o = new PartyRoleConstraintType(GetRandom.ObjectOf<PartyRoleConstraintTypeData>());
            areEqual(o.PartyType, o.Data.PartyType);
            areEqual(new PartyRoleConstraintType().PartyType, PartyType.Unspecified);
        }
    }
}