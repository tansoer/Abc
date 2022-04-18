using Abc.Data.Common;
using Abc.Data.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Data.Orders {

    [TestClass]
    public class OrderDataTests :SealedTests<OrderData, EntityBaseData> {
        [TestMethod] public void OrderManagerIdTest() => isNullable<string>();
        [TestMethod] public void OrderTypeTest() => isProperty<OrderType>();
        [TestMethod] public void DiscountRuleContextIdTest() => isNullable<string>();
        [TestMethod] public void DateSentReceivedTest() => isNullable<DateTime?>();
        [TestMethod] public void SalesChannelIdTest() => isNullable<string>();
        [TestMethod] public void PurchaseOrderIdTest() => isNullable<string>();
        [TestMethod] public void StatusTest() => isProperty<OrderLifecycleStatus>();
    }
}
