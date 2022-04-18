using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Infra.Common;

namespace Abc.Infra.Currencies {
    public sealed class ExchangeRulesRepo : EntityRepo<ExchangeRule, ExchangeRuleData>,
        IExchangeRulesRepo {
        public ExchangeRulesRepo(MoneyDb c = null) : base(c, c?.RateRules) { }
    }
}

