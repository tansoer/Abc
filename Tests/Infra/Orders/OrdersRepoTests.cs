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
    public class OrdersRepoTests :OrderReposTests<OrdersRepo, IOrder,
        OrderData> {

        protected override Type getBaseClass() => typeof(PagedRepo<IOrder, OrderData>);

        protected override OrdersRepo getObject(OrderDb c) => new (c);

        protected override DbSet<OrderData> getSet(OrderDb c) => c.Orders;
    }
}