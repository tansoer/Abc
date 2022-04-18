using Abc.Aids.Enums;
using Abc.Data.Parties;
using Abc.Domain.Parties;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {

    public sealed class PartiesRepo :PagedRepo<IParty, PartyData>, IPartiesRepo {
        public PartiesRepo(PartyDb c = null) : base(c, c?.Parties) { }
        protected internal override IParty toDomainObject(PartyData d) {
            d ??= new PartyData();
            if (d.PartyType == PartyType.Organization) return new Organization(d);
            return new Person(d);
        }
    }
}


