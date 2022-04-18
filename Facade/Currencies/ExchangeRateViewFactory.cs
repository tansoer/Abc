using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Common;

namespace Abc.Facade.Currencies {

    public sealed class ExchangeRateViewFactory :
        AbstractViewFactory<ExchangeRateData, ExchangeRate, ExchangeRateView> {
        protected internal override ExchangeRate toObject(ExchangeRateData d) =>
            new ExchangeRate(d);
    }

}
