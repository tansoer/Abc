using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Reflection;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Rules;

namespace Abc.Domain.Currencies {

    public sealed class RateType : Entity<RateTypeData> {
        public static string OfficialRate => "OfficialRate";
        public bool IsOfficial => Id == OfficialRate;
        public RateType() : this(null) { }
        public RateType(RateTypeData d) : base(d) { }
        public IReadOnlyList<ExchangeRule> ExchangeRateRules =>
            new GetFrom<IExchangeRulesRepo, ExchangeRule>()
                .ListBy(GetMember.Name<ExchangeRuleData>(x => x.RateTypeId), Id);
        public IReadOnlyList<IRuleSet> ApplicabilityRules =>
            ExchangeRateRules.Select(e => e.RuleSet).ToList().AsReadOnly();
        public bool IsApplicable(RuleContext c, params RuleOverride[] overrides) {
            var sets = ApplicabilityRules;
            return sets.Count == 0 || sets.Any(s => (s.Evaluate(c, overrides) as BooleanVariable)?.Value ?? false);
        }
    }
}