using Abc.Data.Currencies;

namespace Abc.Domain.Currencies {
    public abstract class BaseBankAccount :PaymentMethod {

        protected BaseBankAccount() : base(null) { }
        protected BaseBankAccount(PaymentMethodData d) : base(d) { }

        public string AccountName => get(Data?.Name);
        public string AccountNumber => get(Data?.Code);
        public string BankName => get(Data?.BankName);
        public string BankAddress => get(Data?.BillingOrBankAddress);
        public string BankIdNumber => get(Data?.IssueOrBankIdNumber);

    }
}