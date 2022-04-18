using Abc.Data.Currencies;

namespace Abc.Domain.Currencies {
    public sealed class BankAccount :BaseBankAccount {
        public BankAccount() : this(null) { }

        public BankAccount(PaymentMethodData d) : base(d) { }

    }
}