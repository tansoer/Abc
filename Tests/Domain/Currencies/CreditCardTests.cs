using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Currencies {

    [TestClass] public class CreditCardTests :SealedTests<CreditCard, PaymentCard> {

        protected override CreditCard createObject()
            => new(GetRandom.ObjectOf<PaymentMethodData>());

        private async Task addCurrency() =>
            await testItemAsync<CurrencyData, Currency, ICurrencyRepo>(
                obj.CurrencyId,
                () => obj.currency.Data,
                d => new Currency(d)
            );

        [TestMethod] public async Task CreditLimitTest() {
            await addCurrency();
            var d = obj.CreditLimit;
            Assert.AreEqual(obj.Data.CreditLimit, d.Amount);
            Assert.AreEqual(obj.currency.Id, d.Currency.Id);
            allPropertiesAreEqual(obj.currency.Data, d.Currency.Data);
        }

    }

}
