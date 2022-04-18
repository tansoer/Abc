using Abc.Aids.Random;
using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Facade.Inventory;
using Abc.Pages.Inventory;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Inventory {
    [TestClass]
    public class InventoriesPageTests : SealedViewPageTests<InventoriesPage, 
        IInventoriesRepo, Abc.Domain.Inventory.Inventory, InventoryView, InventoryData> {
        protected override string pageTitle => InventoryTitles.Inventories;
        protected override string pageUrl => InventoryUrls.Inventories;
        protected override Abc.Domain.Inventory.Inventory toObject(InventoryData d) => new(d);
        private class testRepo :mockRepo<Abc.Domain.Inventory.Inventory, InventoryData>, IInventoriesRepo { }
        private testRepo repo;

        protected override InventoriesPage createObject() {
            repo = new();
            addRandomInventories();
            return new InventoriesPage(repo);
        }

        private void addRandomInventories() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                repo.Add(new (random<InventoryData>()));
        }

    }
}