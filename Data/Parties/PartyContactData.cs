using Abc.Aids.Enums;
using Abc.Data.Common;

namespace Abc.Data.Parties {

    public sealed class PartyContactData : EntityBaseData, IPartyContactData {

        public string CountryId { get; set; }

        public string NationalDirectDialingPrefix { get; set; }

        public TelecomDeviceType Device { get; set; }

        public string CityOrAreaCode { get; set; }

        public string RegionOrStateOrCountryCode { get; set; }

        public ContactType ContactType { get; set; }
    }

}