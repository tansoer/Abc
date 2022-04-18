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
    public class OrderLineItemsRepoTests :OrderReposTests<OrderLineItemsRepo, IOrderLineItem,
        OrderLineItemData> {

        protected override Type getBaseClass() => typeof(PagedRepo<IOrderLineItem, OrderLineItemData>);

        protected override OrderLineItemsRepo getObject(OrderDb c) => new(c);

        protected override DbSet<OrderLineItemData> getSet(OrderDb c) => c.OrderLineItems;
    }
}