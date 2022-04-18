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
    public class DeliveryTypesRepoTests :OrderReposTests<DeliveryTypesRepo, DeliveryType,
        DeliveryTypeData> {

        protected override Type getBaseClass() => typeof(PagedRepo<DeliveryType, DeliveryTypeData>);

        protected override DeliveryTypesRepo getObject(OrderDb c) => new DeliveryTypesRepo(c);

        protected override DbSet<DeliveryTypeData> getSet(OrderDb c) => c.DeliveryTypes;
    }
}