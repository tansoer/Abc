using Abc.Data.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Currencies;
using Abc.Infra.Parties;
using Abc.Pages.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Currencies.Pages.Countries {
    public abstract class CountriesTests :BaseAcceptanceTests<CountryView, CountryData, PartyDb> {
        //Countries DB set is in PartyDb, thus this test class cannot inherit
        //from BaseCurrenciesTests and needs some strange overrides
        [TestCleanup] public override void TestCleanup() {
            base.TestCleanup();
            clear(db.Countries);
        }
        protected override string baseUrl() => MoneyUrls.Countries;
        protected override void changeValuesExceptId(CountryData d) {
            var id = d.Id;
            d = random<CountryData>();
            d.Id = id;
        }
        protected override string getItemId(CountryData d) => d.Id;
        protected override void setItemId(CountryData d, string id) => d.Id = id;
        protected override string baseClassName() => "Country";
        protected override string performTitleCorrection(string n) => "Country";
        protected override CountryView toView(CountryData d) => new CountryViewFactory().Create(new Country(d));
        protected override void isCorrectContext() {
            var n = "Party";
            var contextName = db.GetType().Name;
            Assert.IsTrue(contextName.StartsWith(n), $"Not testing {n}");
        }
        protected override IEnumerable<Expression<Func<CountryView, object>>> indexPageColumns =>
            new Expression<Func<CountryView, object>>[] {
                x => x.Name,
                x => x.OfficialName,
                x => x.NativeName,
                x => x.Code,
                x => x.NumericCode,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            CountryView v = toView(item);
            canView(v, m => m.Id);
            canView(v, m => m.Name);
            canView(v, m => m.OfficialName);
            canView(v, m => m.NativeName);
            canView(v, m => m.Code);
            canView(v, m => m.NumericCode);
            canView(v, m => m.Details);
            canViewCheckBox(v, m => m.IsIsoCountry);
            canViewCheckBox(v, m => m.IsLoyaltyProgram);
            canView(v, m => m.ValidFrom);
            canView(v, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            CountryView v = toView(item);
            canEdit(v, m => m.Id, true);
            canEdit(v, m => m.Name, true);
            canEdit(v, m => m.OfficialName);
            canEdit(v, m => m.NativeName);
            canEdit(v, m => m.Code);
            canEdit(v, m => m.NumericCode);
            canEdit(v, m => m.Details);
            canClickCheckBox(v, m => m.IsIsoCountry, true);
            canClickCheckBox(v, m => m.IsLoyaltyProgram, true);
            canEdit(v, m => m.ValidFrom);
            canEdit(v, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Id, true);
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.OfficialName);
            canEdit(null, m => m.NativeName);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.NumericCode);
            canEdit(null, m => m.Details);
            canClickCheckBox(null, m => m.IsIsoCountry, true);
            canClickCheckBox(null, m => m.IsLoyaltyProgram, true);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            HiddenInputs.Clear();
        }
    }
    [TestClass] public class CreatePageTests :CountriesTests { }
    [TestClass] public class DeletePageTests :CountriesTests { }
    [TestClass] public class DetailsPageTests :CountriesTests { }
    [TestClass] public class EditPageTests :CountriesTests { }
    [TestClass] public class IndexPageTests :CountriesTests { }
}
