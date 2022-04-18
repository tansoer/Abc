using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abc.Aids.Enums;
using Abc.Data.Common;
using Abc.Facade.Common;

namespace Abc.Facade.Parties {

    public sealed class PartyContactView :PartyAttributeView {
        public static string EmailAddressCaption => "Email address";
        public static string WebAddressCaption => "Web address";
        public static string AddressLineCaption => "Address line";
        public static string PhoneNumberCaption => "Phone number";
        public static string CityCaption => "City";
        public static string AreaCodeCaption => "Area code";
        public static string RegionOrStateCaption => "Region or state";
        public static string CountryCodeCaption => "Country code";
        public static string ZipOrPostCodeCaption => "Zip or post code";
        public static string ExtensionCaption => "Extension";
        [DisplayName("Contact usage")] public string ContactUsageId { get; set; }
        [DisplayName("Contact type")] public ContactType ContactType { get; set; }
        [DisplayName("Comments")] public new string Details { get; set; }
        [DisplayName("Contact")] public string Contact => ToString();
        [DisplayName("Country")] public string CountryId { get; set; }
        [DisplayName("National direct dialing prefix")] public string NationalDirectDialingPrefix { get; set; }
        [DisplayName("Device")] public TelecomDeviceType Device { get; set; }
        [Required] [DisplayName("Address line")] public new string Name { get; set; }
        [DisplayName("City")] public string CityOrAreaCode { get; set; }
        [DisplayName("Region or state")] public string RegionOrStateOrCountryCode { get; set; }
        [DisplayName("Zip or post code")] public new string Code { get; set; }

        public override string ToString() => new PartyContactViewFactory().Create(this).ToString();

    }
}

