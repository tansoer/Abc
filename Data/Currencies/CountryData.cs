using Abc.Data.Common;

namespace Abc.Data.Currencies {

    public sealed class CountryData : EntityBaseData {

        public string OfficialName { get; set; }
        public string NativeName { get; set; }
        public string NumericCode { get; set; }
        public bool IsIsoCountry { get; set; }
        public bool IsLoyaltyProgram { get; set; }

    }

}