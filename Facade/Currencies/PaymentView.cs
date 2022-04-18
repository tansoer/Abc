using System;
using System.ComponentModel;
using Abc.Facade.Common;

namespace Abc.Facade.Currencies {
    public sealed class PaymentView : EntityBaseView {
        public decimal Amount { get; set; }
        [DisplayName("Currency")] public string CurrencyId { get; set; }
        [DisplayName("From payment method")] public string FromPaymentMethodId { get; set; }
        [DisplayName("Date received")] public DateTime? DateReceived { get; set; }
        [DisplayName("Date due")] public DateTime? DateDue { get; set; }
        [DisplayName("Order")] public string OrderId { get; set; }
        [DisplayName("Order event")] public string OrderEventId { get; set; }
        [DisplayName("To payment method")] public string ToPaymentMethodId { get; set; }
    }
}
