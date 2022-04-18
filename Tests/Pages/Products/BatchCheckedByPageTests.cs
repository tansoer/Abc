using Abc.Aids.Random;
using Abc.Aids.Reflection;
using Abc.Data.Parties;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Products;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Products {
    [TestClass]
    public class BatchCheckedByPageTests :SealedViewFactoryPageTests<
        BatchCheckedByPage, IBatchCheckedByRepo, BatchCheckedBy, BatchCheckedByView, BatchCheckedByData, BatchCheckedByViewFactory> {
        protected override string pageTitle => ProductTitles.BatchCheckedBy;
        protected override string pageUrl => ProductUrls.BatchCheckedBy;
        protected override BatchCheckedBy toObject(BatchCheckedByData d) => new(d);

        private class batchCheckedByRepo :mockRepo<BatchCheckedBy, BatchCheckedByData>, IBatchCheckedByRepo { };
        private batchCheckedByRepo batchCheckedBy;
        private BatchCheckedByData batchCheckedByData;

        private class bacthesRepo :mockRepo<Batch, BatchData>, IBatchesRepo { };
        private bacthesRepo batches;
        private BatchData batchData;

        private class partySignaturesRepo :mockRepo<PartySignature, PartySignatureData>, IPartySignaturesRepo { };
        private partySignaturesRepo partySignatures;
        private PartySignatureData partySignatureData;

        protected override BatchCheckedByPage createObject() {
            batchCheckedBy = new ();
            batches = new();
            partySignatures = new();
            batchCheckedByData = GetRandom.ObjectOf<BatchCheckedByData>();
            batchData = GetRandom.ObjectOf<BatchData>();
            partySignatureData = GetRandom.ObjectOf<PartySignatureData>();
            addRandomBatchCheckedByInfo();
            addRandomBatches();
            addRandomPartySignatures();
            return new BatchCheckedByPage(batchCheckedBy);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(batchCheckedBy, batches, partySignatures);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void addRandomBatchCheckedByInfo() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? batchCheckedByData : GetRandom.ObjectOf<BatchCheckedByData>();
                batchCheckedBy.Add(new BatchCheckedBy(d));
            }
        }

        private void addRandomBatches() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? batchData : GetRandom.ObjectOf<BatchData>();
                batches.Add(new(d));
            }
        }

        private void addRandomPartySignatures() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? partySignatureData : GetRandom.ObjectOf<PartySignatureData>();
                partySignatures.Add(new(d));
            }
        }

        [TestMethod]
        public void BatchesTest() {
            var list = batches.Get();
            Assert.AreEqual(list.Count + 1, obj.Batches.Count());
        }

        [TestMethod]
        public void PartySignaturesTest() {
            var list = partySignatures.Get();
            Assert.AreEqual(list.Count + 1, obj.PartySignatures.Count());
        }

        [TestMethod]
        public override void ToObjectTest() {
            var view = GetRandom.ObjectOf<BatchCheckedByView>();
            var o = obj.toObject(view);
            allPropertiesAreEqual(view, o.Data, GetMember.Name<BatchCheckedByView>(x => x.Address),
                GetMember.Name<BatchCheckedByView>(x => x.CheckedBy),
                GetMember.Name<BatchCheckedByView>(x => x.EmailAddress),
                GetMember.Name<BatchCheckedByView>(x => x.PhoneNumber));
        }

        [TestMethod]
        public override void ToViewTest() {
            var d = GetRandom.ObjectOf<BatchCheckedByData>();
            var view = obj.toView(toObject(d));
            allPropertiesAreEqual(view, d, GetMember.Name<BatchCheckedByView>(x => x.Address),
                GetMember.Name<BatchCheckedByView>(x => x.CheckedBy),
                GetMember.Name<BatchCheckedByView>(x => x.EmailAddress),
                GetMember.Name<BatchCheckedByView>(x => x.PhoneNumber));
        }

        [TestMethod]
        public void BatchNameTest() {
            var name = obj.BatchName(batchData.Id);
            Assert.AreEqual(batchData.Name, name);
        }

        [TestMethod]
        public void PartySignatureNameTest() {
            var name = obj.PartySignatureName(partySignatureData.Id);
            Assert.AreEqual(partySignatureData.Name, name);
        }
    }
}
