using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Infra.Common;

namespace Abc.Infra.Currencies {

    public sealed class CurrencyRepo : EntityRepo<Currency, CurrencyData>, ICurrencyRepo {
        public CurrencyRepo(MoneyDb c = null) : base(c, c?.Currencies) { }
    }
}