using Abc.Data.Orders;
using Abc.Facade.Common;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class OrderStatusViewTests :SealedTests<OrderStatusView, EntityBaseView> {
        [TestMethod] public void OrderIdTest() => isNullableProperty<string>("Order");
        [TestMethod] public void OrderEventIdTest() => isNullableProperty<string>("Order event");
        [TestMethod] public void OrderStatusTest() => isProperty<OrderLifecycleStatus>("Order status");
    }
}