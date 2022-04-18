using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Abc.Pages.Currencies;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Currencies {
    [TestClass] public class PaymentMethodsPageTests :SealedViewsPageTests<
        PaymentMethodsPage, IPaymentMethodsRepo, PaymentMethod, PaymentMethodView, PaymentMethodData, PaymentMethodType> {

        protected override string pageUrl => MoneyUrls.PaymentMethods;
        protected override string pageTitle => MoneyTitles.PaymentMethods;
        protected override PaymentMethod toObject(PaymentMethodData d) => PaymentMethodFactory.Create(d);

        private PaymentMethodData Data;
        private CurrencyData currencyData;
        private CardAssociationData cardAssociationData;

        private class testRepo :mockRepo<PaymentMethod, PaymentMethodData>, IPaymentMethodsRepo { }
        private class currenciesRepo :mockRepo<Currency, CurrencyData>, ICurrencyRepo { }
        private class cardAssociationsRepo :mockRepo<CardAssociation, CardAssociationData>, ICardAssociationsRepo { }

        private testRepo repo;
        private currenciesRepo currencies;
        private cardAssociationsRepo cards;

        protected override PaymentMethodsPage createObject() {
            repo = new ();
            currencies = new();
            cards = new ();

            Data = random<PaymentMethodData>();
            currencyData = random<CurrencyData>();
            cardAssociationData = random<CardAssociationData>();

            addRandomPaymentMethods();
            addRandomCurrencies();
            addRandomCardAssociations();

            return new PaymentMethodsPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, currencies, cards);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void addRandomPaymentMethods() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? Data : GetRandom.ObjectOf<PaymentMethodData>();
                repo.Add(PaymentMethodFactory.Create(d));
            }
        }
        private void addRandomCurrencies() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? currencyData : GetRandom.ObjectOf<CurrencyData>();
                currencies.Add(new(d));
            }
        }
        private void addRandomCardAssociations() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? cardAssociationData : GetRandom.ObjectOf<CardAssociationData>();
                cards.Add(new(d));
            }
        }

        [TestMethod]
        public void CurrenciesTest() {
            var list = currencies.Get();
            Assert.AreEqual(list.Count + 1, obj.Currencies.Count());
        }

        [TestMethod]
        public void CardAssociationsTest() {
            var list = cards.Get();
            Assert.AreEqual(list.Count + 1, obj.CardAssociations.Count());
        }

        [TestMethod]
        public void CurrencyNameTest() {
            var name = obj.CurrencyName(currencyData.Id);
            Assert.AreEqual(currencyData.Name, name);
        }

        [TestMethod]
        public void CardAssociationNameTest() {
            var name = obj.CardAssociationName(cardAssociationData.Id);
            Assert.AreEqual(cardAssociationData.Name, name);
        }
    }
}
