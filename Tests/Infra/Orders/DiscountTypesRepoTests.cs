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
    public class DiscountTypesRepoTests :OrderReposTests<DiscountTypesRepo, IDiscountType, DiscountTypeData> {
        protected override Type getBaseClass() => typeof(PagedRepo<IDiscountType, DiscountTypeData>);
        protected override DiscountTypesRepo getObject(OrderDb c) => new(c);
        protected override DbSet<DiscountTypeData> getSet(OrderDb c) => c.DiscountTypes;
    }
}