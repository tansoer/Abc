using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Pages.Currencies;
using AngleSharp.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Currencies.Pages.Currencies {
    public abstract class CurrenciesTests :BaseCurrenciesTests<CurrencyView, CurrencyData> {
        protected override string baseUrl() => MoneyUrls.Currencies;
        protected override void changeValuesExceptId(CurrencyData d) {
            var id = d.Id;
            d = random<CurrencyData>();
            d.Id = id;
        }
        protected override CurrencyView toView(CurrencyData d) => new CurrencyViewFactory().Create(new Currency(d));
        protected override CurrencyData randomItem() {
            var d = base.randomItem();
            d.RatioOfMinorUnit = GetRandom.Double(-1000, 1000);
            d.Id = d.Code;
            return d;
        }
        protected override string baseClassName() {
            var n = GetType().BaseType?.Name;
            n = n?.ReplaceFirst("Base", string.Empty);
            n = n?.ReplaceFirst("iesTests", "y");
            return n;
        }
        protected override string performTitleCorrection(string n) => n.Replace("ies", "y");
        protected override IEnumerable<Expression<Func<CurrencyView, object>>> indexPageColumns =>
            new Expression<Func<CurrencyView, object>>[] {
                x => x.IsIsoCurrency,
                x => x.FullName,
                x => x.Formula,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            CurrencyView v = toView(item);
            canView(v, m => m.Name);
            canView(v, m => m.Code);
            canView(v, m => m.NumericCode);
            canView(v, m => m.Details);
            canView(v, m => m.MajorUnitSymbol);
            canView(v, m => m.MinorUnitSymbol);
            canView(v, m => m.RatioOfMinorUnit);
            canViewCheckBox(v, m => m.IsIsoCurrency);
            canView(v, m => m.ValidFrom);
            canView(v, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            CurrencyView v = toView(item);
            canEdit(v, m => m.Name, true);
            canEdit(v, m => m.Code, true);
            canEdit(v, m => m.NumericCode);
            canEdit(v, m => m.Details);
            canEdit(v, m => m.MajorUnitSymbol);
            canEdit(v, m => m.MinorUnitSymbol);
            canEdit(v, m => m.RatioOfMinorUnit, true, true);
            canClickCheckBox(v, m => m.IsIsoCurrency, true);
            canEdit(v, m => m.ValidFrom);
            canEdit(v, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code, true);
            canEdit(null, m => m.NumericCode);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.MajorUnitSymbol);
            canEdit(null, m => m.MinorUnitSymbol);
            canEdit(null, m => m.RatioOfMinorUnit, true, true);
            canClickCheckBox(null, m => m.IsIsoCurrency, true);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            hasHidden(item, x => x.Timestamp, true);
        }
    }
    [TestClass] public class CreatePageTests :CurrenciesTests { }
    [TestClass] public class DeletePageTests :CurrenciesTests { }
    [TestClass] public class DetailsPageTests :CurrenciesTests { }
    [TestClass] public class EditPageTests :CurrenciesTests { }
    [TestClass] public class IndexPageTests :CurrenciesTests { }
}
