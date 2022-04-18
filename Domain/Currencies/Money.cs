using System;
using Abc.Aids.Constants;
using Abc.Aids.Extensions;
using Abc.Aids.Methods;
using Abc.Core.Rounding;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Quantities;

namespace Abc.Domain.Currencies {
    public sealed class Money :MeasurableValue<Money, decimal, Currency> {
        public Money() :this(0, (Currency)null) { }
        public Money(decimal amount, Currency c, DateTime? date = null) :base(amount, c, date) { }
        public Money(decimal amount, string currencyId, DateTime? date = null) :
            base(amount, new GetFrom<ICurrencyRepo, Currency>().ById(currencyId), date) {
        }
        public Currency Currency {
            get {
                var d = new CurrencyData {Id = Word.Unspecified};
                if (!(measure is null)) Copy.Members(measure.Data, d);
                return new Currency(d);
            }
        }
        public override Money Divide(decimal a)
            => Safe.Run(() => new Money(Amount / a, Currency, ValidFrom), Unspecified);
        public override Money Round(IRoundingPolicy roundingPolicy) {
            var a = roundingPolicy.Round(Amount);
            return new Money(a, Currency, ValidFrom);
        }
        public override bool IsEqual(Money m)
            => CompareTo(m) == 0;
        public override bool IsLess(Money m) => CompareTo(m) < 0;
        public override bool IsGreater(Money m) => CompareTo(m) > 0;
        public override int CompareTo(Money m) {
            var d1 = round(Currency.ToBase(Amount, ValidFrom));
            var d2 = round(m.Currency.ToBase(m.Amount, ValidFrom));
            var r = d1.CompareTo(d2);
            return r;
        }
        internal static decimal round(decimal d)
            => new RoundingPolicy(RoundingStrategy.Round, 10).Round(d);
        public override Money ConvertTo(Currency c)
            => Currency.Id == c.Id
                ? this
                : new Money(c.FromBase(Currency.ToBase(Amount, ValidFrom), ValidFrom), c, ValidFrom);
        public override Money Add(Money m) {
            var d1 = Currency.ToBase(Amount, ValidFrom);
            var d2 = m.Currency.ToBase(m.Amount, ValidFrom);
            var amount = m.Currency.FromBase(d1 + d2, ValidFrom);
            return new Money(amount, m.Currency, ValidFrom);
        }
        public override Money Subtract(Money m) {
            var d1 = Currency.ToBase(Amount, ValidFrom);
            var d2 = m.Currency.ToBase(m.Amount, ValidFrom);
            var amount = m.Currency.FromBase(d1 - d2, ValidFrom);
            return new Money(amount, m.Currency, ValidFrom);
        }
        public override Money Multiply(decimal a)
            => Safe.Run(() => new Money(Amount * a, Currency, ValidFrom), Unspecified);
        public static bool TryParse(string s, out Money m) {
            m = new Money();
            var amountStr = s.GetHead(' ');
            var currencyStr = s.GetTail(' ');
            var r = decimal.TryParse(amountStr, out var amount);
            if (r) m = new Money(amount, currencyStr);
            return r;
        }
        public static Money Parse(string s) => TryParse(s, out var m) ? m : new Money();
        public static Money Unspecified => new();

        public override string ToString() => $"{Amount} {Currency}";
    }
}