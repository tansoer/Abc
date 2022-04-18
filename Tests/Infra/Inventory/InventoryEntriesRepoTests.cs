using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Infra.Common;
using Abc.Infra.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Inventory {
    [TestClass]
    public class InventoryEntriesRepoTests :InventoryRepoTests<InventoryEntriesRepo, InventoryEntry,
        InventoryEntryData> {

        protected override Type getBaseClass() => typeof(PagedRepo<InventoryEntry, InventoryEntryData>);

        protected override InventoryEntriesRepo getObject(InventoryDb c) => new (c);

        protected override DbSet<InventoryEntryData> getSet(InventoryDb c) => c.InventoryEntries;
    }
}