using Abc.Data.Common;

namespace Abc.Data.Roles {
    public sealed class ResponsibilityTypeData: EntityTypeData {
        public string RequirementsRuleSetId { get; set; }
        public string ConditionsOfSatisfactionRuleSetId { get; set; }
    }
}
