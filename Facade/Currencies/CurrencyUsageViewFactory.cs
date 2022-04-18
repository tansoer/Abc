using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Common;
namespace Abc.Facade.Currencies {

    public sealed class CurrencyUsageViewFactory :
        AbstractViewFactory<CurrencyUsageData, CurrencyUsage, CurrencyUsageView> {
        internal protected override CurrencyUsage toObject(CurrencyUsageData d)
            => new CurrencyUsage(d);
    }
}
