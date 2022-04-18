using Abc.Data.Currencies;
using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Currencies {
    public sealed class PaymentMethodView :EntityBaseView{
        [DisplayName("Daily Limit")]
        public decimal DailyLimit { get; set; }
        [DisplayName("Issue Or Bank Id Number")]
        public string IssueOrBankIdNumber { get; set; }
        [DisplayName("Billing Or Bank Address")]
        public string BillingOrBankAddress { get; set; }
        [DisplayName("Card Or Check Number")]
        public string CardOrCheckNumber { get; set; }
        [DisplayName("Bank Name")]
        public string BankName { get; set; }
        [DisplayName("Currency")]
        public string CurrencyId { get; set; }
        [DisplayName("Payee")]
        public string Payee { get; set; }
        [DisplayName("Credit Limit")]
        public decimal CreditLimit { get; set; }
        [DisplayName("Card Association")]
        public string CardAssociationId { get; set; }
        [DisplayName("Payment Method Type")]
        public PaymentMethodType PaymentMethodType { get; set; }
    }
}
