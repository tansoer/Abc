using Abc.Data.Inventory;
using Abc.Facade.Inventory;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Inventory {
    [TestClass]
    public class InventoryViewFactoryTests :ViewFactoryTests<InventoryViewFactory, 
        InventoryData, Abc.Domain.Inventory.Inventory, InventoryView> {
    }
}
