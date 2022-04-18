using Abc.Data.Parties;
using Abc.Domain.Common;

namespace Abc.Domain.Parties.Contacts {

    public interface IPartyContact : IPartyContact<PartyContactData> { }
    public interface IPartyContact<TData> : IEntity<TData> {
        string ToString();
    }
}
