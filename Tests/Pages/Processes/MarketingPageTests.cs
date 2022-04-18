using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Abc.Pages.Currencies;
using Abc.Pages.Processes;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Pages.Processes {
    [TestClass]
    public class MarketingPageTests : SealedViewPageTests<
        MarketingPage,
        ICountriesRepo,
        Country,
        CountryView,
        CountryData> {
        protected override string pageTitle => "Marketing is not implemented";
        protected override string pageUrl => MoneyUrls.Countries;
        protected override Country toObject(CountryData d) => new(d);
        private class testRepo
            : mockRepo<Country, CountryData>,
                ICountriesRepo { }

        private testRepo Repo;

        protected override Type getBaseClass() => typeof(ViewPage<MarketingPage, ICountriesRepo, 
            Country, CountryView, CountryData>);

        protected override MarketingPage createObject() {
        Repo = new testRepo();
            addRandomCountries();
            return new MarketingPage(Repo);
        }
        private void addRandomCountries() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                Repo.Add(new Country(GetRandom.ObjectOf<CountryData>()));
        }

    }
}