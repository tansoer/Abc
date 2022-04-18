using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Currencies {

    [TestClass]
    public class ExchangeRateTests : SealedTests<ExchangeRate, DetailedEntity<ExchangeRateData>> {

        protected override ExchangeRate createObject() => new (GetRandom.ObjectOf<ExchangeRateData>());

        [TestMethod] public void CurrencyIdTest() => isReadOnly(obj.Data.CurrencyId);

        [TestMethod] public void RateTypeIdTest() => isReadOnly(obj.Data.RateTypeId);

        [TestMethod] public void IsOfficialTest() => isReadOnly(obj.RateType.IsOfficial);

        [TestMethod] public void RateTest() => isReadOnly(obj.Data.Rate);

        [TestMethod]
        public async Task RateTypeTest() {
            await testItemAsync<RateTypeData, RateType, IRateTypesRepo>(
                obj.RateTypeId, () => obj.RateType.Data, d => new RateType(d));
            obj = createObject();
            Assert.IsNotNull(obj.RateType);
            Assert.IsNotNull(obj.RateType.Data);
        }

        [TestMethod]
        public async Task CurrencyTest() {
            await testItemAsync<CurrencyData, Currency, ICurrencyRepo>(
                obj.CurrencyId, () => obj.Currency.Data, d => new Currency(d));
            obj = createObject();
            Assert.IsNotNull(obj.Currency);
            Assert.IsNotNull(obj.Currency.Data);
        }
    }
}
