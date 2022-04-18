using System;
using Abc.Data.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Currencies;
using Abc.Pages.Common;

namespace Abc.Pages.Currencies {

    public sealed class CountriesPage : ViewPage<CountriesPage, ICountriesRepo, Country, CountryView, CountryData> {
        protected override string title => MoneyTitles.Countries;
        public CountriesPage(ICountriesRepo r) : base(r) { }
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.OfficialName);
            tableColumn(x => Item.NativeName);
            tableColumn(x => Item.Code);
            tableColumn(x => Item.NumericCode);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.NativeName);
            valueViewer(x => Item.OfficialName);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.NumericCode);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.IsIsoCountry);
            valueViewer(x => Item.IsLoyaltyProgram);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Id);
            valueEditor(x => Item.Name);
            valueEditor(x => Item.NativeName);
            valueEditor(x => Item.OfficialName);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.NumericCode);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.IsIsoCountry);
            valueEditor(x => Item.IsLoyaltyProgram);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => MoneyUrls.Countries;
        protected internal override Country toObject(CountryView v) =>
            new CountryViewFactory().Create(v);
        protected internal override CountryView toView(Country o) => 
            new CountryViewFactory().Create(o);
    }
}

