using Abc.Data.Parties;
using Abc.Domain.Parties.Signatures;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {
    public sealed class SignedEntityTypesRepo : EntityRepo<SignedEntityType, SignedEntityTypeData>,
        ISignedEntityTypesRepo {
        public SignedEntityTypesRepo(PartyDb c = null) : base(c, c?.SignedEntityTypes) { }
    }
}

