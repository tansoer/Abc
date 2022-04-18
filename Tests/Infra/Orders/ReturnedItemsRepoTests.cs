using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Facade.Orders;
using Abc.Infra.Common;
using Abc.Infra.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Orders {

    [TestClass]
    public class ReturnedItemsRepoTests :OrderReposTests<ReturnedItemsRepo, ReturnedItem,
        ReturnedItemData> {

        protected override Type getBaseClass() => typeof(EntityRepo<ReturnedItem, ReturnedItemData>);

        protected override ReturnedItemsRepo getObject(OrderDb c) => new (c);

        protected override DbSet<ReturnedItemData> getSet(OrderDb c) => c.ReturnedItems;
    }
}