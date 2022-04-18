using Abc.Aids.Random;
using Abc.Data.Inventory;
using Abc.Data.Parties;
using Abc.Data.Products;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Domain.Products;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using Abc.Facade.Inventory;
using Abc.Facade.Parties;
using Abc.Pages.Inventory;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Inventory {
    [TestClass]
    public class ReservationRequestsPageTests :SealedViewFactoryPageTests<ReservationRequestsPage,
        IReservationRequestsRepo, ReservationRequest, ReservationRequestView, ReservationRequestData,
        ReservationRequestViewFactory> {
        protected override string pageTitle => InventoryTitles.ReservationRequests;
        protected override string pageUrl => InventoryUrls.ReservationRequests;
        protected override ReservationRequest toObject(ReservationRequestData d) => new(d);

        private ReservationRequestData reservationRequestData;
        private InventoryData inventoryData;
        private PartySummaryData partySummaryData;
        private ProductTypeData productTypeData;
        private RuleContextData ruleContextData;
        private RuleOverrideData ruleOverrideData;

        private class repo :mockRepo<ReservationRequest, ReservationRequestData>, IReservationRequestsRepo {}
        private class inventoriesRepo :mockRepo<Abc.Domain.Inventory.Inventory, InventoryData>, IInventoriesRepo {}
        private class partySummariesRepo :mockRepo<IPartySummary, PartySummaryData>, IPartySummariesRepo {}
        private class productTypesRepo :mockRepo<IProductType, ProductTypeData>, IProductTypesRepo {}
        private class ruleContextsRepo :mockRepo<RuleContext, RuleContextData>, IRuleContextsRepo {
            public string GetRuleId(string id) => Get(id).RuleId;
        }
        private class ruleOverridesRepo :mockRepo<RuleOverride, RuleOverrideData>, IRuleOverridesRepo {}

        private repo reservationRequests;
        private inventoriesRepo inventories;
        private partySummariesRepo partySummaries;
        private productTypesRepo productTypes;
        private ruleContextsRepo ruleContexts;
        private ruleOverridesRepo ruleOverrides;

        protected override ReservationRequestsPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new ReservationRequestsPage(reservationRequests);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(reservationRequests
                , inventories, partySummaries, productTypes, ruleContexts, ruleOverrides);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void initializeRepos() {
            reservationRequests = new();
            inventories = new();
            partySummaries = new();
            productTypes = new();
            ruleContexts = new();
            ruleOverrides = new();
        }
        private void setRandomData() {
            reservationRequestData = random<ReservationRequestData>();
            inventoryData = random<InventoryData>();
            partySummaryData = random<PartySummaryData>();
            productTypeData = random<ProductTypeData>();
            ruleContextData = random<RuleContextData>();
            ruleOverrideData = random<RuleOverrideData>();
        }
        private void addRandomDataToRepos() {
            addRandomReservationRequests();
            addRandomInventories();
            addRandomProductTypes();
            addRandomPartySummaries();
            addRandomRuleContexts();
            addRandomRuleOverrides();
        }
        private void addRandomReservationRequests() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? reservationRequestData : random<ReservationRequestData>();
                reservationRequests.Add(new(d));
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
        private void addRandomPartySummaries() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? partySummaryData : random<PartySummaryData>();
                partySummaries.Add(PartySummaryFactory.Create(d));
            }
        }
        private void addRandomRuleContexts() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? ruleContextData : random<RuleContextData>();
                ruleContexts.Add(new(d));
            }
        }
        private void addRandomRuleOverrides() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? ruleOverrideData : random<RuleOverrideData>();
                ruleOverrides.Add(new(d));
            }
        }

        [TestMethod]
        public void InventoriesTest() {
            var list = inventories.Get();
            areEqual(list.Count + 1, obj.Inventories.Count());
        }

        [TestMethod]
        public void ProductTypesTest() {
            var list = productTypes.Get();
            areEqual(list.Count + 1, obj.ProductTypes.Count());
        }


        [TestMethod]
        public void PartySummariesTest() {
            var list = partySummaries.Get();
            areEqual(list.Count + 1, obj.PartySummaries.Count());
        }

        [TestMethod]
        public void RuleContextsTest() {
            var list = ruleContexts.Get();
            areEqual(list.Count + 1, obj.RuleContexts.Count());
        }

        [TestMethod]
        public void RuleOverridesTest() {
            var list = ruleOverrides.Get();
            areEqual(list.Count + 1, obj.RuleOverrides.Count());
        }

        [TestMethod]
        public void InventoryNameTest() {
            var name = obj.InventoryName(inventoryData.Id);
            areEqual(inventoryData.Name, name);
        }

        [TestMethod]
        public void ProductTypeNameTest() {
            var name = obj.ProductTypeName(productTypeData.Id);
            areEqual(productTypeData.Name, name);
        }

        [TestMethod]
        public void PartySummaryNameTest() {
            var name = obj.PartySummaryName(partySummaryData.Id);
            areEqual(partySummaryData.Name, name);
        }

        [TestMethod]
        public void RuleContextNameTest() {
            var name = obj.RuleContextName(ruleContextData.Id);
            areEqual(ruleContextData.Name, name);
        }

        [TestMethod]
        public void RuleOverrideNameTest() {
            var name = obj.RuleOverrideName(ruleOverrideData.Id);
            areEqual(ruleOverrideData.Name, name);
        }
    }
}
