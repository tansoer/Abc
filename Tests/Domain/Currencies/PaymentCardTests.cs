using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Currencies {

    [TestClass]
    public class PaymentCardTests : AbstractTests<PaymentCard, PaymentMethod> {
        private class testClass : PaymentCard {
            public testClass(PaymentMethodData d = null) : base(d) { }
        }
        protected override PaymentCard createObject() => 
            new testClass(GetRandom.ObjectOf<PaymentMethodData>());
        [TestMethod] public void CardAssociationTest() {
            var r = GetRepo.Instance<ICardAssociationsRepo>();
            var d = GetRandom.ObjectOf<CardAssociationData>();
            d.Id = obj.CardAssociationId;
            r.Add(new CardAssociation(d));
            allPropertiesAreEqual(d, obj.CardAssociation.Data);
        }
        [TestMethod] public void CurrencyTest() {
            var r = GetRepo.Instance<ICurrencyRepo>();
            var d = GetRandom.ObjectOf<CurrencyData>();
            d.Id = obj.CurrencyId;
            r.Add(new Currency(d));
            allPropertiesAreEqual(d, obj.currency.Data);
        }
        [TestMethod] public void DailyLimitTest() {
            CurrencyTest();
            var d = obj.DailyLimit;
            Assert.AreEqual(obj.Data.DailyLimit, d.Amount);
            Assert.AreEqual(obj.currency.Id, d.Currency.Id);
            allPropertiesAreEqual(obj.currency.Data, d.Currency.Data);
        }
        [TestMethod] public void CurrencyIdTest() => isReadOnly(obj.Data.CurrencyId);
        [TestMethod] public void CardAssociationIdTest() => isReadOnly(obj.Data.CardAssociationId);
        [TestMethod] public void CardNumberTest() => isReadOnly(obj.Data.CardOrCheckNumber);
        [TestMethod] public void NameOnCardTest() => isReadOnly(obj.Data.Name);
        [TestMethod] public void ExpiringDateTest() => isReadOnly(obj.Data.ValidTo);
        [TestMethod] public void BillingAddressTest() => isReadOnly(obj.Data.BillingOrBankAddress);
        [TestMethod] public void ValidFromTest() => isReadOnly(obj.Data.ValidFrom);
        [TestMethod] public void VerificationCodeTest() => isReadOnly(obj.Data.Code);
        [TestMethod] public void IssueNumberTest() => isReadOnly(obj.Data.IssueOrBankIdNumber);
        [TestMethod] public void ValidToTest() => isReadOnly(obj.Data.ValidTo);
    }
}
