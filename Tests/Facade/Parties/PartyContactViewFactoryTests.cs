using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Facade.Parties {
    [TestClass] public class PartyContactViewFactoryTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(PartyContactViewFactory);

        [TestMethod]
        public void CreateTest() {
            foreach (var t in (ContactType[])Enum.GetValues(typeof(ContactType))) {
                var view = GetRandom.ObjectOf<PartyContactView>();
                view.ContactType = t;
                var o = new PartyContactViewFactory().Create(view);
                view = new PartyContactViewFactory().Create(o);
                allPropertiesAreEqual(o.Data, view);
            }
        }
        //[TestInitialize] public void TestInitialize() => type = typeof(PartyContactViewFactory);
        //[TestMethod] public void CreateTest() { }
        //[DataRow(ContactType.Unspecified)]
        //[DataRow(ContactType.Email)]
        //[DataRow(ContactType.Web)]
        //[DataRow(ContactType.Telecom)]
        //[DataRow(ContactType.Postal)]
        //[DataTestMethod]
        //public void CreateObjectTest(ContactType t) {
        //    var v = createView(t);
        //    var o = PartyContactViewFactory.Create(v);
        //    v = PartyContactViewFactory.Create(o);
        //    o = PartyContactViewFactory.Create(v);
        //    isTrue(validate(o, v));
        //}
        //private static PartyContactView createView(ContactType t) 
        //    => t switch {
        //        ContactType.Email => emailView(),
        //        ContactType.Web => webView(),
        //        ContactType.Postal => postalView(),
        //        ContactType.Telecom => telecomView(),
        //        _ => unspecifiedView(),
        //    };
        //private static PartyContactView unspecifiedView() {
        //    var v = unspecifiedView(new PostalAddressView(), ContactType.Unspecified) as PostalAddressView;
        //    v.Name = random<string>();
        //    return v;
        //}
        //private static TelecomAddressView telecomView() {
        //    var v = unspecifiedView(new TelecomAddressView(), ContactType.Telecom) as TelecomAddressView;
        //    v.NationalDirectDialingPrefix = random<uint>().ToString();
        //    v.Name = random<uint>().ToString();
        //    v.Device = random<TelecomDeviceType>();
        //    v.AreaCode = random<uint>().ToString();
        //    v.CountryCode = random<uint>().ToString();
        //    v.Code = random<uint>().ToString();
        //    return v;
        //}
        //private static PartyContactView postalView() {
        //    var v = unspecifiedView(new PostalAddressView(),ContactType.Postal) as PostalAddressView;
        //    v.Name = random<string>();
        //    v.CountryId = random<string>();
        //    v.City = random<string>();
        //    v.RegionOrState = random<string>();
        //    v.Code = random<string>();
        //    return v;
        //}
        //private static PartyContactView webView() {
        //    var v = unspecifiedView(new WebPageAddressView(), ContactType.Web) as WebPageAddressView;
        //    v.Name = random<string>();
        //    return v;
        //}
        //private static PartyContactView emailView() {
        //    var v = unspecifiedView(new EmailAddressView(), ContactType.Email) as EmailAddressView;
        //    v.Name = random<string>();
        //    return v;
        //}

        //private static PartyContactView unspecifiedView(PartyContactView v, ContactType t) {
        //    v.ValidFrom = GetRandom.DateTime(DateTime.Now.AddYears(-10), DateTime.Now.AddYears(10));
        //    v.ContactType = t;
        //    v.Details = random<string>();
        //    v.ValidTo = GetRandom.DateTime(v.ValidFrom, DateTime.Now.AddYears(10));
        //    return v;
        //}

        //private static bool validate(IPartyContact o, PartyContactView v) {
        //    allPropertiesAreEqual(o.Data, v
        //        , nameof(o.Data.CityOrAreaCode)
        //        , nameof(o.Data.RegionOrStateOrCountryCode)
        //        , nameof(o.Data.CountryId)
        //        , nameof(o.Data.NationalDirectDialingPrefix)
        //        , nameof(o.Data.Device));

        //    return o switch {
        //        GeographicAddress x => validatePostal(x, v as PostalAddressView),
        //        WebPageAddress x => validateWeb(x, v as WebPageAddressView),
        //        TelecomAddress x => validateTelco(x, v as TelecomAddressView),
        //        EmailAddress x => validateEmail(x, v as EmailAddressView),
        //        _ => false
        //    };
        //}
        //private static bool validateWeb(WebPageAddress o, WebPageAddressView v) {
        //    areEqual(o.Data.Name, v.Name);
        //    isNull(v.Code);
        //    isNotNull(v.Name);
        //    return true;
        //}
        //private static bool validateEmail(EmailAddress o, EmailAddressView v) {
        //    areEqual(o.Data.Name, v.Name);
        //    isNull(v.Code);
        //    isNotNull(v.Name);
        //    return true;
        //}
        //private static bool validatePostal(GeographicAddress o, PostalAddressView v) {
        //    if (o.Data.ContactType == ContactType.Unspecified) return validateUnspecified(o, v); 
        //    areEqual(o.Data.CityOrAreaCode, v.City);
        //    areEqual(o.Data.RegionOrStateOrCountryCode, v.RegionOrState);
        //    isNotNull(v.Code);
        //    isNotNull(v.Name);
        //    areEqual(o.Data.Name, v.Name);
        //    areEqual(o.Data.Code, v.Code);
        //    return true;
        //}
        //private static bool validateUnspecified(GeographicAddress o, PostalAddressView v) {
        //    areEqual(o.Data.Name, v.Name);
        //    isNull(v.Code);
        //    isNotNull(v.Name);
        //    return true;
        //}
        //private static bool validateTelco(TelecomAddress o, TelecomAddressView v) {
        //    areEqual(o.Data.Name, v.Name);
        //    areEqual(o.Data.Code, v.Code);
        //    areEqual(o.Data.CityOrAreaCode, v.AreaCode);
        //    areEqual(o.Data.RegionOrStateOrCountryCode, v. CountryCode);
        //    areEqual(o.Data.Device, v.Device);
        //    isNotNull(v.Code);
        //    isNotNull(v.Name);
        //    return true;
        //}
        //[DataRow(ContactType.Unspecified)]
        //[DataRow(ContactType.Email)]
        //[DataRow(ContactType.Web)]
        //[DataRow(ContactType.Telecom)]
        //[DataRow(ContactType.Postal)]
        //[DataTestMethod]
        //public void CreateViewTest(ContactType t) {
        //    var d = createData(t);
        //    var o = PartyContactFactory.Create(d);
        //    var v = PartyContactViewFactory.Create(o);
        //    o = PartyContactViewFactory.Create(v);
        //    v = PartyContactViewFactory.Create(o);
        //    isTrue(validate(o, v));
        //}
        //private static PartyContactData createData(ContactType t)
        //    => t switch {
        //        ContactType.Email => unspecifiedData(t),
        //        ContactType.Web => unspecifiedData(t),
        //        ContactType.Postal => postalData(),
        //        ContactType.Telecom => telecomData(),
        //        _ => unspecifiedData(),
        //    };

        //private static PartyContactData telecomData() {
        //    var v = unspecifiedData(ContactType.Telecom);
        //    v.NationalDirectDialingPrefix = random<uint>().ToString();
        //    v.Name = random<uint>().ToString();
        //    v.Device = random<TelecomDeviceType>();
        //    v.CityOrAreaCode = random<uint>().ToString();
        //    v.RegionOrStateOrCountryCode = random<uint>().ToString();
        //    v.Code = random<uint>().ToString();
        //    return v;
        //}
        //private static PartyContactData postalData() {
        //    var v = unspecifiedData(ContactType.Postal);
        //    v.CountryId = random<string>();
        //    v.CityOrAreaCode = random<string>();
        //    v.RegionOrStateOrCountryCode = random<string>();
        //    v.Code = random<string>();
        //    return v;
        //}
        //private static PartyContactData unspecifiedData(ContactType t = ContactType.Unspecified) {
        //    var v = new PartyContactData {
        //        ValidFrom = GetRandom.DateTime(DateTime.Now.AddYears(-10), DateTime.Now.AddYears(10)),
        //        ContactType = t,
        //        Details = random<string>(),
        //        Name = random<string>()
        //    };
        //    v.ValidTo = GetRandom.DateTime(v.ValidFrom, DateTime.Now.AddYears(10));
        //    return v;
        //}
    }
}
