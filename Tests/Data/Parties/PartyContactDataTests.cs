using Abc.Aids.Enums;
using Abc.Data.Common;
using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {

    [TestClass]
    public class PartyContactDataTests : SealedTests<PartyContactData, EntityBaseData> {
        [TestMethod] public void CityOrAreaCodeTest() => isNullable<string>();
        [TestMethod] public void RegionOrStateOrCountryCodeTest() => isNullable<string>();
        [TestMethod] public void CountryIdTest() => isNullable<string>();
        [TestMethod] public void NationalDirectDialingPrefixTest() => isNullable<string>();
        [TestMethod] public void DeviceTest() => isProperty<TelecomDeviceType>();
        [TestMethod] public void ContactTypeTest() => isProperty<ContactType>();
    }
}