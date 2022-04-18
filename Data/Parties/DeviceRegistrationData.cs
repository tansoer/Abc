using Abc.Data.Common;

namespace Abc.Data.Parties {

    public sealed class DeviceRegistrationData : EntityBaseData {
        public string PostalAddressId { get; set; }
        public string TelecomDeviceId { get; set; }
    }
}