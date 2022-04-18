using Abc.Data.Common;
using Abc.Data.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Currencies {

    [TestClass]
    public class PaymentMethodDataTests : SealedTests<PaymentMethodData, EntityBaseData> {

        [TestMethod] public void DailyLimitTest() => isProperty<decimal>();

        [TestMethod] public void CreditLimitTest() => isProperty<decimal>();

        [TestMethod] public void IssueOrBankIdNumberTest() => isNullable<string>();

        [TestMethod] public void BillingOrBankAddressTest() => isNullable<string>();

        [TestMethod] public void CardOrCheckNumberTest() => isNullable<string>();

        [TestMethod] public void CardAssociationIdTest() => isNullable<string>();

        [TestMethod] public void BankNameTest() => isNullable<string>();

        [TestMethod] public void PayeeTest() => isNullable<string>();

        [TestMethod] public void CurrencyIdTest() => isNullable<string>();

        [TestMethod] public void PaymentMethodTypeTest() => isProperty<PaymentMethodType>();
    }

}