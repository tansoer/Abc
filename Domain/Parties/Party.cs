using System.Collections.Generic;
using System.Linq;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Capabilities;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Parties.Identifiers;
using Abc.Domain.Parties.Names;
using Abc.Domain.Parties.Preferences;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Roles;
using Abc.Domain.Rules;

namespace Abc.Domain.Parties {

    public abstract class Party<TName> : Entity<PartyData>, IParty<TName>
        where TName : IPartyName, new() {
        protected Party() : this(null) { }
        protected Party(PartyData d) : base(d) { }

        public string GetName() => OfficialName?.ToString();
        public string GetPhone() => getContact(Phones);
        public string GetAddress() => getContact(Addresses);
        public string GetEmail() => getContact(Emails);
        public TName OfficialName => getOfficialName();
        public IReadOnlyList<TName> Names => names();
        public IReadOnlyList<PartyRole> Roles => list<IPartyRolesRepo, PartyRole>(partyId, Id);
        public IReadOnlyList<PartyContactUsage> Contacts => list<IPartyContactUsagesRepo, PartyContactUsage>(partyId, Id);
        public IReadOnlyList<GeographicAddress> Addresses => getContacts<GeographicAddress>();
        public IReadOnlyList<EmailAddress> Emails => getContacts<EmailAddress>();
        public IReadOnlyList<TelecomAddress> Phones => getContacts<TelecomAddress>();
        public IReadOnlyList<RegisteredIdentifier> Identifiers => list<IRegisteredIdentifiersRepo, RegisteredIdentifier>(partyId, Id);
        public IReadOnlyList<PartyCapability> Capabilities => list<IPartyCapabilitiesRepo, PartyCapability>(partyId, Id);
        public IReadOnlyList<Preference> Preferences => list<IPreferencesRepo, Preference>(partyId, Id);
        public IReadOnlyList<Authentication> Authentications => list<IAuthenticationsRepo, Authentication>(partyId, Id);

        public PartySummary ToPartySummary() =>
            new(new PartySummaryData {
                Name = GetName(),
                Address = GetAddress(),
                EmailAddress = GetEmail(),
                PhoneNumber = GetPhone(),
                PartyId = Id,
                Details = Details
            });

        public bool IsCapable(IRuleSet requirements) 
            => Capabilities?.Any(x => x.IsCapable(requirements))?? false;
        public bool IsCapable(IReadOnlyList<Responsibility> responsibilities)
            => responsibilities?.Any(x => x.IsCapable(Capabilities))??false;

        internal static string partyId => nameOf<PartyAttributeData>(x => x.PartyId);
        internal IReadOnlyList<IPartyName> partyNames => list<IPartyNamesRepo, PartyName>(partyId, Id);
        internal IReadOnlyList<TName> names() {
            var l = new List<TName>();
            foreach (var e in partyNames) if (e is TName name) l.Add(name);
            return l;
        }
        internal TName getOfficialName() {
            var n = Names.FirstOrDefault(x => x.NameType == Aids.Enums.NameType.Official);
            return (!(n is null)) ? n : getFirstName();
        }
        internal TName getFirstName() => (Names.Count != 0) ? Names[0] : new TName();
        internal IReadOnlyList<T> getContacts<T>() where T : IPartyContact {
            //TODO we have to think about some relevant order for contact usage types 
            //so that most relevant get the first place
            var l = new List<T>(); 
            foreach (var c in Contacts) {
                if (c.Contact is not T) continue;
                l.Add((T)c.Contact);
            }
            return l.AsReadOnly();
        }
        internal static string getContact<T>(IReadOnlyList<T> l) where T : IPartyContact
            => (l?.Count > 0) ? l[0].ToString() : Unspecified.String;
    }
}
