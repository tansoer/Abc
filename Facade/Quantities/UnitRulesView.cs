using System.ComponentModel;

namespace Abc.Facade.Quantities {
    public sealed class UnitRulesView :UnitAttributeView {
        [DisplayName("From base units rule")] public string FromBaseUnitRuleId { get; set; }
        [DisplayName("To base units rule")] public string ToBaseUnitRuleId { get; set; }
        public string ToFormula { get; set; }
        public string FromFormula { get; set; }
    }
}
