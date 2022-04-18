using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Parties;
using Abc.Domain.Rules;
using System.Linq;
using System.Collections.Generic;

namespace Abc.Domain.Roles {
    public sealed class PartyRoleType :EntityType<IPartyRoleTypesRepo, PartyRoleType, PartyRoleTypeData> {
        public PartyRoleType() : this(null) { }
        public PartyRoleType(PartyRoleTypeData d) : base(d) { }
        public IReadOnlyList<PartyRoleConstraint> Constraints
            => list<IPartyRoleConstraintsRepo, PartyRoleConstraint>(roleTypeId, Id);
        public IRuleSet Requirements => item<IRuleSetsRepo, IRuleSet>(ruleSetId);
        public IReadOnlyList<Responsibility> MandatoryResponsibilities
            => responsibilities.Where(x => !x.IsOptional).ToList().AsReadOnly();
        public IReadOnlyList<Responsibility> OptionalResponsibilities
            => responsibilities.Where(x => x.IsOptional).ToList().AsReadOnly();
        public bool CanPlayRole(IParty p) => Constraints.Any(c => c.CanPlayRole(p));
        public bool IsCapableForRole(IParty p) => p?.IsCapable(Requirements) ?? false;
        public bool IsCapableForMandatoryResponsibilities(IParty p) => p?.IsCapable(MandatoryResponsibilities) ?? false;
        public bool IsCapableForOptionalResponsibilities(IParty p) => p?.IsCapable(OptionalResponsibilities) ?? false;
        public void AddMandatoryResponsibility(ResponsibilityType t) {
            if (HasResponsibility(t)) return;
            add<IResponsibilitiesRepo, Responsibility>(newResponsibility(t, false));
        }
        public static void DeleteResponsibility(Responsibility r) 
            => delete<IResponsibilitiesRepo, Responsibility>(r);
        public void AddOptionalResponsibility(ResponsibilityType t) {
            if (HasResponsibility(t)) return;
            add<IResponsibilitiesRepo, Responsibility>(newResponsibility(t, true));
        }
        public Responsibility FindResponsibility(ResponsibilityType t)
            => responsibilities.FirstOrDefault(x => x.typeId == t?.Id);
        public bool HasResponsibility(ResponsibilityType t) => !(FindResponsibility(t) is null);
        internal Responsibility newResponsibility(ResponsibilityType t, bool isOptional)
            => new(newResponsibilityData(t, isOptional));
        internal ResponsibilityData newResponsibilityData(ResponsibilityType t, bool isOptional)
            => new() {
                PartyRoleTypeId = Id,
                IsOptional = isOptional,
                ResponsibilityTypeId = get(t?.Id)
            };
        internal string ruleSetId => get(Data?.RuleSetId);
        internal static string roleTypeId => nameOf<IPartyRoleTypeAttributeData>(x => x.PartyRoleTypeId);
        internal IReadOnlyList<Responsibility> responsibilities
            => list<IResponsibilitiesRepo, Responsibility>(roleTypeId, Id);
    }
}