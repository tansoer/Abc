using Abc.Data.Common;
using Abc.Data.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders {
    [TestClass]
    public class DiscountTypeDataTests : SealedTests<DiscountTypeData, EntityTypeData> {
        [TestMethod] public void OrderManagerIdTest() => isNullable<string>();
        [TestMethod] public void EligibilityRuleSetIdTest() => isNullable<string>();
        [TestMethod] public void CurrencyIdTest() => isNullable<string>();
        [TestMethod] public void AmountTest() => isProperty<decimal>();
    }
}