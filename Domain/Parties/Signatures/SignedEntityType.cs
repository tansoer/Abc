using Abc.Data.Parties;
using Abc.Domain.Common;

namespace Abc.Domain.Parties.Signatures {

    public sealed class SignedEntityType : Entity<SignedEntityTypeData> {
        public SignedEntityType() : this(null) { }
        public SignedEntityType(SignedEntityTypeData d) : base(d) { }
    }
}