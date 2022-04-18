using Abc.Data.Common;

namespace Abc.Data.Currencies {

    public sealed class CurrencyUsageData : DetailedData {
        public string CountryId { get; set; }
        public string CurrencyId { get; set; }
        public string CurrencyNativeName { get; set; }
    }
}