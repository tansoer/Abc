using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;

namespace Abc.Domain.Roles {
    public sealed class AssignedResponsibility : Entity<AssignedResponsibilityData> {
        public AssignedResponsibility() : this(null) { }
        public AssignedResponsibility(AssignedResponsibilityData d) : base(d) { }
        public Responsibility Responsibility => item<IResponsibilitiesRepo, Responsibility>(responsibilityId);
        public PartySignature AssignedBy => item<IPartySignaturesRepo, PartySignature>(partySignatureId);
        internal string partyRoleId => get(Data?.PartyRoleId);
        internal string responsibilityId => get(Data?.ResponsibilityId);
        internal string partySignatureId => get(Data?.PartySignatureId);
    }
}
