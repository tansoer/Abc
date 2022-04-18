using Abc.Data.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Infra.Common;

namespace Abc.Infra.Parties {

    public sealed class CountriesRepo : EntityRepo<Country, CountryData>, ICountriesRepo {
        public CountriesRepo(PartyDb c = null) : base(c, c?.Countries) { }
    }
}
