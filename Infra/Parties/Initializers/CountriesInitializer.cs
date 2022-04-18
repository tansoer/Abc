using Abc.Data.Currencies;
using System.Linq;

namespace Abc.Infra.Parties.Initializers {
    public sealed class CountriesInitializer: DbInitializer {
        public static void Initialize(PartyDb db) {
            if (db.Countries.Any()) return;
            var countries = IsoCountries.Get;
            addSet(countries, db);
        }

    }
}
