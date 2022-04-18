using Abc.Data.Common;
using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {
    [TestClass] public class DeviceRegistrationDataTests : SealedTests<DeviceRegistrationData, EntityBaseData> {
        [TestMethod] public void PostalAddressIdTest() => isNullable<string>();
        [TestMethod] public void TelecomDeviceIdTest() => isNullable<string>();
    }
}