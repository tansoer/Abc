using Abc.Data.Currencies;
using Abc.Domain.Common;

namespace Abc.Domain.Currencies {

    public sealed class ExchangeRate : DetailedEntity<ExchangeRateData> {
        public ExchangeRate() : this(null) { }
        public ExchangeRate(ExchangeRateData d) : base(d) { }
        public string CurrencyId => Data?.CurrencyId ?? Unspecified.String;
        public string RateTypeId => Data?.RateTypeId ?? Unspecified.String;
        public Currency Currency => new GetFrom<ICurrencyRepo, Currency>().ById(CurrencyId);
        public RateType RateType => new GetFrom<IRateTypesRepo, RateType>().ById(RateTypeId);
        public bool IsOfficial => RateType.IsOfficial;
        public decimal Rate => Data?.Rate ?? 0;
    }
}