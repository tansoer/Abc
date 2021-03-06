using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Orders;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class OrderLineItemViewFactoryTests :ViewFactoryTests<OrderLineItemViewFactory,
        OrderLineItemData, IOrderLineItem, OrderLineItemView> {
    }
}
