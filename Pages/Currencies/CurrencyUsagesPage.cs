using System.Collections.Generic;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Currencies {

    public sealed class CurrencyUsagesPage 
        : ViewPage<CurrencyUsagesPage, ICurrencyUsagesRepo, CurrencyUsage, CurrencyUsageView, CurrencyUsageData> {
        protected override string title => MoneyTitles.CurrencyUsages;
        protected internal override string subtitle => CurrencyName(FixedValue);
        public CurrencyUsagesPage(ICurrencyUsagesRepo r) : base(r) {  }
        private IEnumerable<SelectListItem> countries;
        public IEnumerable<SelectListItem> Countries => countries ??= selectListByName<ICountriesRepo, Country, CountryData>();
        private IEnumerable<SelectListItem> currencies;
        public IEnumerable<SelectListItem> Currencies => currencies ??= selectListByName<ICurrencyRepo, Currency, CurrencyData>();
        protected override void tableColumns() {
            tableColumn(x => x.Item.CountryId, () => CountryName(Item.CountryId));
            tableColumn(x => x.Item.CurrencyId, () => CurrencyName(Item.CurrencyId));
            tableColumn(x => x.Item.CurrencyNativeName);
            tableColumn(x => x.Item.ValidFrom);
            tableColumn(x => x.Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.CountryId, () => CountryName(Item.CountryId));
            valueViewer(x => Item.CurrencyId, () => CurrencyName(Item.CurrencyId));
            valueViewer(x => Item.CurrencyNativeName);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.CountryId, () => Countries);
            valueEditor(x => Item.CurrencyId, () => Currencies);
            valueEditor(x => Item.CurrencyNativeName);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => MoneyUrls.CurrencyUsages;
        protected internal override CurrencyUsage toObject(CurrencyUsageView v) => new CurrencyUsageViewFactory().Create(v);
        protected internal override CurrencyUsageView toView(CurrencyUsage obj) => new CurrencyUsageViewFactory().Create(obj);
        public string CurrencyName(string id) => itemName(Currencies, id);
        public string CountryName(string id) => itemName(Countries, id);
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate)
            => Item = new();
    }
}

