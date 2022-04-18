using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Facade.Orders;
using Abc.Infra.Common;
using Abc.Infra.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Abc.Tests.Infra.Orders {
    [TestClass]
    public class OrderEventsRepoTests :OrderReposTests<OrderEventsRepo, IOrderEvent,
        OrderEventData> {

        protected override Type getBaseClass() => typeof(PagedRepo<IOrderEvent, OrderEventData>);

        protected override OrderEventsRepo getObject(OrderDb c) => new (c);

        protected override DbSet<OrderEventData> getSet(OrderDb c) => c.OrderEvents;
    }
}