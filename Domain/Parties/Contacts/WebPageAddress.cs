using Abc.Data.Parties;

namespace Abc.Domain.Parties.Contacts {
    public sealed class WebPageAddress : PartyContact<PartyContactData>, IPartyContact {
        public WebPageAddress() : this(null) { }
        public WebPageAddress(PartyContactData d) : base(d) { }
        public string Url => get(Data?.Name);
        public override string ToString() => Url;
    }
}
