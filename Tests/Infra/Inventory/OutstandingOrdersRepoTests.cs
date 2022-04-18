using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Infra.Common;
using Abc.Infra.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Inventory {

    [TestClass]
    public class OutstandingOrdersRepoTests :InventoryRepoTests<OutstandingOrdersRepo, OutstandingOrder,
        OutstandingOrderData> {

        protected override Type getBaseClass() => typeof(PagedRepo<OutstandingOrder, OutstandingOrderData>);

        protected override OutstandingOrdersRepo getObject(InventoryDb c) => new (c);

        protected override DbSet<OutstandingOrderData> getSet(InventoryDb c) => c.OutstandingOrders;

    }
}