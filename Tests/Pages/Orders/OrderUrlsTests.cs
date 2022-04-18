using Abc.Pages.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Orders {
    [TestClass]
    public class OrderUrlsTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(OrderUrls);
        [TestMethod] public void OrdersTest() => areEqual("/Orders/Orders", OrderUrls.Orders);
        [TestMethod] public void PaymentsTest() => areEqual("/Orders/Payments", OrderUrls.Payments);
        [TestMethod] public void SalesChannelsTest() => areEqual("/Orders/SalesChannels", OrderUrls.SalesChannels);
        [TestMethod] public void TaxationTypesTest() => areEqual("/Orders/TaxationTypes", OrderUrls.TaxationTypes);
        [TestMethod] public void OrderLinesTest() => areEqual("/Orders/OrderLines", OrderUrls.OrderLines);
        [TestMethod] public void OrderEventsTest() => areEqual("/Orders/OrderEvents", OrderUrls.OrderEvents);
        [TestMethod] public void OrderPartiesTest() => areEqual("/Orders/OrderParties", OrderUrls.OrderParties);
        [TestMethod] public void OrderManagersTest() => areEqual("/Orders/OrderManagers", OrderUrls.OrderManagers);
        [TestMethod] public void RejectedItemsTest() => areEqual("/Orders/RejectedItems", OrderUrls.RejectedItems);
        [TestMethod] public void DiscountTypesTest() => areEqual("/Orders/DiscountTypes", OrderUrls.DiscountTypes);
        [TestMethod] public void DiscountsTest() => areEqual("/Orders/Discounts", OrderUrls.Discounts);
        [TestMethod] public void TaxationPoliciesTest() => areEqual("/Orders/TaxationPolicies", OrderUrls.TaxationPolicies);
        [TestMethod] public void OrderPartyTypesTest() => areEqual("/Orders/OrderPartyTypes", OrderUrls.OrderPartyTypes);

        [TestMethod] public void TermsAndConditionsTest() => areEqual("/Orders/TermsAndConditions", OrderUrls.TermsAndConditions);
        [TestMethod] public void TermsAndConditionsTypesTest() => areEqual("/Orders/TermsAndConditionsTypes", OrderUrls.TermsAndConditionsTypes);
    }
}
