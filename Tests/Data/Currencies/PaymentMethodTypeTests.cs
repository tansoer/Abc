using Abc.Aids.Reflection;
using Abc.Data.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Currencies {

    [TestClass]
    public class PaymentMethodTypeTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(PaymentMethodType);

        [TestMethod]
        public void CountTest()
            => Assert.AreEqual(7, GetEnum.Count<PaymentMethodType>());

        [TestMethod]
        public void UnspecifiedTest() =>
            Assert.AreEqual(9, (int) PaymentMethodType.Unspecified);

        [TestMethod]
        public void CreditCardTest() =>
            Assert.AreEqual(3, (int) PaymentMethodType.CreditCard);

        [TestMethod]
        public void DebitCardTest() =>
            Assert.AreEqual(2, (int) PaymentMethodType.DebitCard);

        [TestMethod]
        public void CashTest() =>
            Assert.AreEqual(0, (int) PaymentMethodType.Cash);

        [TestMethod]
        public void CheckTest() =>
            Assert.AreEqual(1, (int) PaymentMethodType.Check);

        [TestMethod]
        public void LoyaltyCardTest() =>
            Assert.AreEqual(4, (int) PaymentMethodType.LoyaltyCard);

        [TestMethod]
        public void BankAccountTest() =>
            Assert.AreEqual(5, (int)PaymentMethodType.BankAccount);
    }
}