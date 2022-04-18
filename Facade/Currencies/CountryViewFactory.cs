using Abc.Data.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Common;

namespace Abc.Facade.Currencies {

    public sealed class CountryViewFactory :
        AbstractViewFactory<CountryData, Country, CountryView> {

        internal protected override Country toObject(CountryData d)
            => new Country(d);

    }
}
