using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Currencies {
    public sealed class ExchangeRuleView :EntityBaseView{
        [DisplayName("Rate Type")]
        public string RateTypeId { get; set; }
        [DisplayName("Rule Set")]
        public string RuleSetId { get; set; }
    }
}
