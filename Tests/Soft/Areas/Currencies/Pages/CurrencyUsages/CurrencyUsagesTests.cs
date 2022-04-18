using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Infra.Parties;
using Abc.Pages.Currencies;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Currencies.Pages.CurrencyUsages {
    public abstract class CurrencyUsagesTests :BaseCurrenciesTests<CurrencyUsageView, CurrencyUsageData> {
        private PartyDb partyDb;
        protected List<SelectListItem> countries => createSelectList(partyDb.Countries);
        protected List<SelectListItem> currencies => createSelectList(db.Currencies);
        protected override CurrencyUsageView toView(CurrencyUsageData d) =>
            new CurrencyUsageViewFactory().Create(new CurrencyUsage(d));
        [TestInitialize] public override void TestInitialize() {
            partyDb = getContext<PartyDb>();
            base.TestInitialize();
        }
        protected override string baseUrl() => MoneyUrls.CurrencyUsages;
        protected override void changeValuesExceptId(CurrencyUsageData d) {
            var id = d.Id;
            d = random<CurrencyUsageData>();
            d.Id = id;
        }
        protected override string getItemId(CurrencyUsageData d) => d.Id;
        protected override void setItemId(CurrencyUsageData d, string id) => d.Id = id;
        protected override void addRelatedItems(CurrencyUsageData d) {
            if (d is null) return;
            addCountry(d.CountryId);
            addCurrency(d.CurrencyId);
        }
        protected override IEnumerable<Expression<Func<CurrencyUsageView, object>>> indexPageColumns =>
            new Expression<Func<CurrencyUsageView, object>>[] {
                x => x.CountryId,
                x => x.CurrencyId,
                x => x.CurrencyNativeName,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.CountryId, Unspecified.String);
            canView(item, m => m.CurrencyId, Unspecified.String);
            canView(item, m => m.CurrencyNativeName);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canSelect(item, m => m.CountryId, countries);
            canSelect(item, m => m.CurrencyId, currencies);
            canEdit(item, m => m.CurrencyNativeName);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canSelect(null, m => m.CountryId, countries);
            canSelect(null, m => m.CurrencyId, currencies);
            canEdit(null, m => m.CurrencyNativeName);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            HiddenInputs.Clear();
        }
    }
    [TestClass] public class CreatePageTests :CurrencyUsagesTests { }
    [TestClass] public class DeletePageTests :CurrencyUsagesTests { }
    [TestClass] public class DetailsPageTests :CurrencyUsagesTests { }
    [TestClass] public class EditPageTests :CurrencyUsagesTests { }
    [TestClass] public class IndexPageTests :CurrencyUsagesTests { }
}
