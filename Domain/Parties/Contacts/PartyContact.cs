using Abc.Data.Common;
using Abc.Data.Parties;
using Abc.Domain.Common;

namespace Abc.Domain.Parties.Contacts {
    public abstract class PartyContact<TData> : Entity<TData>, IPartyContact<TData>
        where TData : EntityBaseData, IPartyContactData, new() {
        protected PartyContact() : this(null) { }
        protected PartyContact(TData d) : base(d) { }
    }
}
