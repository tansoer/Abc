using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Reflection;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Abc.Pages.Quantities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Currencies {

    public sealed class CurrenciesPage : ViewPage<CurrenciesPage, ICurrencyRepo, Currency, CurrencyView, CurrencyData> {
        protected override string title => MoneyTitles.Currencies;
        private currencyPageCountries countries;
        private currencyPageRates rates;
        public CurrenciesPage(ICurrencyRepo r) : base(r) {
            countries = new currencyPageCountries(() => Item);
            rates = new currencyPageRates(() => Item);
        }
        public override void LoadDetails() {
            countries.loadDetails();
            rates.loadDetails();
        }
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
            field(x => Item.Timestamp)
        };
        protected override void addFields() {
            addField(x => Item.IsIsoCurrency);
            addField(x => Item.FullName);
            addField(x => Item.Details);
            addField(x => Item.Formula);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override void fieldsForViewers() {
            removeField(x => Item.FullName);
            removeField(x => Item.Formula);
            addFieldBefore(field(x => Item.Name), x => Item.Details);
            addFieldBefore(field(x => Item.Code), x => Item.Details);
            addFieldBefore(field(x => Item.NumericCode), x => Item.Details);
            addFieldBefore(field(x => Item.MajorUnitSymbol), x => Item.ValidFrom);
            addFieldBefore(field(x => Item.MinorUnitSymbol), x => Item.ValidFrom);
            addFieldBefore(field(x => Item.RatioOfMinorUnit), x => Item.ValidFrom);
        }
        protected override void fieldsForEditors() => fieldsForViewers();
        protected internal override string pageUrl => MoneyUrls.Currencies;
        protected internal override Currency toObject(CurrencyView v) =>
            new CurrencyViewFactory().Create(v);
        protected internal override CurrencyView toView(Currency o) => 
            new CurrencyViewFactory().Create(o);
        [BindProperty] public List<CurrencyUsageView> Countries { get => countries.details; set => countries.details = value; }
        [BindProperty] public List<ExchangeRateView> Rates { get => rates.details; set => rates.details = value; }
        public List<ComponentArgs> CountriesInputs => countries.Editors;
        public List<ComponentArgs> RatesInputs => rates.Editors;
        protected override void doAfterOnPostCreate() => createDetails();
        protected override void doBeforeOnPostCreate() => Item.Id = Item.Code;
        protected override void doBeforeOnGetEdit(string id) => LoadDetails();
        protected override void doAfterOnPostEdit() => editDetails();
        protected override void doAfterOnPostDelete(string id) => deleteDetails(id);
        private void deleteDetails(string id) {
            rates.deleteDetails(id);
            countries.deleteDetails(id);
        }
        private void createDetails() {
            rates.createDetails();
            countries.createDetails();
        }
        private void editDetails() {
            rates.editDetails();
            countries.editDetails();
        }
    }

    internal class currencyPageRates :
        masterDetailsManager<CurrencyView, ExchangeRateData, ExchangeRate,
            ExchangeRateView, IExchangeRatesRepo, ExchangeRateViewFactory> {
        public currencyPageRates(Func<CurrencyView> f)
            : base(f, GetMember.Name<ExchangeRateData>(x => x.CurrencyId)) {
            rateTypes = BasePageAids.selectListByName<IRateTypesRepo,RateType, RateTypeData>();
        }
        protected internal readonly IEnumerable<SelectListItem> rateTypes;
        protected internal override void loadDetails() {
            base.loadDetails();
            details = details.Where(x => x.ValidFrom > DateTime.Today.AddDays(-10)).ToList();
        }
        public List<ComponentArgs> Editors => new()
        {
            editorFor(nameof(ExchangeRateView.RateTypeId), rateTypes),
            editorFor(nameof(ExchangeRateView.Rate)),
            editorFor(nameof(ExchangeRateView.ValidFrom)),
            editorFor(nameof(ExchangeRateView.ValidTo)),
            editorFor(nameof(ExchangeRateView.Id), true),
            editorFor(nameof(ExchangeRateView.CurrencyId), true, itemId)
        };
        protected internal override void updateMasterId(ExchangeRateView x, string id) => x.CurrencyId = id;

        protected override bool isMasterDetail(ExchangeRate detail, string masterId) => detail.CurrencyId == masterId;
    }
    internal class currencyPageCountries :
        masterDetailsManager<CurrencyView, CurrencyUsageData, CurrencyUsage,
            CurrencyUsageView, ICurrencyUsagesRepo, CurrencyUsageViewFactory> {
        public currencyPageCountries(Func<CurrencyView> f)
            : base(f, GetMember.Name<CurrencyUsageData>(x => x.CurrencyId)) {
            countries = BasePageAids.selectListByName<ICountriesRepo,Country, CountryData>();
        }
        protected internal readonly IEnumerable<SelectListItem> countries;
        public List<ComponentArgs> Editors => new()
        {
            editorFor(nameof(CurrencyUsageView.CountryId), countries),
            editorFor(nameof(CurrencyUsageView.CurrencyNativeName)),
            editorFor(nameof(CurrencyUsageView.ValidFrom)),
            editorFor(nameof(CurrencyUsageView.ValidTo)),
            editorFor(nameof(CurrencyUsageView.Id), true),
            editorFor(nameof(CurrencyUsageView.CurrencyId), true, itemId)
        };
        protected internal override void updateMasterId(CurrencyUsageView x, string id) => x.CurrencyId = id;

        protected override bool isMasterDetail(CurrencyUsage detail, string masterId) => detail.CurrencyId == masterId;
    }

}

