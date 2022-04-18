using Abc.Data.Currencies;
using Abc.Domain.Common;

namespace Abc.Domain.Parties.Contacts {

    public sealed class Country : Entity<CountryData> {
        public Country() : this(null) { }
        public Country(CountryData d) : base(d) { }
        public string IsoCode => get(Data?.Id);
        public string OfficialName => get(Data?.OfficialName);
        public string NativeName => get(Data?.NativeName);
        public string NumericCode => get(Data?.NumericCode);
        public bool IsIsoCountry => !IsUnspecified && Data.IsIsoCountry;
        public bool IsLoyaltyProgram => !IsUnspecified && Data.IsLoyaltyProgram;
    }
}


