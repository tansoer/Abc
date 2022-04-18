using Abc.Data.Orders;
using Abc.Domain.Orders.Statuses;
using Abc.Facade.Orders;
using Abc.Infra.Common;
using Abc.Infra.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Orders {
    [TestClass]
    public class OrderStatusesRepoTests :OrderReposTests<OrderStatusesRepo, IOrderStatus,
        OrderStatusData> {

        protected override Type getBaseClass() => typeof(PagedRepo<IOrderStatus, OrderStatusData>);

        protected override OrderStatusesRepo getObject(OrderDb c) => new(c);

        protected override DbSet<OrderStatusData> getSet(OrderDb c) => c.OrderStatuses;
    }
}