using System;
using Abc.Data.Parties;
using Abc.Domain.Parties.Contacts;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class DeviceRegistrationsRepoTests : PartyRepoTests<DeviceRegistrationsRepo,
        DeviceRegistration, DeviceRegistrationData> {
        protected override Type getBaseClass() => typeof(EntityRepo<DeviceRegistration, DeviceRegistrationData>);
        protected override DeviceRegistrationsRepo getObject(PartyDb c) => new DeviceRegistrationsRepo(c);
        protected override DbSet<DeviceRegistrationData> getSet(PartyDb c) => c.TelecomDeviceRegistrations;
    }
}