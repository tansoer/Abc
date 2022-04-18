using Abc.Data.Products;
using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class PriceViewTests :SealedTests<PriceView, EntityBaseView> {
        [TestMethod] public void AmountTest() => isProperty<decimal>("Amount");
        [TestMethod] public void CurrencyIdTest() => isNullableProperty<string>("Currency");
        [TestMethod] public void DiscountIdTest() => isNullableProperty<string>("Discount");
        [TestMethod] public void PriceIdTest() => isNullableProperty<string>("Price");
        [TestMethod] public void ProductIdTest() => isNullableProperty<string>("Product");
        [TestMethod] public void PossiblePriceIdTest() => isNullableProperty<string>("Possible Price");
        [TestMethod] public void RuleSetIdTest() => isNullableProperty<string>("Rule Set");
        [TestMethod] public void RuleOverrideIdTest() => isNullableProperty<string>("Rule Override");
        [TestMethod] public void ProductTypeIdTest() => isNullableProperty<string>("Product Type");
        [TestMethod] public void KindTest() => isProperty<PriceKind>("Price Kind");

    }
}
