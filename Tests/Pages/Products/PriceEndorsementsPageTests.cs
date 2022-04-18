using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Products.Price;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Products {
    [TestClass]
    public class PriceEndorsementsPageTests :SealedViewPageTests<PriceEndorsementsPage, IPriceEndorsementsRepo, 
        PriceEndorsement, PriceEndorsementView, PriceEndorsementData> {
        protected override string pageTitle => ProductTitles.PriceEndorsements;
        protected override string pageUrl => ProductUrls.PriceEndorsements;
        protected override PriceEndorsement toObject(PriceEndorsementData d) => new(d);

        private PriceEndorsementData data;
        private PriceData priceData;
        private PartySignatureData signatureData;
        private class Repo : mockRepo<PriceEndorsement, PriceEndorsementData>, IPriceEndorsementsRepo {}
        private class PricesRepo : mockRepo<IPrice, PriceData>, IPricesRepo {}
        private class SignaturesRepo : mockRepo<PartySignature, PartySignatureData>, IPartySignaturesRepo {}
        private Repo repo;
        private PricesRepo prices;
        private SignaturesRepo partySignatures;

        protected override PriceEndorsementsPage createObject() {
            repo = new();
            prices = new();
            partySignatures = new();
            data = GetRandom.ObjectOf<PriceEndorsementData>();
            priceData = GetRandom.ObjectOf<PriceData>();
            signatureData = GetRandom.ObjectOf<PartySignatureData>();
            addRandomPriceEndorsements();
            addRandomPrices();
            addRandomPartySignatures();
            return new PriceEndorsementsPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, prices, partySignatures);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void addRandomPriceEndorsements() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<PriceEndorsementData>();
                repo.Add(new(d));
            }
        }

        private void addRandomPrices() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? priceData : GetRandom.ObjectOf<PriceData>();
                prices.Add(PriceFactory.Create(d));
            }
        }

        private void addRandomPartySignatures() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? signatureData : GetRandom.ObjectOf<PartySignatureData>();
                partySignatures.Add(new(d));
            }
        }

        [TestMethod]
        public void PricesTest() {
            var list = prices.Get();
            Assert.AreEqual(list.Count + 1, obj.Prices.Count());
        }

        [TestMethod]
        public void PartySignaturesTest() {
            var list = partySignatures.Get();
            Assert.AreEqual(list.Count + 1, obj.PartySignatures.Count());
        }

        [TestMethod]
        public void OnGetCreateTest() {
            var page = obj.OnGetCreate(sortOrder, searchString, pageIndex, fixedFilter, fixedValue, createSwitch);
            Assert.IsInstanceOfType(page, typeof(PageResult));
            testPageProperties();
        }

        [TestMethod]
        public void ValueForTest() {
            var mock = new mockHtmlHelper<PriceEndorsementsPage>();
            Assert.IsNull(obj.ValueFor(mock, 0));
            Assert.IsNull(obj.ValueFor(mock, 1));
        }

        [TestMethod]
        public void PriceNameTest() {
            var name = obj.PriceName(priceData.Id);
            Assert.AreEqual(priceData.Name, name);
        }

        [TestMethod]
        public void PartySignatureNameTest() {
            var name = obj.PartySignatureName(signatureData.Id);
            Assert.AreEqual(signatureData.Name, name);
        }

    }
}
