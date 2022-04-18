using Abc.Data.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Abc.Pages.Currencies;

namespace Abc.Pages.Processes {

    //TODO: Is not implemented
    public sealed class RecruitingPage : ViewPage<RecruitingPage, ICountriesRepo, Country, CountryView, CountryData> {
        public RecruitingPage(ICountriesRepo r) : base(r) { }
        protected override string title => "Recruiting is not implemented";
        protected override void tableColumns() {}
        protected internal override string pageUrl => MoneyUrls.Countries;
        protected internal override Country toObject(CountryView v) =>new CountryViewFactory().Create(v);
        protected internal override CountryView toView(Country o) => new CountryViewFactory().Create(o);
    }
}