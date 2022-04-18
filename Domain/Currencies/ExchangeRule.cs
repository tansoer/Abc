using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Rules;

namespace Abc.Domain.Currencies {
    public sealed class ExchangeRule : Entity<ExchangeRuleData> {
        public ExchangeRule() : this(null) { }
        public ExchangeRule(ExchangeRuleData d) : base(d) { }
        public string RateTypeId => Data?.RateTypeId ?? Unspecified.String;
        public string RuleSetId => Data?.RuleSetId ?? Unspecified.String;
        public RateType RateType => new GetFrom<IRateTypesRepo, RateType>().ById(RateTypeId);
        public IRuleSet RuleSet => new GetFrom<IRuleSetsRepo, IRuleSet>().ById(RuleSetId);
    }
}