using Abc.Aids.Enums;
using Abc.Data.Parties;

namespace Abc.Domain.Parties.Contacts {

    public static class PartyContactFactory {

        public static IPartyContact Create(PartyContactData d) =>
            d?.ContactType switch
            {
                ContactType.Email => new EmailAddress(d),
                ContactType.Web => new WebPageAddress(d),
                ContactType.Telecom => new TelecomAddress(d),
                ContactType.Postal => new GeographicAddress(d),
                _ => new GeographicAddress(d)
            };

        public static PartyContactData Create(IPartyContact obj) => obj.Data;
    }
}