using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Infra.Common;
using Abc.Infra.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Inventory {

    [TestClass]
    public class CapacityManagersRepoTests :InventoryRepoTests<CapacityManagersRepo, CapacityManager,
        CapacityManagerData> {

        protected override Type getBaseClass() => typeof(EntityRepo<CapacityManager, CapacityManagerData>);

        protected override CapacityManagersRepo getObject(InventoryDb c) => new (c);

        protected override DbSet<CapacityManagerData> getSet(InventoryDb c) => c.CapacityManagers;
    }
}