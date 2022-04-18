using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Rules;

namespace Abc.Domain.Roles {
    public sealed class ResponsibilityType
        : EntityType<IResponsibilityTypesRepo, ResponsibilityType, ResponsibilityTypeData> {
        public ResponsibilityType() : this(null) { }
        public ResponsibilityType(ResponsibilityTypeData d) : base(d) { }

        //TODO Gunnar: Kas on vajalik?
        public override ResponsibilityType BaseType => base.BaseType;
        public IRuleSet Requirements => item<IRuleSetsRepo, IRuleSet>(requirementsRuleSetId);
        public IRuleSet ConditionsOfSatisfaction => item<IRuleSetsRepo, IRuleSet>(satisfactionRuleSetId);
        internal string requirementsRuleSetId => get(Data?.RequirementsRuleSetId); 
        internal string satisfactionRuleSetId => get(Data?.ConditionsOfSatisfactionRuleSetId);
    }
}
