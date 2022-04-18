using Abc.Data.Parties;

namespace Abc.Domain.Parties.Contacts {

    public sealed class EmailAddress : PartyContact<PartyContactData>, IPartyContact {
        public EmailAddress() : this(null) { }
        public EmailAddress(PartyContactData d) : base(d) { }
        public string Email => get(Data?.Name);
        public override string ToString() => Email;
    }
}