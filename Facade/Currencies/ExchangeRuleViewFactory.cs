using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Common;

namespace Abc.Facade.Currencies {
    public sealed class ExchangeRuleViewFactory :AbstractViewFactory<ExchangeRuleData, 
        ExchangeRule, ExchangeRuleView> {
        protected internal override ExchangeRule toObject(ExchangeRuleData d)
            => new(d);
    }
}
