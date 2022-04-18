using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Infra.Common;

namespace Abc.Infra.Currencies {

    public sealed class ExchangeRatesRepo : EntityRepo<ExchangeRate, ExchangeRateData>,
        IExchangeRatesRepo {

        public ExchangeRatesRepo(MoneyDb c = null) : base(c, c?.Rates) { }

    }
}