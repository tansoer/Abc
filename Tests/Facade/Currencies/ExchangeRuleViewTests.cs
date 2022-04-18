using Abc.Facade.Common;
using Abc.Facade.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Currencies {
    [TestClass]
    public class ExchangeRuleViewTests :SealedTests<ExchangeRuleView, EntityBaseView> {
        [TestMethod] public void RateTypeIdTest() => isNullableProperty<string>("Rate Type");
        [TestMethod] public void RuleSetIdTest() => isNullableProperty<string>("Rule Set");
    }
}
