using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class PriceDataTests : SealedTests<PriceData, EntityBaseData> {

        [TestMethod] public void AmountTest() => isProperty<decimal>();
        [TestMethod] public void CurrencyIdTest() => isNullable<string>();
        [TestMethod] public void ProductIdTest() => isNullable<string>();
        [TestMethod] public void PossiblePriceIdTest() => isNullable<string>();
        [TestMethod] public void RuleSetIdTest() => isNullable<string>();
        [TestMethod] public void DiscountIdTest() => isNullable<string>();
        [TestMethod] public void PriceIdTest() => isNullable<string>();
        [TestMethod] public void RuleOverrideIdTest() => isNullable<string>();
        [TestMethod] public void ProductTypeIdTest() => isNullable<string>();
        [TestMethod] public void KindTest() => isProperty<PriceKind>();

    }

}