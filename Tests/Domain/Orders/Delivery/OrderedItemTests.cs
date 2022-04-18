using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Delivery {
    [TestClass]
    public class OrderedItemTests :SealedTests<OrderedItem, OrderLineItem> {

        protected override OrderedItem createObject() => new(random<OrderLineItemData>());
    }
}