using Abc.Aids.Random;
using Abc.Data.Inventory;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Domain.Products;
using Abc.Facade.Inventory;
using Abc.Pages.Inventory;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Inventory {
    [TestClass]
    public class InventoryEntriesPageTests :SealedViewsPageTests<InventoryEntriesPage, IInventoryEntriesRepo,
        InventoryEntry, InventoryEntryView, InventoryEntryData, ProductKind> {
        protected override string pageTitle => InventoryTitles.InventoryEntries;
        protected override string pageUrl => InventoryUrls.InventoryEntries;
        protected override InventoryEntry toObject(InventoryEntryData d) => InventoryEntryFactory.Create(d);

        private InventoryEntryData data;
        private InventoryData inventoryData;
        private ProductTypeData productTypeData;
        private CapacityManagerData capacityManagerData;
        private RestockPolicyData restockPolicyData;

        private class repo :mockRepo<InventoryEntry, InventoryEntryData>, IInventoryEntriesRepo {}
        private class inventoriesRepo :mockRepo<Abc.Domain.Inventory.Inventory, InventoryData>, IInventoriesRepo {}
        private class productTypesRepo :mockRepo<IProductType, ProductTypeData>, IProductTypesRepo {}
        private class capacityManagersRepo :mockRepo<CapacityManager, CapacityManagerData>, ICapacityManagersRepo {}
        private class restockPoliciesRepo :mockRepo<RestockPolicy, RestockPolicyData>, IRestockPoliciesRepo {}

        private repo inventoryEntries;
        private inventoriesRepo inventories;
        private productTypesRepo productTypes;
        private capacityManagersRepo capacityManagers;
        private restockPoliciesRepo restockPolicies;

        protected override InventoryEntriesPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new(inventoryEntries);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(inventoryEntries, inventories
                , productTypes, capacityManagers, restockPolicies);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            inventoryEntries = new();
            inventories = new();
            productTypes = new();
            capacityManagers = new();
            restockPolicies = new();
        }
        private void setRandomData() {
            data = random<InventoryEntryData>();
            inventoryData = random<InventoryData>();
            productTypeData = random<ProductTypeData>();
            capacityManagerData = random<CapacityManagerData>();
            restockPolicyData = random<RestockPolicyData>();
        }
        private void addRandomDataToRepos() {
            addRandomInventoryEntries();
            addRandomInventories();
            addRandomProductTypes();
            addRandomCapacityManagers();
            addRandomRestockPolicies();
        }
        private void addRandomInventoryEntries() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : random<InventoryEntryData>();
                inventoryEntries.Add(InventoryEntryFactory.Create(d));
            }
        }
        private void addRandomInventories() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? inventoryData : random<InventoryData>();
                inventories.Add(new(d));
            }
        }
        private void addRandomProductTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? productTypeData : random<ProductTypeData>();
                productTypes.Add(ProductTypeFactory.Create(d));
            }
        }
        private void addRandomCapacityManagers() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? capacityManagerData : random<CapacityManagerData>();
                capacityManagers.Add(new(d));
            }
        }
        private void addRandomRestockPolicies() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? restockPolicyData : GetRandom.ObjectOf<RestockPolicyData>();
                restockPolicies.Add(new(d));
            }
        }

        [TestMethod] public void InventoriesTest() {
            var list = inventories.Get();
            areEqual(list.Count + 1, obj.Inventories.Count());
        }

        [TestMethod] public void ProductTypesTest() {
            var list = productTypes.Get();
            areEqual(list.Count + 1, obj.ProductTypes.Count());
        }

        [TestMethod] public void CapacityManagersTest() {
            var list = capacityManagers.Get();
            areEqual(list.Count + 1, obj.CapacityManagers.Count());
        }

        [TestMethod] public void RestockPoliciesTest() {
            var list = restockPolicies.Get();
            areEqual(list.Count + 1, obj.RestockPolicies.Count());
        }

        [TestMethod] public void InventoryNameTest() {
            var name = obj.InventoryName(inventoryData.Id);
            areEqual(inventoryData.Name, name);
        }

        [TestMethod] public void ProductTypeNameTest() {
            var name = obj.ProductTypeName(productTypeData.Id);
            areEqual(productTypeData.Name, name);
        }

        [TestMethod] public void CapacityManagerNameTest() {
            var name = obj.CapacityManagerName(capacityManagerData.Id);
            areEqual(capacityManagerData.Name, name);
        }

        [TestMethod] public void RestockPolicyNameTest() {
            var name = obj.RestockPolicyName(restockPolicyData.Id);
            areEqual(restockPolicyData.Name, name);
        }
    }
}
