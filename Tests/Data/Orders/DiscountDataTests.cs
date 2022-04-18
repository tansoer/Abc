using Abc.Data.Common;
using Abc.Data.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders {
    [TestClass]
    public class DiscountDataTests :SealedTests<DiscountData, EntityBaseData> {
        [TestMethod] public void DiscountTypeIdTest() => isNullable<string>();
        [TestMethod] public void DiscountTypeTest() => isProperty<DiscountsType>();
        [TestMethod] public void OrderEventIdTest() => isNullable<string>();
        [TestMethod] public void CurrencyIdTest() => isNullable<string>();
        [TestMethod] public void AmountTest() => isProperty<decimal>();
    }
}