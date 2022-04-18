using Abc.Aids.Enums;
using Abc.Data.Parties;
using Abc.Domain.Common;

namespace Abc.Domain.Parties.Names {
    public interface IPartyName : IPartyName<PartyNameData> { }

    public interface IPartyName<TData> : IEntity<TData> {
        NameType NameType { get; }
        public string PartyId { get; }
    }
}
