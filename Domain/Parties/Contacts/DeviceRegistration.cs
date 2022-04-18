using Abc.Data.Parties;
using Abc.Domain.Common;

namespace Abc.Domain.Parties.Contacts {

    public sealed class DeviceRegistration : Entity<DeviceRegistrationData> {
        public DeviceRegistration() : this(null) { }
        public DeviceRegistration(DeviceRegistrationData d) : base(d) { }
        internal string addressId => get(Data?.PostalAddressId);
        internal string deviceId => get(Data?.TelecomDeviceId);
    }
}
