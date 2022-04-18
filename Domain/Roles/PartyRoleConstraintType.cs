using Abc.Aids.Enums;
using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Parties;

namespace Abc.Domain.Roles {
    public sealed class PartyRoleConstraintType :Entity<PartyRoleConstraintTypeData> {
        public PartyRoleConstraintType() : this(null) { }
        public PartyRoleConstraintType(PartyRoleConstraintTypeData d) : base(d) { }
        public PartyType PartyType => Data?.PartyType ?? PartyType.Unspecified;
        internal string organizationTypeId => get(Data?.OrganizationTypeId);
        public bool CanPlayRole(IParty p) {
            if (p is Organization) return canPlayRole(p as Organization);
            return canPlayRole(p as Person);
        }
        private bool canPlayRole(Person p) {
            if (p is null) return false;
            return PartyType == PartyType.Person;
        }
        private bool canPlayRole(Organization o) {
            if (o is null) return false;
            if  (PartyType != PartyType.Organization) return false;
            return o.typeId == organizationTypeId;
        }
    }
}
