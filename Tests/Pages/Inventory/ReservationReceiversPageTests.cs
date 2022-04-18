using Abc.Aids.Random;
using Abc.Data.Inventory;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Domain.Roles;
using Abc.Facade.Inventory;
using Abc.Facade.Parties;
using Abc.Pages.Inventory;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Inventory {
    [TestClass]
    public class ReservationReceiversPageTests :SealedViewFactoryPageTests<ReservationReceiversPage,
        IReservationReceiversRepo, ReservationReceiver, ReservationReceiverView, ReservationReceiverData,
        ReservationReceiverViewFactory> {
        protected override string pageTitle => InventoryTitles.ReservationReceivers;
        protected override string pageUrl => InventoryUrls.ReservationReceivers;
        protected override ReservationReceiver toObject(ReservationReceiverData d) => new(d);

        private ReservationReceiverData reservationReceiverData;
        private ReservationRequestData reservationRequestData;
        private PartySummaryData partySummaryData;

        private class repo :mockRepo<ReservationReceiver, ReservationReceiverData>, IReservationReceiversRepo {}
        private class reservationRequestsRepo :mockRepo<ReservationRequest, ReservationRequestData>, IReservationRequestsRepo {}
        private class partySummariesRepo :mockRepo<IPartySummary, PartySummaryData>, IPartySummariesRepo {}

        private repo receivers;
        private reservationRequestsRepo requests;
        private partySummariesRepo partySummaries;

        protected override ReservationReceiversPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new ReservationReceiversPage(receivers);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(receivers, requests, partySummaries);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void initializeRepos() {
            receivers = new();
            requests = new();
            partySummaries = new();
        }
        private void setRandomData() {
            reservationReceiverData = random<ReservationReceiverData>();
            reservationRequestData = random<ReservationRequestData>();
            partySummaryData = random<PartySummaryData>();
        }
        private void addRandomDataToRepos() {
            addRandomReservationReceivers();
            addRandomReservationRequests();
            addRandomPartySummaries();
        }
        private void addRandomReservationReceivers() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? reservationReceiverData : random<ReservationReceiverData>();
                receivers.Add(new(d));
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
        private void addRandomPartySummaries() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? partySummaryData : random<PartySummaryData>();
                partySummaries.Add(PartySummaryFactory.Create(d));
            }
        }

        [TestMethod]
        public void ReservationRequestsTest() {
            var list = requests.Get();
            areEqual(list.Count + 1, obj.ReservationRequests.Count());
        }

        [TestMethod]
        public void PartySummariesTest() {
            var list = partySummaries.Get();
            areEqual(list.Count + 1, obj.PartySummaries.Count());
        }

        [TestMethod]
        public void ReservationRequestNameTest() {
            var name = obj.ReservationRequestName(reservationRequestData.Id);
            areEqual(reservationRequestData.Name, name);
        }

        [TestMethod]
        public void PartySummaryNameTest() {
            var name = obj.PartySummaryName(partySummaryData.Id);
            areEqual(partySummaryData.Name, name);
        }
    }
}
