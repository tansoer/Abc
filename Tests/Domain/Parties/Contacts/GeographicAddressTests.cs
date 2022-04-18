using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Data.Parties;
using Abc.Domain.Parties.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties.Contacts {

    [TestClass]
    public class GeographicAddressTests : SealedTests<GeographicAddress, PartyContact<PartyContactData>> {

        protected override GeographicAddress createObject() {
            var d = GetRandom.ObjectOf<PartyContactData>();
            d.ContactType = Abc.Aids.Enums.ContactType.Postal;
            return new GeographicAddress(d);
        }
        [TestMethod] public void AddressTest() => isReadOnly(obj.Data.Name);

        [TestMethod] public void CityTest() => isReadOnly(obj.Data.CityOrAreaCode);

        [TestMethod] public void RegionOrStateTest() => isReadOnly(obj.Data.RegionOrStateOrCountryCode);

        [TestMethod] public void ZipOrPostCodeTest() => isReadOnly(obj.Data.Code);

        [TestMethod] public void CountryIdTest() => isReadOnly(obj.Data.CountryId);

        [TestMethod]
        public async Task DevicesTest()
            => await testListAsync<DeviceRegistration, DeviceRegistrationData, IDeviceRegistrationsRepo>(
                d => d.PostalAddressId = obj.Id, d => new DeviceRegistration(d));

        [TestMethod]
        public async Task CountryTest()
            => await testItemAsync<CountryData, Country, ICountriesRepo>(
                obj.CountryId, () => obj.Country.Data, d => new Country(d));

        [TestMethod]
        public void ToStringTest() {
            var c = obj.Country.IsUnspecified ? string.Empty : obj.Country.Name;
            var adr = $"{obj.Address} {obj.City} {obj.RegionOrState} {obj.ZipOrPostCode} {c}".Trim();
            Assert.AreEqual(adr, obj.ToString());
        }
    }

}