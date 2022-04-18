using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Delivery {
    [TestClass]
    public class ErrorOrderLineItemTests :SealedTests<ErrorOrderLineItem, OrderLineItem> {

        protected override ErrorOrderLineItem createObject() => new(random<OrderLineItemData>());
    }
}