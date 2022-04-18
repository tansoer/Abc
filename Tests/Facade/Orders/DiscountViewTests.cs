using Abc.Data.Orders;
using Abc.Facade.Common;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class DiscountViewTests :SealedTests<DiscountView, EntityBaseView> {
        [TestMethod] public void DiscountTypeIdTest() => isNullableProperty<string>("Discount type");
        [TestMethod] public void DiscountTypeTest() => isProperty<DiscountsType>("Discounts type");
        [TestMethod] public void OrderEventIdTest() => isNullableProperty<string>("Order event");
        [TestMethod] public void CurrencyIdTest() => isNullableProperty<string>("Currency");
        [TestMethod] public void AmountTest() => isProperty<decimal>("Amount");
    }
}