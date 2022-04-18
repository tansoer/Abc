using Abc.Data.Common;

namespace Abc.Data.Currencies {

    public sealed class PaymentMethodData : EntityBaseData {

        public decimal DailyLimit { get; set; }

        public string IssueOrBankIdNumber { get; set; }

        public string BillingOrBankAddress { get; set; }

        public string CardOrCheckNumber { get; set; }

        public string BankName { get; set; }

        public string CurrencyId { get; set; }

        public string Payee { get; set; }

        public decimal CreditLimit { get; set; }

        public string CardAssociationId { get; set; }

        public PaymentMethodType PaymentMethodType { get; set; }
    }
}