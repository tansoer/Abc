using System.Collections.Generic;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Currencies {

    public sealed class ExchangeRatesPage : ViewPage<ExchangeRatesPage, IExchangeRatesRepo, ExchangeRate,
        ExchangeRateView, ExchangeRateData> {
        protected override string title => MoneyTitles.ExchangeRates;
        public ExchangeRatesPage(IExchangeRatesRepo r) :
            base(r) {
        }
        protected override void tableColumns() {
            tableColumn(x => x.Item.RateTypeId);
            tableColumn(x => x.Item.CurrencyId);
            tableColumn(x => x.Item.Rate);
            tableColumn(x => x.Item.ValidFrom);
            tableColumn(x => x.Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.RateTypeId, () => RateTypeName(Item.RateTypeId));
            valueViewer(x => Item.CurrencyId, () => CurrencyName(Item.CurrencyId));
            valueViewer(x => Item.Rate);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.RateTypeId, () => RateTypes);
            valueEditor(x => Item.CurrencyId, () => Currencies);
            valueEditor(x => Item.Rate);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => MoneyUrls.ExchangeRates;
        protected internal override ExchangeRate toObject(ExchangeRateView v) => 
            new ExchangeRateViewFactory().Create(v);
        protected internal override ExchangeRateView toView(ExchangeRate o) => 
            new ExchangeRateViewFactory().Create(o);
        private IEnumerable<SelectListItem> rateTypes;
        private IEnumerable<SelectListItem> currencies;
        public IEnumerable<SelectListItem> RateTypes => rateTypes 
            ??= selectListByName<IRateTypesRepo, RateType, RateTypeData>();
        public IEnumerable<SelectListItem> Currencies => currencies 
            ??= selectListByName<ICurrencyRepo, Currency, CurrencyData>();
        public string CurrencyName(string id) => itemName(Currencies, id);
        public string RateTypeName(string id) => itemName(RateTypes, id);
        protected internal override string subtitle => CurrencyName(FixedValue);
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate)
            => Item = new();
    }
}
