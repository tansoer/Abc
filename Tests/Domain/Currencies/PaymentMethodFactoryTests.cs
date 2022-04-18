using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Domain.Currencies {

    [TestClass] public class PaymentMethodFactoryTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(PaymentMethodFactory);

        [TestMethod] public void CreateTest() => isInstanceOfType<Cash>(PaymentMethodFactory.Create());

        [DataTestMethod]
        [DataRow(PaymentMethodType.Check, typeof(Check))]
        [DataRow(PaymentMethodType.CreditCard, typeof(CreditCard))]
        [DataRow(PaymentMethodType.DebitCard, typeof(DebitCard))]
        [DataRow(PaymentMethodType.Cash, typeof(Cash))]
        [DataRow(PaymentMethodType.BankAccount, typeof(BankAccount))]
        [DataRow(PaymentMethodType.LoyaltyCard, typeof(LoyalityCard))]
        public void Create(PaymentMethodType t, Type type) =>
            isInstanceOfType(PaymentMethodFactory.Create(
                new PaymentMethodData { PaymentMethodType = t }), type);
    }
}