using Abc.Facade.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Inventory {
    [TestClass]
    public class ReservationRequestViewTests :SealedTests<ReservationRequestView, InventoryItemView> {

        [TestMethod] public void RequesterPartySummaryIdTest() => isNullableProperty<string>("Requester Party Summary");
        [TestMethod] public void NumberRequestedTest() => isProperty<uint>("Number Requested");
        [TestMethod] public void ProductTypeIdTest() => isNullableProperty<string>("Product Type");
        [TestMethod] public void RuleContextIdTest() => isNullableProperty<string>("Rule Context");
        [TestMethod] public void RuleOverridesIdTest() => isNullableProperty<string>("Rule Overrides");
    }
}
