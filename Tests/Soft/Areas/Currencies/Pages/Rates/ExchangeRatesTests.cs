using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Pages.Currencies;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Currencies.Pages.Rates {
    public abstract class ExchangeRatesTests :BaseCurrenciesTests<ExchangeRateView, ExchangeRateData> {

        protected List<SelectListItem> rateTypes => createSelectList(db.RateTypes);
        protected List<SelectListItem> currencies => createSelectList(db.Currencies);
        protected override ExchangeRateView toView(ExchangeRateData d) 
            => new ExchangeRateViewFactory().Create(new ExchangeRate(d));
        protected override string baseUrl() => MoneyUrls.ExchangeRates;
        protected override void changeValuesExceptId(ExchangeRateData d) {
            var id = d.Id;
            d = random<ExchangeRateData>();
            d.Id = id;
        }
        protected override string getItemId(ExchangeRateData d) => d.Id;
        protected override void setItemId(ExchangeRateData d, string id) => d.Id = id;
        protected override void addRelatedItems(ExchangeRateData d) {
            if (d is null) return;
            addRateType(d.RateTypeId);
            addCurrency(d.CurrencyId);
        }
        protected override ExchangeRateData randomItem() {
            var d = base.randomItem();
            d.Rate = Math.Round(GetRandom.Decimal(-1000, 1000), 2);
            return d;
        }
        protected override IEnumerable<Expression<Func<ExchangeRateView, object>>> indexPageColumns =>
            new Expression<Func<ExchangeRateView, object>>[] {
                x => x.RateTypeId,
                x => x.CurrencyId,
                x => x.Rate,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.RateTypeId, Unspecified.String);
            canView(item, m => m.CurrencyId, Unspecified.String);
            canView(item, m => m.Rate);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canSelect(item, m => m.RateTypeId, rateTypes);
            canSelect(item, m => m.CurrencyId, currencies);
            canEdit(item, m => m.Rate, true, true, 0.00.ToString("0.00"));
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() { ;
            canSelect(null, m => m.RateTypeId, rateTypes);
            canSelect(null, m => m.CurrencyId, currencies);
            canEdit(null, m => m.Rate, true, true, 0.00.ToString("0.00"));
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            HiddenInputs.Clear();
        }
    }
    [TestClass] public class CreatePageTests :ExchangeRatesTests { }
    [TestClass] public class DeletePageTests :ExchangeRatesTests { }
    [TestClass] public class DetailsPageTests :ExchangeRatesTests { }
    [TestClass] public class EditPageTests :ExchangeRatesTests { }
    [TestClass] public class IndexPageTests :ExchangeRatesTests { }
}
