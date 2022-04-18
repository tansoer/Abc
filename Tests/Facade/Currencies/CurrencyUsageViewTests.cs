using Abc.Aids.Random;
using Abc.Facade.Common;
using Abc.Facade.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Currencies {
    [TestClass] public class CurrencyUsageViewTests 
        : SealedTests<CurrencyUsageView, CommentedView> {
        protected override CurrencyUsageView createObject() => GetRandom.ObjectOf<CurrencyUsageView>();
        [TestMethod] public void CurrencyIdTest() => isNullableProperty<string>("Currency");
        [TestMethod] public void CountryIdTest() => isNullableProperty<string>("Country");
        [TestMethod] public void CurrencyNativeNameTest() => isNullableProperty<string>("Currency native name");
    }
}
