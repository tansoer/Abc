using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Constants;
using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Abc.Pages.Currencies;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Currencies {

    [TestClass] public class ExchangeRatesPageTests : SealedViewPageTests<ExchangeRatesPage,IExchangeRatesRepo,
        ExchangeRate,ExchangeRateView,ExchangeRateData> {

        protected override System.Type getBaseClass() =>
            typeof(ViewPage<ExchangeRatesPage, IExchangeRatesRepo,
                ExchangeRate, ExchangeRateView, ExchangeRateData>);

        private class testRepo
            : mockRepo<ExchangeRate, ExchangeRateData>,
                IExchangeRatesRepo { }

        private class rateTypesRepo
            : mockRepo<RateType, RateTypeData>,
                IRateTypesRepo { }

        private class currencyRepo
            : mockRepo<Currency, CurrencyData>,
                ICurrencyRepo { }

        private rateTypesRepo rateTypes;
        private currencyRepo currencies;
        private testRepo repo;

        protected override ExchangeRatesPage createObject() {
            rateTypes = new rateTypesRepo();
            currencies = new currencyRepo();
            repo = new testRepo();
            addRandomRateTypes();
            addRandomCurrencies();
            return new ExchangeRatesPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, currencies, rateTypes);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void addRandomCurrencies() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                currencies.Add(new Currency(GetRandom.ObjectOf<CurrencyData>()));
        }

        private void addRandomRateTypes() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                rateTypes.Add(new RateType(GetRandom.ObjectOf<RateTypeData>()));
        }
        protected override string pageUrl => MoneyUrls.ExchangeRates;
        protected override string pageTitle => MoneyTitles.ExchangeRates;

        protected override ExchangeRate toObject(ExchangeRateData d) => new(d);

        [TestMethod] public void RateTypeNameTest() {
            Assert.AreEqual(Word.Unspecified, obj.RateTypeName(rndStr));
            var m = rateTypes.list[0];
            Assert.AreEqual(m.Data.Name, obj.RateTypeName(m.Data.Id));
        }

        [TestMethod]
        public void CurrencyNameTest() {
            Assert.AreEqual(Word.Unspecified, obj.CurrencyName(rndStr));
            var m = currencies.list[0];
            Assert.AreEqual(m.Data.Name, obj.CurrencyName(m.Data.Id));
        }

        [TestMethod]
        public void CurrenciesTest() {
            Assert.IsInstanceOfType(obj.Currencies, typeof(List<SelectListItem>));
            Assert.AreEqual(currencies.list.Count + 1, obj.Currencies.Count());
        }

        [TestMethod]
        public void RateTypesTest() {
            Assert.IsInstanceOfType(obj.RateTypes, typeof(List<SelectListItem>));
            Assert.AreEqual(rateTypes.list.Count + 1, obj.RateTypes.Count());
        }
    }

}
