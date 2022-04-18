using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {

    public sealed class PersonEthnicitiesRepo : EntityRepo<PersonEthnicity, PersonEthnicityData>,
        IPersonEthnicitiesRepo {
        public PersonEthnicitiesRepo(PartyDb c = null) : base(c, c?.PersonEthnicities) { }
    }
}
