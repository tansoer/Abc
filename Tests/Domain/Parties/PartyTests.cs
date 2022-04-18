using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Capabilities;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Parties.Identifiers;
using Abc.Domain.Parties.Names;
using Abc.Domain.Parties.Preferences;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties {

    [TestClass] public class PartyTests :
        AbstractTests<Party<OrganizationName>, Entity<PartyData>> {
        private class testClass :Party<OrganizationName> {
            public testClass(PartyData d) :base(d) { }
        }

        protected override Party<OrganizationName> createObject() {
            var d = GetRandom.ObjectOf<PartyData>();
            d.PartyType = PartyType.Organization;
            return new testClass(d);
        }

        [TestMethod] public void GetNameTest() => areEqual(obj.OfficialName.ToString(), obj.GetName());

        [TestMethod] public async Task OfficialNameTest() {
            await NamesTest();
            var o = isReadOnly() as OrganizationName;
            Assert.IsNotNull(o);
            if (o.NameType != NameType.Official) {
                var n = obj.Names[0];
                if (o.Id == n.Id) return;
            } else if (obj.Names.Any(n => o.Id == n.Id))
                return;

            Assert.Fail();
        }

        [TestMethod] public void GetPhoneTest() => isNotNull(obj.GetPhone());
        [TestMethod] public void GetAddressTest() => isNotNull(obj.GetAddress());
        [TestMethod] public void GetEmailTest() => isNotNull(obj.GetEmail());

        [TestMethod] public async Task ContactsTest()
            => await testListAsync<PartyContactUsage, PartyContactUsageData, IPartyContactUsagesRepo>(
                d => d.PartyId = obj.Id,
                d => new PartyContactUsage(d));

        [TestMethod] public void AddressesTest() => isReadOnly();
        [TestMethod] public void EmailsTest() => isReadOnly();
        [TestMethod] public void PhonesTest() => isReadOnly();

        [TestMethod] public async Task IdentifiersTest()
            => await testListAsync<RegisteredIdentifier, RegisteredIdentifierData, IRegisteredIdentifiersRepo>(
                d => d.PartyId = obj.Id,
                d => new RegisteredIdentifier(d));

        [TestMethod] public async Task CapabilitiesTest()
            => await testListAsync<PartyCapability, PartyCapabilityData, IPartyCapabilitiesRepo>(
                d => d.PartyId = obj.Id,
                d => new PartyCapability(d));

        [TestMethod] public async Task PreferencesTest()
            => await testListAsync<Preference, PreferenceData, IPreferencesRepo>(
                d => d.PartyId = obj.Id,
                d => new Preference(d));

        [TestMethod] public async Task AuthenticationsTest()
            => await testListAsync<Authentication, AuthenticationData, IAuthenticationsRepo>(
                d => d.PartyId = obj.Id,
                d => new Authentication(d));

        [TestMethod] public async Task RolesTest()
            => await testListAsync<PartyRole, PartyRoleData, IPartyRolesRepo>(
                d => d.PartyId = obj.Id,
                d => new PartyRole(d));

        [TestMethod] public async Task NamesTest()
            => await testListAsync<PartyName, PartyNameData, IPartyNamesRepo>(
                d => d.PartyId = obj.Id,
                d => {
                    d.PartyType = PartyType.Organization;
                    return PartyNameFactory.Create(d);
                });

        [TestMethod] public void ToPartySummaryTest() {
            PartySummary s = obj.ToPartySummary();
            isNotNull(s);
            areEqual(s.Name, obj.GetName());
            areEqual(s.Address, obj.GetAddress());
            areEqual(s.EmailAddress, obj.GetEmail());
            areEqual(s.PhoneNumber, obj.GetPhone());
            areEqual(s.PartyId, obj.Id);
            areEqual(s.Details, obj.Details);
        }

        [TestMethod] public void IsCapableTest() {
            isFalse(obj.IsCapable((RuleSet)null));
            isFalse(obj.IsCapable((IReadOnlyList<Responsibility>)null));
            //TODO Gunnar: Test for not null cases?
        }
    }
}
