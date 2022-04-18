using System;
using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Currencies {

    [TestClass] public class CurrencyTests :SealedTests<Currency, BaseMetric<CurrencyData>> {
        private DateTime date;
        private ExchangeRateData cbRate;
        protected override Currency createObject() => new(GetRandom.ObjectOf<CurrencyData>());
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            date = GetRandom.DateTime(DateTime.Now.AddYears(-30), DateTime.Now);
        }
        [TestMethod] public void RatesTest() {
            var count = GetRandom.UInt8(10, 30);
            var r = GetRepo.Instance<IExchangeRatesRepo>();
            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<ExchangeRateData>();
                if (i % 2 == 0) d.CurrencyId = obj.Id;
                if (i % 4 == 0) {
                    d.CurrencyId = obj.Id;
                    d.ValidFrom = date.Date;
                    d.ValidTo = date.Date.AddDays(1).AddSeconds(-1);
                }
                r.Add(new ExchangeRate(d));
            }
            var t = obj.Rates(date);
            Assert.IsNotNull(t);
            Assert.AreEqual(Math.Ceiling(count / 4.0), t.Count);
        }
        [TestMethod] public async Task AcceptedInTest()
            => await testListAsync<CurrencyUsage, CurrencyUsageData, ICurrencyUsagesRepo>(
                d => d.CurrencyId = obj.Id, d => new CurrencyUsage(d));
        [TestMethod] public void AcceptedInCountriesTest() {
            testRelatedList<Country, CountryData, CurrencyUsage, ICountriesRepo>(
                () => obj.AcceptedInCountries, () => obj.AcceptedIn,
                (d, e) => { d.Id = e.CountryId; return new Country(d); },
                AcceptedInTest, (x, r) => x.Id == r.CountryId);
        }
        [TestMethod] public void NumericCodeTest() => isReadOnly(obj.Data.NumericCode);
        [TestMethod] public void MajorUnitSymbolTest() => isReadOnly(obj.Data.MajorUnitSymbol);
        [TestMethod] public void MinorUnitSymbolTest() => isReadOnly(obj.Data.MinorUnitSymbol);
        [TestMethod] public void RatioOfMinorUnitTest() => isReadOnly(obj.Data.RatioOfMinorUnit);
        [TestMethod] public void IsIsoCurrencyTest() => isReadOnly(obj.Data.IsIsoCurrency);
        [TestMethod] public void CentralBankDayRateTest() {
            cbRate = addRandomRates(obj, date);
            allPropertiesAreEqual(cbRate, obj.CentralBankDayRate(date).Data);
        }
        [TestMethod] public void RateTest() {
            cbRate = addRandomRates(obj, date);
            Assert.AreEqual(cbRate.Rate, obj.Rate(date));
        }
        [TestMethod] public void ToBaseTest() {
            cbRate = addRandomRates(obj, date);
            var d = GetRandom.Decimal(-1000, 1000);
            Assert.AreEqual(cbRate.Rate * d, obj.ToBase(d, date));
        }
        [TestMethod] public void FromBaseTest() {
            cbRate = addRandomRates(obj, date);
            var d = GetRandom.Decimal(-1000, 1000);
            Assert.AreEqual(d / cbRate.Rate, obj.FromBase(d, date));
        }
        internal static ExchangeRateData addRandomRates(Currency c, DateTime d) {
            var r = GetRepo.Instance<IExchangeRatesRepo>();
            var cRate = GetRandom.ObjectOf<ExchangeRateData>();
            cRate.RateTypeId = RateType.OfficialRate;
            cRate.CurrencyId = c.Id;
            cRate.ValidFrom = d.Date;
            cRate.ValidTo = d.Date.AddDays(1).AddSeconds(-1);
            cRate.Rate = Convert.ToDecimal(GetRandom.Double(0.0001, 1000));
            var count = GetRandom.Int32(10, 100);
            var idx = GetRandom.Int32(0, count);
            for (var i = 0; i <= count; i++) {
                var rate = GetRandom.ObjectOf<ExchangeRateData>();
                r.Add(new ExchangeRate(i == idx ? cRate : rate));
            }
            return cRate;
        }

        [TestMethod] public void ToStringTest() => areEqual(obj.FullName, obj.ToString());
        [TestMethod] public void FullNameTest()
            => areEqual($"{obj.Name} ({obj.Code}, {obj.NumericCode})",obj.FullName);
        [TestMethod]
        public void FormulaTest() {
            var major = Currency.fixDirection(obj.MajorUnitSymbol);
            var minor = Currency.fixDirection(obj.MinorUnitSymbol);
            areEqual( $"1 {major} = {obj.RatioOfMinorUnit} {minor}", obj.Formula);
        }
    }
}
