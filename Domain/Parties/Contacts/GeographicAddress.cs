using System.Collections.Generic;
using Abc.Data.Parties;

namespace Abc.Domain.Parties.Contacts {

    public sealed class GeographicAddress : PartyContact<PartyContactData>, IPartyContact {
        internal static string adrId => nameOf<DeviceRegistrationData>(x => x.PostalAddressId);
        public GeographicAddress() : this(null) { }
        public GeographicAddress(PartyContactData d) : base(d) { }
        public string CountryId => get(Data?.CountryId);
        public IReadOnlyList<DeviceRegistration> Devices => list<IDeviceRegistrationsRepo, DeviceRegistration>(adrId, Id);
        public string Address => get(Data?.Name);
        public string City => get(Data?.CityOrAreaCode);
        public string RegionOrState => get(Data?.RegionOrStateOrCountryCode);
        public string ZipOrPostCode => get(Data?.Code);
        public Country Country => item<ICountriesRepo, Country>(CountryId);
        public override string ToString() => country().Trim();
        internal string country() => (Country.IsUnspecified ? zip() : zip() + $" {Country?.OfficialName}").Trim();
        internal string zip() => (isNull(ZipOrPostCode) ? region() : region() + $" {ZipOrPostCode}").Trim();
        internal string region() => (isNull(RegionOrState)? city() : city() + $" {RegionOrState}").Trim();
        private string city() => (isNull(City)? adr() : adr() + $" {City}").Trim();
        internal string adr() => Address.Trim();
    }

}