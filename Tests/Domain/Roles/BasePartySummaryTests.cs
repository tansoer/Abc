using Abc.Data.Parties;
using Abc.Data.Roles;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Roles;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Roles {
    [TestClass]
    public class BasePartySummaryTests :AbstractTests<BasePartySummary, BasePartyAttribute<PartySummaryData>> {
        private class testClass :BasePartySummary {
            public testClass(PartySummaryData d) :base(d) { }
        }

        protected override BasePartySummary createObject() => new testClass(random<PartySummaryData>());
        [TestMethod] public void NameTest() => isReadOnly(obj.Data.Name);

        [TestMethod] public void AddressTest() => isReadOnly(obj.Data.Address);

        [TestMethod] public void PhoneNumberTest() => isReadOnly(obj.Data.PhoneNumber);

        [TestMethod] public void EmailAddressTest() => isReadOnly(obj.Data.EmailAddress);

        [TestMethod] public async Task RoleTest() {
            await testItemAsync<PartyRoleData, PartyRole, IPartyRolesRepo>(
                obj.partyRoleId, () => obj.Role.Data, d => new PartyRole(d));
            obj = createObject();
            isNotNull(obj.Role);
            isNotNull(obj.Role.Data);
        }
    }
}
