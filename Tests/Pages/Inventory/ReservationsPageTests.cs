using Abc.Aids.Random;
using Abc.Data.Inventory;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Domain.Rules;
using Abc.Facade.Inventory;
using Abc.Pages.Inventory;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Inventory {
    [TestClass]
    public class ReservationsPageTests :SealedViewFactoryPageTests<ReservationsPage, IReservationsRepo,
        Reservation, ReservationView, ReservationData, ReservationViewFactory> {
        protected override string pageTitle => InventoryTitles.Reservations;
        protected override string pageUrl => InventoryUrls.Reservations;
        protected override Reservation toObject(ReservationData d) => new(d);

        private ReservationData reservationData;
        private InventoryData inventoryData;
        private ReservationRequestData reservationRequestData;
        private RuleSetData ruleSetData;

        private class repo :mockRepo<Reservation, ReservationData>, IReservationsRepo { }
        private class inventoriesRepo :mockRepo<Abc.Domain.Inventory.Inventory, InventoryData>, IInventoriesRepo { }
        private class reservationRequestsRepo :mockRepo<ReservationRequest, ReservationRequestData>, IReservationRequestsRepo { }
        private class ruleSetsRepo :mockRepo<IRuleSet, RuleSetData>, IRuleSetsRepo {}

        private repo reservations;
        private inventoriesRepo inventories;
        private reservationRequestsRepo requests;
        private ruleSetsRepo ruleSets;

        protected override ReservationsPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new ReservationsPage(reservations);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(reservations, inventories, requests, ruleSets);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void initializeRepos() {
            reservations = new();
            inventories = new();
            requests = new();
            ruleSets = new();
        }
        private void setRandomData() {
            reservationData = random<ReservationData>();
            inventoryData = random<InventoryData>();
            reservationRequestData = random<ReservationRequestData>();
            ruleSetData = random<RuleSetData>();
        }
        private void addRandomDataToRepos() {
            addRandomReservations();
            addRandomInventories();
            addRandomReservationRequests();
            addRandomRuleSets();
        }
        private void addRandomReservations() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? reservationData : random<ReservationData>();
                reservations.Add(new(d));
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
        private void addRandomReservationRequests() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? reservationRequestData : random<ReservationRequestData>();
                requests.Add(new(d));
            }
        }
        private void addRandomRuleSets() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? ruleSetData : random<RuleSetData>();
                ruleSets.Add(new RuleSet(d));
            }
        }

        [TestMethod]
        public void InventoriesTest() {
            var list = inventories.Get();
            areEqual(list.Count + 1, obj.Inventories.Count());
        }

        [TestMethod]
        public void ReservationRequestsTest() {
            var list = requests.Get();
            areEqual(list.Count + 1, obj.ReservationRequests.Count());
        }

        [TestMethod]
        public void RuleSetsTest() {
            var list = ruleSets.Get();
            areEqual(list.Count + 1, obj.RuleSets.Count());
        }

        [TestMethod]
        public void InventoryNameTest() {
            var name = obj.InventoryName(inventoryData.Id);
            areEqual(inventoryData.Name, name);
        }

        [TestMethod]
        public void ReservationRequestNameTest() {
            var name = obj.ReservationRequestName(reservationRequestData.Id);
            areEqual(reservationRequestData.Name, name);
        }

        [TestMethod]
        public void RuleSetNameTest() {
            var name = obj.RuleSetName(ruleSetData.Id);
            areEqual(ruleSetData.Name, name);
        }
    }
}
