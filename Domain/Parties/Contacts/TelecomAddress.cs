using System.Collections.Generic;
using Abc.Data.Common;
using Abc.Data.Parties;

namespace Abc.Domain.Parties.Contacts {

    public sealed class TelecomAddress : PartyContact<PartyContactData>, IPartyContact {
        public TelecomAddress() : base(null) { }
        public TelecomAddress(PartyContactData d) : base(d) { }
        public IReadOnlyList<DeviceRegistration> Registrations
            => list<IDeviceRegistrationsRepo, DeviceRegistration>(deviceId, Id);
        public string CountryCode => get(Data?.RegionOrStateOrCountryCode);
        public string NationalDirectDialingPrefix => get(Data?.NationalDirectDialingPrefix);
        public string AreaCode => get(Data?.CityOrAreaCode);
        public string Number => get(Data?.Name);
        public string Extension => get(Data?.Code);
        public TelecomDeviceType Device => Data?.Device ?? TelecomDeviceType.NotKnown;
        public override string ToString() => phoneNo();

        internal static string deviceId => nameOf<DeviceRegistrationData>(x => x.TelecomDeviceId);
        internal string phoneNo() => ext();
        internal string ext() => isNull(Extension) ? number() : number() + $" ext.{Extension}";
        internal string number() => area() + Number;
        internal string area() => isNull(AreaCode) ? prefix() : prefix() + AreaCode + " ";
        internal string prefix() => isNull(NationalDirectDialingPrefix) ? country() : country() + $"({NationalDirectDialingPrefix})";
        internal string country() => isNull(CountryCode) ? string.Empty : $"+{CountryCode} ";
    }
}