using Abc.Data.Parties;
using Abc.Domain.Parties.Contacts;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {
    public sealed class DeviceRegistrationsRepo : 
        EntityRepo<DeviceRegistration, DeviceRegistrationData>,
        IDeviceRegistrationsRepo {
        public DeviceRegistrationsRepo(PartyDb c = null) 
            : base(c, c?.TelecomDeviceRegistrations) { }
    }
}

