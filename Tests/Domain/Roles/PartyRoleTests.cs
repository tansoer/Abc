using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Roles;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Preferences;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Roles {

    [TestClass]
    public class PartyRoleTests : SealedTests<PartyRole, BasePartyAttribute<PartyRoleData>> {

        protected override PartyRole createObject() => new (GetRandom.ObjectOf<PartyRoleData>());

        [TestMethod]
        public async Task PreferencesTest()
            => await testListAsync<Preference, PreferenceData, IPreferencesRepo>(
                d => d.PartyRoleId = obj.Id, d => new Preference(d));

        [TestMethod]
        public async Task ResponsibilitiesTest()
            => await testListAsync<AssignedResponsibility, AssignedResponsibilityData, IAssignedResponsibilitiesRepo>(
                d => d.PartyRoleId = obj.Id, d => new AssignedResponsibility(d));

        [TestMethod]
        public void AssignTest() {
            var count = obj.Responsibilities.Count;
            areEqual(count, obj.Responsibilities.Count);
            obj.Assign(GetRandom.ObjectOf<Responsibility>(), GetRandom.ObjectOf<PartySignature>());

            areEqual(count + 1, obj.Responsibilities.Count);
        }

        [TestMethod]
        public void RemoveTest() {
            var count = obj.Responsibilities.Count;
            obj.Assign(GetRandom.ObjectOf<Responsibility>(), GetRandom.ObjectOf<PartySignature>());
            areEqual(count + 1, obj.Responsibilities.Count);

            var responsibility = obj?.Responsibilities?[0];
            PartyRole.Remove(responsibility);
            areEqual(count, obj.Responsibilities.Count);
        }

        [TestMethod]
        public async Task PartyTest() {
            await testItemAsync<PartyData, IParty, IPartiesRepo>(
                obj.PartyId, () => obj.Party.Data, PartyFactory.Create);
            obj = createObject();
            isNotNull(obj.Party);
            isNotNull(obj.Party.Data);
        }

        [TestMethod]
        public async Task TypeTest() {
            await testItemAsync<PartyRoleTypeData, PartyRoleType, IPartyRoleTypesRepo>(
                obj.typeId, () => obj.Type.Data, d => new PartyRoleType(d));
            obj = createObject();
            isNotNull(obj.Type);
            isNotNull(obj.Type.Data);
        }
    }
}
