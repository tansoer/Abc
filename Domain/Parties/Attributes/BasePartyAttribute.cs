using Abc.Data.Parties;
using Abc.Domain.Common;

namespace Abc.Domain.Parties.Attributes {

    public abstract class BasePartyAttribute<T> : Entity<T> where T : PartyAttributeData, new() {

        protected internal BasePartyAttribute() : this(null) { }
        protected internal BasePartyAttribute(T d) : base(d) { }

        public string PartyId => get(Data?.PartyId);
        public virtual IParty Party => item<IPartiesRepo, IParty>(PartyId);
    }
}
