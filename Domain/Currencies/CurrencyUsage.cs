using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Parties.Contacts;

namespace Abc.Domain.Currencies {
    public sealed class CurrencyUsage : DetailedEntity<CurrencyUsageData> {
        public CurrencyUsage() : this(null) { }
        public CurrencyUsage(CurrencyUsageData d) : base(d) { }
        public string CountryId => get(Data?.CountryId);
        public string CurrencyId => get(Data?.CurrencyId);
        public Country Country => item<ICountriesRepo, Country>(CountryId);
        public Currency Currency => item<ICurrencyRepo, Currency>(CurrencyId);
        public string CurrencyNativeName => get(Data?.CurrencyNativeName);
    }
}
