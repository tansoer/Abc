using Abc.Facade.Common;
using Abc.Facade.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Currencies {

    [TestClass]
    public class ExchangeRateViewTests: 
        SealedTests<ExchangeRateView, CommentedView> {
        [TestMethod] public void CurrencyIdTest() => isNullableProperty<string>("Currency");
        [TestMethod] public void RateTypeIdTest() => isNullableProperty<string>("Rate type");
        [TestMethod] public void RateTest() => isProperty<decimal>();
    }
}
