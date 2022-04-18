using Abc.Data.Inventory;
using Abc.Infra.Common;
using Abc.Infra.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Inventory {
    [TestClass]
    public class InventoriesRepoTests :InventoryRepoTests<InventoriesRepo, Abc.Domain.Inventory.Inventory,
        InventoryData>{
        protected override Type getBaseClass() => typeof(PagedRepo<Abc.Domain.Inventory.Inventory, InventoryData>);
        protected override InventoriesRepo getObject(InventoryDb c) => new(c);
        protected override DbSet<InventoryData> getSet(InventoryDb c) => c.Inventories;
    }
}
