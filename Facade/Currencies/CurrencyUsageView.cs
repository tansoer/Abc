using System.ComponentModel;
using Abc.Facade.Common;

namespace Abc.Facade.Currencies {

    public sealed class CurrencyUsageView : CommentedView {
        [DisplayName("Country")] public string CountryId { get; set; }
        [DisplayName("Currency")] public string CurrencyId { get; set; }
        [DisplayName("Currency native name")] public string CurrencyNativeName { get; set; }
    }
}
