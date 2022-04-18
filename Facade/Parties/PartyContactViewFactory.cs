using Abc.Aids.Methods;
using Abc.Data.Parties;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Common;

namespace Abc.Facade.Parties {
    public sealed class PartyContactViewFactory : AbstractViewFactory<PartyContactData, IPartyContact, PartyContactView> {
        protected internal override IPartyContact toObject(PartyContactData d) => PartyContactFactory.Create(d);
        //public static PartyContactView Create(IPartyContact o) 
        //    => o switch {
        //        GeographicAddress x => Create(x),
        //        WebPageAddress x => Create(x),
        //        TelecomAddress x => Create(x),
        //        EmailAddress x => Create(x),
        //        _ => null
        //    };
        //public static PartyContactView Create(GeographicAddress o) {
        //    var v = new PostalAddressView();
        //    Copy.Members(o.Data, v);
        //    v.CountryId = o.Data.CountryId;
        //    v.RegionOrState = o.Data.RegionOrStateOrCountryCode;
        //    v.City = o.Data.CityOrAreaCode;
        //    return v;
        //}
        //public static PartyContactView Create(WebPageAddress o) {
        //    var v = new WebPageAddressView();
        //    Copy.Members(o.Data, v);
        //    return v;
        //}
        //public static PartyContactView Create(EmailAddress o) {
        //    var v = new EmailAddressView();
        //    Copy.Members(o.Data, v);
        //    return v;
        //}
        //public static PartyContactView Create(TelecomAddress o) {
        //    var v = new TelecomAddressView();
        //    Copy.Members(o.Data, v);
        //    v.CountryCode = o.Data.RegionOrStateOrCountryCode;
        //    v.AreaCode  = o.Data.CityOrAreaCode;
        //    return v;
        //}

        //public static IPartyContact Create(PartyContactView v) {
        //    var d = new PartyContactData();
        //    Copy.Members(v, d);
        //    return v switch {
        //        EmailAddressView x => emailAddress(x, d),
        //        WebPageAddressView x => webAddress(x, d),
        //        PostalAddressView x => postalAddress(x, d),
        //        TelecomAddressView x => telecomAddress(x, d),
        //        _ => unspecifiedAddress(v as PostalAddressView, d),
        //    };
        //}

        //private static IPartyContact unspecifiedAddress(PostalAddressView v, PartyContactData d) {
        //    d.ContactType = ContactType.Unspecified;
        //    d.Name = v?.Name;
        //    return PartyContactFactory.Create(d);
        //}

        //private static IPartyContact telecomAddress(TelecomAddressView v, PartyContactData d) {
        //    d.ContactType = ContactType.Telecom;
        //    d.RegionOrStateOrCountryCode = v.CountryCode;
        //    d.CityOrAreaCode = v.AreaCode;
        //    d.Name = v.Name;
        //    d.Code = v.Code;
        //    return PartyContactFactory.Create(d);
        //}

        //private static IPartyContact postalAddress(PostalAddressView v, PartyContactData d) {
        //    if (d.ContactType == ContactType.Unspecified) return unspecifiedAddress(v, d);
        //    d.ContactType = ContactType.Postal;
        //    d.RegionOrStateOrCountryCode = v.RegionOrState;
        //    d.CityOrAreaCode = v.City;
        //    return PartyContactFactory.Create(d);
        //}

        //private static IPartyContact webAddress(WebPageAddressView v, PartyContactData d) {
        //    d.ContactType = ContactType.Web;
        //    d.Name = v.Name;
        //    return PartyContactFactory.Create(d);
        //}

        //private static IPartyContact emailAddress(EmailAddressView v, PartyContactData d) {
        //    d.ContactType = ContactType.Email;
        //    d.Name = v.Name;
        //    return PartyContactFactory.Create(d);
        //}
    }
}