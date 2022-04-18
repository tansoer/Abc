using Abc.Data.Common;

namespace Abc.Data.Currencies {
    public sealed class ExchangeRuleData :EntityBaseData {
        public string RateTypeId { get; set; }
        public string RuleSetId { get; set; }

    }
}
