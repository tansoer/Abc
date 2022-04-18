using System.ComponentModel;
using Abc.Facade.Common;

namespace Abc.Facade.Currencies {

    public sealed class ExchangeRateView : CommentedView{
        [DisplayName("Currency")] public string CurrencyId { get; set; }
        [DisplayName("Rate type")] public string RateTypeId { get; set; }
        [DisplayName("Rate")] public decimal Rate { get; set; }
    }
}
