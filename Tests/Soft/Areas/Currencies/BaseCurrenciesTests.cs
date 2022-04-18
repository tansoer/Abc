using Abc.Data.Common;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Common;
using Abc.Infra.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Currencies {
    public abstract class BaseCurrenciesTests<TView, TData>
        :BaseAcceptanceTests<TView, TData, MoneyDb> 
        where TData : BaseData where TView: BaseView {
        protected override void doOnCreated(MoneyDb c) => clearAll(c);

        [TestCleanup]
        public override void TestCleanup() {
            base.TestCleanup();
            clearAll(db);
        }

        protected void clearAll(MoneyDb c) {
            if (c is null) return;
            clear(c.RateRules);
            clear(c.Currencies);
            clear(c.CardAssociations);
            clear(c.Countries);
            clear(c.RateTypes);
            clear(c.Rates);
            clear(c.CurrencyUsages);
            clear(c.PaymentMethods);
        }

        protected override void isCorrectContext() {
            var n = "Money";
            var contextName = db.GetType().Name;
            Assert.IsTrue(contextName.StartsWith(n), $"Not testing {n}");
        }

        protected void addRateRule(string id) {
            var d = random<ExchangeRuleData>();
            d.Id = id;
            add<IExchangeRulesRepo, ExchangeRule>(new(d));
        }

        protected void addCurrency(string id) {
            var d = random<CurrencyData>();
            d.Id = id;
            add<ICurrencyRepo, Currency>(new(d));
        }

        protected void addCardAssociation(string id) {
            var d = random<CardAssociationData>();
            d.Id = id;
            add<ICardAssociationsRepo, CardAssociation>(new(d));
        }

        protected void addCountry(string id) {
            var d = random<CountryData>();
            d.Id = id;
            add<ICountriesRepo, Country>(new(d));
        }

        protected void addRateType(string id) {
            var d = random<RateTypeData>();
            d.Id = id;
            add<IRateTypesRepo, RateType>(new(d));
        }

        protected void addRate(string id) {
            var d = random<ExchangeRateData>();
            d.Id = id;
            add<IExchangeRatesRepo, ExchangeRate>(new(d));
        }

        protected void addCurrencyUsage(string id) {
            var d = random<CurrencyUsageData>();
            d.Id = id;
            add<ICurrencyUsagesRepo, CurrencyUsage>(new(d));
        }

        protected void addPaymentMethod(string id) {
            var d = random<PaymentMethodData>();
            d.Id = id;
            add<IPaymentMethodsRepo, PaymentMethod>(PaymentMethodFactory.Create(d));
        }
    }
}
