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
    public class ProductDeliveriesRepoTests :OrderReposTests<ProductDeliveriesRepo, ProductDelivery,
        ProductDeliveryData> {

        protected override Type getBaseClass() => typeof(PagedRepo<ProductDelivery, ProductDeliveryData>);

        protected override ProductDeliveriesRepo getObject(OrderDb c) => new (c);

        protected override DbSet<ProductDeliveryData> getSet(OrderDb c) => c.ProductDeliveries;
    }
}