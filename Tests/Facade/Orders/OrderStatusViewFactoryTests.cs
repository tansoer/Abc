using Abc.Data.Orders;
using Abc.Domain.Orders.Statuses;
using Abc.Facade.Common;
using Abc.Facade.Orders;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class OrderStatusViewFactoryTests :ViewFactoryTests<OrderStatusViewFactory,
        OrderStatusData, IOrderStatus, OrderStatusView> {
    }
}
