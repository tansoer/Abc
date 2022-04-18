using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Orders;
using Abc.Infra.Common;
using Abc.Infra.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Orders {

    [TestClass]
    public class OrderLinesRepoTests :OrderReposTests<OrderLinesRepo, IOrderLine,
        OrderLineData> {

        protected override Type getBaseClass() => typeof(PagedRepo<IOrderLine, OrderLineData>);

        protected override OrderLinesRepo getObject(OrderDb c) => new OrderLinesRepo(c);

        protected override DbSet<OrderLineData> getSet(OrderDb c) => c.OrderLines;
    }
}