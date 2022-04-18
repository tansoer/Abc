using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Infra.Common;

namespace Abc.Infra.Currencies {

    public sealed class ExchangeTypesRepo : EntityRepo<RateType, RateTypeData>,
        IRateTypesRepo {
        public ExchangeTypesRepo(MoneyDb c = null) : base(c, c?.RateTypes) { }
    }
}