using Abc.Data.Common;
using System;

namespace Abc.Data.Currencies {

    public sealed class ExchangeRateData : DetailedData {

        public ExchangeRateData() { }
        public ExchangeRateData(string currency, in decimal rate, string exchangeRateTypeId, in DateTime validFrom) {
            CurrencyId = currency;
            RateTypeId = exchangeRateTypeId;
            ValidFrom = validFrom;
            ValidTo = validFrom.AddDays(1).AddSeconds(-1);
            Rate = rate;
            Id = composeId(currency, exchangeRateTypeId, validFrom);
        }
        private static string composeId(string currency, string exchangeRateTypeId, in DateTime validFrom)
            => $"{validFrom:yyyy-MM-dd}-{currency}-{exchangeRateTypeId}";

        public string CurrencyId { get; set; }

        public string RateTypeId { get; set; }

        public decimal Rate { get; set; }

    }
}