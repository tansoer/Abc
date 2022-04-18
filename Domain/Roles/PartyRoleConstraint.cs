using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Parties;

namespace Abc.Domain.Roles {
    public sealed class PartyRoleConstraint: Entity<PartyRoleConstraintData> {
        public PartyRoleConstraint() : this(null) { }
        public PartyRoleConstraint(PartyRoleConstraintData d) : base(d) { }
        public PartyRoleConstraintType Type
            => item<IPartyRoleConstraintTypesRepo, PartyRoleConstraintType>(typeId);
        internal string partyRoleTypeId => get(Data?.PartyRoleTypeId);
        internal string typeId => get(Data?.RoleConstraintTypeId);
        public bool CanPlayRole(IParty p) => Type?.CanPlayRole(p) ?? false;
    }
}
