using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Currencies {
    [TestClass]
    public class BaseBankAccountTests :AbstractTests<BaseBankAccount, PaymentMethod> {

        private class testClass: BaseBankAccount {
            public testClass(PaymentMethodData d = null) : base(d) { }
        }
        protected override BaseBankAccount createObject()
            => new testClass(GetRandom.ObjectOf<PaymentMethodData>());

        [TestMethod] public void AccountNameTest() => isReadOnly(obj.Data.Name);

        [TestMethod] public void AccountNumberTest() => isReadOnly(obj.Data.Code);

        [TestMethod] public void BankNameTest() => isReadOnly(obj.Data.BankName);

        [TestMethod] public void BankAddressTest() => isReadOnly(obj.Data.BillingOrBankAddress);

        [TestMethod] public void BankIdNumberTest() => isReadOnly(obj.Data.IssueOrBankIdNumber);
    }

}
