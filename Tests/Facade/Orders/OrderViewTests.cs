using Abc.Data.Orders;
using Abc.Facade.Common;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Facade.Orders {

    [TestClass]
    public class OrderViewTests : SealedTests<OrderView, EntityBaseView> {
        [TestMethod] public void OrderTypeTest() => isProperty<OrderType>("Order type");
        [TestMethod] public void StatusTest() => isProperty<OrderLifecycleStatus>("Status");
        [TestMethod] public void ValidFromTest() => isNullableProperty<DateTime?>("Date created");
        [TestMethod] public void DateSentTest() => isNullableProperty<DateTime?>("Date sent");
        [TestMethod] public void DatePurchaseOrderReceivedTest() => isNullableProperty<DateTime?>("Purchase order received");
        [TestMethod] public void OrderManagerIdTest() => isNullableProperty<string>("Order manager");
        [TestMethod] public void SalesChannelIdTest() => isNullableProperty<string>("Sales channel");
        [TestMethod] public void DiscountRuleContextIdTest() => isNullableProperty<string>("Discount rule context");
        [TestMethod] public void PurchaseOrderIdTest() => isNullableProperty<string>("Purchase order");
    }
}
