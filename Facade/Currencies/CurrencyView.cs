using Abc.Facade.Attributes;
using Abc.Facade.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Abc.Facade.Currencies {

    public sealed class CurrencyView : EntityBaseView {
        [DisplayName("Numeric code")] public string NumericCode { get; set; }
        [DisplayName("Major unit symbol")] public string MajorUnitSymbol { get; set; }
        [DisplayName("Minor unit symbol")] public string MinorUnitSymbol { get; set; }
        [DisplayName("Ratio of minor unit")] public double RatioOfMinorUnit { get; set; }
        [DisplayName("Is ISO currency")] public bool IsIsoCurrency { get; set; }
        [DisplayName("Full name")] public string FullName { get; set; }
        [Required] [Unique(typeof(Domain.Currencies.ICurrencyRepo))] public new string Code { get; set; }
        public string Formula { get; set; }
    }
}
