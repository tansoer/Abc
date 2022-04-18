using System.ComponentModel;
using Abc.Facade.Common;

namespace Abc.Facade.Currencies {

    public sealed class CountryView : EntityBaseView {

        [DisplayName("Official Name")]
        public string OfficialName { get; set; }

        [DisplayName("Native Name")]
        public string NativeName { get; set; }

        [DisplayName("Numeric Code")]
        public string NumericCode { get; set; }

        [DisplayName("Is Iso Country")]
        public bool IsIsoCountry { get; set; }

        [DisplayName("Is Loyalty Program")]
        public bool IsLoyaltyProgram { get; set; }


    }

}


