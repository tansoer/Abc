using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Abc.Aids.Reflection;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Quantities;

namespace Abc.Domain.Currencies {

    public sealed class Currency : BaseMetric<CurrencyData> {
        private const double defaultRatioOfMinorUnit = 0.01;
        private const decimal hasNoRate = 0M;
        private static string currencyId => nameOf<CurrencyUsageData>(x => x.CurrencyId);
        public Currency() : this(null) { }
        public Currency(CurrencyData d) : base(d) { }
        public IReadOnlyList<CurrencyUsage> AcceptedIn =>
            list<ICurrencyUsagesRepo, CurrencyUsage>(currencyId, Id);
        public IReadOnlyList<Country> AcceptedInCountries =>
            AcceptedIn.Select(e => e.Country).ToList().AsReadOnly();
        public string NumericCode => Data?.NumericCode ?? Unspecified.String;
        public string MajorUnitSymbol => Data?.MajorUnitSymbol ?? Unspecified.String;
        public string MinorUnitSymbol => Data?.MinorUnitSymbol ?? Unspecified.String;
        public double RatioOfMinorUnit => Data?.RatioOfMinorUnit ?? defaultRatioOfMinorUnit;
        public bool IsIsoCurrency => Data?.IsIsoCurrency ?? false;
        public string FullName => $"{Name} ({Code}, {NumericCode})";
        public string Formula => 
            $"1 {fixDirection(MajorUnitSymbol)} = {RatioOfMinorUnit} {fixDirection(MinorUnitSymbol)}";
        internal static string fixDirection(string s) {
            var leftToRight = ((char)0x200E).ToString();
            var rightToLeft = ((char)0x200F).ToString();
            if (Regex.IsMatch(s,@"\p{IsArabic}")) return rightToLeft + s + leftToRight;
            return s;
        }
        public ExchangeRate CentralBankDayRate(DateTime date) {
            var r = Rates(date).FirstOrDefault(x => x.RateTypeId == RateType.OfficialRate);
            r ??= Rates(date).FirstOrDefault();
            r ??= new ExchangeRate(new ExchangeRateData { Rate = hasNoRate });
            return r;
        }
        public decimal FromBase(decimal amount, DateTime? date = null) {
            var r = Rate(date);
            if (r == 0) return hasNoRate;
            r = amount / r;
            return r;
        }
        public decimal Rate(DateTime? date = null) {
            var r = CentralBankDayRate(date ?? DateTime.Today);
            return r.Rate;
        }
        public IReadOnlyList<ExchangeRate> Rates(DateTime date) {
            var r = new GetFrom<IExchangeRatesRepo, ExchangeRate>().ListBy(GetMember.Name<ExchangeRateData>(x => x.CurrencyId),
                    Id, date.ToShortDateString());
            r ??= new List<ExchangeRate>().AsReadOnly();
            return r;
        }
        public decimal ToBase(decimal amount, DateTime? date = null) {
            var r = Rate(date);
            r = amount * r;
            return r;
        }

        public override string ToString() => FullName;
    }
}