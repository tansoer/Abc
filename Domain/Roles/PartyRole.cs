using System.Collections.Generic;
using Abc.Data.Roles;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Preferences;
using Abc.Domain.Parties.Signatures;

namespace Abc.Domain.Roles {

    public sealed class PartyRole :BasePartyAttribute<PartyRoleData> {
        public PartyRole() : this(null) { }
        public PartyRole(PartyRoleData d) : base(d) { }
        public IReadOnlyList<Preference> Preferences => list<IPreferencesRepo, Preference>(roleId, Id);
        public IReadOnlyList<AssignedResponsibility> Responsibilities 
            => list<IAssignedResponsibilitiesRepo, AssignedResponsibility>(roleId, Id);
        public void Assign(Responsibility r, PartySignature s)
            => add<IAssignedResponsibilitiesRepo, AssignedResponsibility>(newAssignedResponsibility(r, s));
        internal AssignedResponsibility newAssignedResponsibility(Responsibility r, PartySignature s)
            => new(newAssignedResponsibilityData(r, s));
        private AssignedResponsibilityData newAssignedResponsibilityData(Responsibility r, PartySignature s)
            => new() {
                PartyRoleId = Id,
                PartySignatureId = get(s?.Id),
                ResponsibilityId = get(r?.Id)
            };
        public static void Remove(AssignedResponsibility r) =>
            delete<IAssignedResponsibilitiesRepo, AssignedResponsibility>(r);
        public PartyRoleType Type => item<IPartyRoleTypesRepo, PartyRoleType>(typeId);
        internal string typeId => get(Data?.PartyRoleTypeId);
        internal static string roleId => nameOf<IPartyRoleAttributeData>(x => x.PartyRoleId);
    }
}