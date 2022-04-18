using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Abc.Pages.Currencies;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Pages.Currencies {

    [TestClass]
    public class CountriesPageTests : SealedViewPageTests< CountriesPage, ICountriesRepo,
        Country, CountryView, CountryData> {
        protected override Type getBaseClass() 
            => typeof(ViewPage<CountriesPage, ICountriesRepo, Country, CountryView, CountryData>);

        private class testRepo: mockRepo<Country, CountryData>, ICountriesRepo { }
        private testRepo Repo;
        protected override CountriesPage createObject() {
        Repo = new testRepo();
            addRandomCountries();
            return new CountriesPage(Repo);
        }
        private void addRandomCountries() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                Repo.Add(new Country(GetRandom.ObjectOf<CountryData>()));
        }
        protected override string pageUrl => MoneyUrls.Countries;
        protected override string pageTitle => MoneyTitles.Countries;
        protected override Country toObject(CountryData d) => new (d);
    }
}
