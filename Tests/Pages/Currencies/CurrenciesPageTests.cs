using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Abc.Pages.Currencies;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Pages.Currencies {

    [TestClass]
    public class CurrenciesPageTests :SealedViewPageTests<
        CurrenciesPage, ICurrencyRepo, Currency, CurrencyView, CurrencyData> {
        protected override Type getBaseClass() => typeof(ViewPage<CurrenciesPage, ICurrencyRepo, Currency, CurrencyView, CurrencyData>);
        internal class testRepo :mockRepo<Currency, CurrencyData>, ICurrencyRepo { }
        private testRepo Repo;
        protected override CurrenciesPage createObject() {
            Repo = new testRepo();
            addRandomCurrencies();
            return new CurrenciesPage(Repo);
        }
        private void addRandomCurrencies() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                Repo.Add(new Currency(GetRandom.ObjectOf<CurrencyData>()));
        }
        protected override string pageUrl => MoneyUrls.Currencies;
        protected override string pageTitle => MoneyTitles.Currencies;
        protected override Currency toObject(CurrencyData d) => new(d);
        protected override void validateData(CurrencyView v, CurrencyData d)
            => allPropertiesAreEqual(v, d, nameof(v.FullName), nameof(v.Formula));
    }
}
