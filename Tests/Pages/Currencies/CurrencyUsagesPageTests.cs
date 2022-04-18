using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Constants;
using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Abc.Pages.Currencies;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Currencies {
    [TestClass] public class CurrencyUsagesPageTests : SealedViewPageTests<CurrencyUsagesPage, 
        ICurrencyUsagesRepo, CurrencyUsage, CurrencyUsageView, CurrencyUsageData> {
        protected override System.Type getBaseClass() =>
            typeof(ViewPage<CurrencyUsagesPage, ICurrencyUsagesRepo,
                CurrencyUsage, CurrencyUsageView, CurrencyUsageData>);
        private class testRepo : mockRepo<CurrencyUsage, CurrencyUsageData>, ICurrencyUsagesRepo {
        }
        private class countriesRepo
            : mockRepo<Country, CountryData>,
                ICountriesRepo { }
        private class currencyRepo
            : mockRepo<Currency, CurrencyData>,
                ICurrencyRepo { }
        private countriesRepo countries;
        private currencyRepo currencies;
        private testRepo repo;
        protected override CurrencyUsagesPage createObject() {
            countries = new countriesRepo();
            currencies = new currencyRepo();
            repo = new testRepo();
            addRandomCountries();
            addRandomCurrencies();
            return new CurrencyUsagesPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, countries, currencies);
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
        private void addRandomCountries() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                countries.Add(new Country(GetRandom.ObjectOf<CountryData>()));
        }
        protected override string pageUrl => MoneyUrls.CurrencyUsages;

        protected override string pageTitle => MoneyTitles.CurrencyUsages;

        protected override CurrencyUsage toObject(CurrencyUsageData d) => new (d);
        [TestMethod] public void CountryNameTest() {
            Assert.AreEqual(Word.Unspecified, obj.CountryName(rndStr));
            var m = countries.list[0];
            Assert.AreEqual(m.Data.Name, obj.CountryName(m.Data.Id));
        }
        [TestMethod] public void CurrencyNameTest() {
            Assert.AreEqual(Word.Unspecified, obj.CurrencyName(rndStr));
            var m = currencies.list[0];
            Assert.AreEqual(m.Data.Name, obj.CurrencyName(m.Data.Id));
        }
        [TestMethod] public void CurrenciesTest() {
            Assert.IsInstanceOfType(obj.Currencies, typeof(List<SelectListItem>));
            Assert.AreEqual(currencies.list.Count + 1, obj.Currencies.Count());
        }
        [TestMethod] public void CountriesTest() {
            Assert.IsInstanceOfType(obj.Countries, typeof(List<SelectListItem>));
            Assert.AreEqual(countries.list.Count + 1, obj.Countries.Count());
        }
    }
}
