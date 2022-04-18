using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Infra.Common;

namespace Abc.Infra.Currencies {

    public sealed class CurrencyUsagesRepo : EntityRepo<CurrencyUsage, CurrencyUsageData>,
        ICurrencyUsagesRepo {

        public CurrencyUsagesRepo(MoneyDb c = null) : base(c, c?.CurrencyUsages) { }
    }
}
