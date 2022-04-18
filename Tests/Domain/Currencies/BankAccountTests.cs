using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;

namespace Abc.Tests.Domain.Currencies {
    public class BankAccountTests :SealedTests<BankAccount, BaseBankAccount> {

        protected override BankAccount createObject()
            => new(GetRandom.ObjectOf<PaymentMethodData>());
    }

}
