using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Parties.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Currencies {

    [TestClass]
    public class CurrencyUsageTests : SealedTests<CurrencyUsage, DetailedEntity<CurrencyUsageData>> {

        protected override CurrencyUsage createObject() => new (GetRandom.ObjectOf<CurrencyUsageData>());

        [TestMethod] public void CountryIdTest() => isReadOnly(obj.Data.CountryId);

        [TestMethod] public void CurrencyIdTest() => isReadOnly(obj.Data.CurrencyId);

        [TestMethod] public void CurrencyNativeNameTest() => isReadOnly(obj.Data.CurrencyNativeName);

        [TestMethod]
        public async Task CountryTest()
            => await testItemAsync<CountryData, Country, ICountriesRepo>(
                obj.CountryId, () => obj.Country.Data, d => new Country(d));

        [TestMethod]
        public async Task CurrencyTest()
            => await testItemAsync<CurrencyData, Currency, ICurrencyRepo>(
                obj.CurrencyId, () => obj.Currency.Data, d => new Currency(d));
    }

}
