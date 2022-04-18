using System;
using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Values {

    [TestClass]
    public class MoneyValueTests : BaseComparableValueTests<MoneyValue, Money> {

        private DateTime date;
        private decimal d;

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            d = GetRandom.Decimal(0.000001M, 1000000);
            date = GetRandom.DateTime(DateTime.Now.AddYears(-30), DateTime.Now);
            GetRepo.Instance<ICurrencyRepo>().Add(randomCurrency());
            GetRepo.Instance<IExchangeRatesRepo>().Add(randomRate());
            f = randomMoney();
            t = randomMoney();

            if (f.Amount <= t.Amount) return;
            var m = f;
            f = t;
            t = m;
        }

        [TestMethod] public void CurrencyIdTest() => isReadOnly(obj.Data.UnitOrCurrencyId);

        [TestMethod] public void FromTest() => isReadOnly(obj.Data.ValidFrom);

        protected override void addTest() {
            assert(f, t, new Money(f.Amount + t.Amount, f.Currency, date));
        }

        protected void assert(Money a, Money b, Money expected) {
            var x = varX(a);
            var y = varY(b);
            var name = $"(({a}) {functionSign} ({b}))";
            var z = binaryFunction(x, y);
            Assert.IsNotNull(z);
            Assert.AreEqual(name, z.Name);
            Assert.AreEqual(Money.round(expected.Amount), Money.round(((Money) z.GetValue()).Amount));
            Assert.AreEqual(expected.Currency.Id, ((Money) z.GetValue()).Currency.Id);
        }

        protected void assert(Money a, decimal b, Money expected) {
            var x = varX(a);
            var y = varY(b);
            var name = $"(({a}) {functionSign} ({b}))";
            var z = binaryFunction(x, y);
            Assert.IsNotNull(z);
            Assert.AreEqual(name, z.Name);
            Assert.AreEqual(Money.round(expected.Amount), Money.round(((Money) z.GetValue()).Amount));
            Assert.AreEqual(expected.Currency.Id, ((Money) z.GetValue()).Currency.Id);
        }

        protected void assert(Money a, Money expected) {
            var x = varX(a);
            var name = $"({functionSign} ({a}))";
            var z = unaryFunction(x);
            Assert.IsNotNull(z);
            Assert.AreEqual(name, z.Name);
            Assert.AreEqual(expected.ToString(), z.GetValue().ToString());
        }

        protected override void divideTest() {
            assert(f, d, new Money(f.Amount / d, f.Currency, date));
        }

        protected override void multiplyTest() {
            assert(f, d, new Money(f.Amount * d, f.Currency, date));
        }

        protected override void oppositeTest() {
            assert(f, new Money(-f.Amount, f.Currency, date));
        }

        protected override void subtractTest() {
            assert(f, t, new Money(f.Amount - t.Amount, f.Currency, date));
        }

        private static CurrencyData data() {
            var c = new CurrencyData { Id = "c", Name = "c", Code = "c", Details = "c" };

            return c;
        }

        private static Currency randomCurrency() {
            return new Currency(data());
        }

        private static decimal randomDecimal() {
            return GetRandom.Decimal(-1000000, 1000000);
        }

        private Money randomMoney() {
            return new Money(randomDecimal(), randomCurrency(), date);
        }

        private ExchangeRate randomRate() {
            var r = GetRandom.ObjectOf<ExchangeRateData>();
            r.CurrencyId = "c";
            r.RateTypeId = RateType.OfficialRate;
            r.ValidFrom = date.Date;
            r.ValidTo = date.Date.AddDays(1).AddSeconds(-1);
            r.Rate = Convert.ToDecimal(GetRandom.Double(1, 1000));

            return new ExchangeRate(r);
        }

    }

}