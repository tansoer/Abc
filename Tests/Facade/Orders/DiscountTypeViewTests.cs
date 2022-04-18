using Abc.Facade.Common;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class DiscountTypeViewTests: SealedTests<DiscountTypeView, EntityTypeView> {
        [TestMethod] public void OrderManagerIdTest() => isNullableProperty<string>("Order manager");
        [TestMethod] public void EligibilityRuleSetIdTest() => isNullableProperty<string>("Eligibility rule");
        [TestMethod] public void AmountTest() => isProperty<decimal>("Amount");
        [TestMethod] public void CurrencyIdTest() => isNullableProperty<string>("Currency");
    }
}