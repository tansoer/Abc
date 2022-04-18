using Abc.Data.Currencies;
using Abc.Facade.Common;
using Abc.Facade.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Currencies {
    [TestClass]
    public class PaymentMethodViewTests :SealedTests<PaymentMethodView, EntityBaseView> {
        [TestMethod] public void DailyLimitTest() => isProperty<decimal>("Daily Limit");
        [TestMethod] public void IssueOrBankIdNumberTest() => isNullableProperty<string>("Issue Or Bank Id Number");
        [TestMethod] public void BillingOrBankAddressTest() => isNullableProperty<string>("Billing Or Bank Address");
        [TestMethod] public void CardOrCheckNumberTest() => isNullableProperty<string>("Card Or Check Number");
        [TestMethod] public void BankNameTest() => isNullableProperty<string>("Bank Name");
        [TestMethod] public void CurrencyIdTest() => isNullableProperty<string>("Currency");
        [TestMethod] public void PayeeTest() => isNullableProperty<string>("Payee");
        [TestMethod] public void CreditLimitTest() => isProperty<decimal>("Credit Limit");
        [TestMethod] public void CardAssociationIdTest() => isNullableProperty<string>("Card Association");
        [TestMethod] public void PaymentMethodTypeTest() => isProperty<PaymentMethodType>("Payment Method Type");
    }
}
