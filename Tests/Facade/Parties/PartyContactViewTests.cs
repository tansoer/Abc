using Abc.Aids.Enums;
using Abc.Data.Common;
using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass]
    public class PartyContactViewTests :SealedTests<PartyContactView, PartyAttributeView> {
        protected override PartyContactView createObject() => random<PartyContactView>();
        [TestMethod] public void CountryIdTest() => isNullableProperty<string>("Country");
        [TestMethod] public void ContactUsageIdTest() => isNullableProperty<string>("Contact usage");
        [TestMethod] public void ContactTest() => isReadOnly(obj.ToString());
        [TestMethod] public void DetailsTest() => isNullableProperty<string>("Comments");
        [TestMethod] public void NationalDirectDialingPrefixTest() => isNullableProperty<string>("National direct dialing prefix");
        [TestMethod] public void DeviceTest() => isProperty<TelecomDeviceType>("Device");
        [TestMethod] public void NameTest() => isNullableProperty<string>(PartyContactView.AddressLineCaption, true);
        [TestMethod] public void CityOrAreaCodeTest() => isNullableProperty<string>(PartyContactView.CityCaption);
        [TestMethod] public void RegionOrStateOrCountryCodeTest() => isNullableProperty<string>(PartyContactView.RegionOrStateCaption);
        [TestMethod] public void CodeTest() => isNullableProperty<string>(PartyContactView.ZipOrPostCodeCaption);
        [TestMethod] public void ContactTypeTest() => isProperty<ContactType>("Contact type");
        [TestMethod] public void ToStringTest() => areEqual(new PartyContactViewFactory().Create(obj).ToString(), obj.ToString());
        [TestMethod] public void EmailAddressCaptionTest() => areEqual(PartyContactView.EmailAddressCaption, "Email address");
        [TestMethod] public void WebAddressCaptionTest() => areEqual(PartyContactView.WebAddressCaption, "Web address");
        [TestMethod] public void AddressLineCaptionTest() => areEqual(PartyContactView.AddressLineCaption, "Address line");
        [TestMethod] public void PhoneNumberCaptionTest() => areEqual(PartyContactView.PhoneNumberCaption, "Phone number");
        [TestMethod] public void CityCaptionTest() => areEqual(PartyContactView.CityCaption, "City");
        [TestMethod] public void AreaCodeCaptionTest() => areEqual(PartyContactView.AreaCodeCaption, "Area code");
        [TestMethod] public void RegionOrStateCaptionTest() => areEqual(PartyContactView.RegionOrStateCaption, "Region or state");
        [TestMethod] public void CountryCodeCaptionTest() => areEqual(PartyContactView.CountryCodeCaption, "Country code");
        [TestMethod] public void ZipOrPostCodeCaptionTest() => areEqual(PartyContactView.ZipOrPostCodeCaption, "Zip or post code");
        [TestMethod] public void ExtensionCaptionTest() => areEqual(PartyContactView.ExtensionCaption, "Extension");
    }
}