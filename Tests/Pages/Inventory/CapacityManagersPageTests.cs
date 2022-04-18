using Abc.Aids.Random;
using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Facade.Inventory;
using Abc.Pages.Inventory;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Inventory {
    [TestClass]
    public class CapacityManagersPageTests :SealedViewFactoryPageTests<CapacityManagersPage,
        ICapacityManagersRepo, CapacityManager, CapacityManagerView, CapacityManagerData, 
        CapacityManagerViewFactory> {
        protected override string pageTitle => InventoryTitles.CapacityManagers;
        protected override string pageUrl => InventoryUrls.CapacityManagers;
        protected override CapacityManager toObject(CapacityManagerData d) => new(d);

        private class repo :mockRepo<CapacityManager, CapacityManagerData>, ICapacityManagersRepo {}
        private repo capacityManagers;

        protected override CapacityManagersPage createObject() {
            capacityManagers = new();
            addRandomInventories();
            return new CapacityManagersPage(capacityManagers);
        }

        private void addRandomInventories() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                capacityManagers.Add(new(random<CapacityManagerData>()));
        }
    }
}
