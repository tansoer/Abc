using System;
using Abc.Data.Common;

namespace Abc.Data.Currencies {
    public sealed class PaymentData : EntityBaseData {
        public decimal Amount { get; set; }
        public string CurrencyId { get; set; }
        public string FromPaymentMethodId { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? DateDue { get; set; }
        public string OrderId { get; set; }
        public string OrderEventId { get; set; }
        public string ToPaymentMethodId { get; set; }
    }
}
