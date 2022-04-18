using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Abc.Infra.Common;
using Abc.Infra.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Orders {
    [TestClass]
    public class ManagersRepoTests :OrderReposTests<ManagersRepo, IManager,
        ManagerData> {

        protected override Type getBaseClass() => typeof(PagedRepo<IManager, ManagerData>);

        protected override ManagersRepo getObject(OrderDb c) => new(c);

        protected override DbSet<ManagerData> getSet(OrderDb c) => c.OrderManagers;
    }
}