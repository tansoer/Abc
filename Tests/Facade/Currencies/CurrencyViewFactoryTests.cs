using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Currencies {

    [TestClass]
    public class CurrencyViewFactoryTests 
        : ViewFactoryTests<CurrencyViewFactory, CurrencyData, Currency, CurrencyView> {
        protected override void evaluateData(CurrencyView v, CurrencyData d) {
            allPropertiesAreEqual(v, d, nameof(v.Formula), nameof(v.FullName));

        }
    }

}
