using Abc.Data.Orders;
using Abc.Domain.Orders.Discounts;
using Abc.Facade.Orders;
using Abc.Infra.Common;
using Abc.Infra.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Orders {

    [TestClass]
    public class DiscountsRepoTests :OrderReposTests<DiscountsRepo, IDiscount,
        DiscountData> {

        protected override Type getBaseClass() => typeof(PagedRepo<IDiscount, DiscountData>);

        protected override DiscountsRepo getObject(OrderDb c) => new(c);

        protected override DbSet<DiscountData> getSet(OrderDb c) => c.Discounts;
    }
}