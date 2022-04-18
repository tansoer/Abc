using Abc.Pages.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Orders {
    [TestClass]
    public class OrderTitlesTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(OrderTitles);
        [TestMethod] public void SalesChannelsTest() => areEqual("Sales channels", OrderTitles.SalesChannels);
        [TestMethod] public void TaxationTypesTest() => areEqual("Taxation types", OrderTitles.TaxationTypes);
        [TestMethod] public void OrdersTest() => areEqual("Orders", OrderTitles.Orders);
        [TestMethod] public void PaymentsTest() => areEqual("Payments", OrderTitles.Payments);

        [TestMethod] public void TaxationPoliciesTest() => areEqual("Taxation policies", OrderTitles.TaxationPolicies);
        [TestMethod] public void OrderPartiesTest() => areEqual("Order parties", OrderTitles.OrderParties);
        [TestMethod] public void OrderPartyTypesTest() => areEqual("Order party types", OrderTitles.OrderPartyTypes);
        [TestMethod] public void OrderLinesTest() => areEqual("Order lines", OrderTitles.OrderLines);
        [TestMethod] public void OrderEventsTest() => areEqual("Order events", OrderTitles.OrderEvents);
        [TestMethod] public void OrderManagersTest() => areEqual("Order managers", OrderTitles.OrderManagers);
        [TestMethod] public void RejectedItemsTest() => areEqual("Rejected items", OrderTitles.RejectedItems);
        [TestMethod] public void DiscountTypesTest() => areEqual("Discount types", OrderTitles.DiscountTypes);
        [TestMethod] public void DiscountsTest() => areEqual("Discounts", OrderTitles.Discounts);

        [TestMethod] public void TermsAndConditionsTest() => areEqual("Terms and conditions", OrderTitles.TermsAndConditions);
        [TestMethod] public void TermsAndConditionsTypesTest() => areEqual("Terms and conditions types", OrderTitles.TermsAndConditionsTypes);

    }
}
