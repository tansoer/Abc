using Abc.Data.Orders;
using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Orders {
    public sealed class OrderEventView : EntityBaseView {
        [DisplayName("Order event type")] public OrderEventType OrderEventType { get; set; }
        [DisplayName("Order")] public string OrderId { get; set; }
        [DisplayName("Payment")] public string PaymentId { get; set; }
        [DisplayName("Product delivery")] public string ProductDeliveryId { get; set; }
        [DisplayName("Is processed")] public bool IsProcessed { get; set; }
        [DisplayName("Authorized party signature")] public string AuthorizedPartySignatureId { get; set; }
        [DisplayName("Old order line")] public string OldOrderLineId { get; set; }
        [DisplayName("Order line")] public string OrderLineId { get; set; }
        [DisplayName("Old party summary")] public string OldPartySummaryId { get; set; }
        [DisplayName("Party summary")] public string PartySummaryId { get; set; }
        [DisplayName("Old terms and conditions")] public string OldTermsAndConditionsId { get; set; }
        [DisplayName("Terms and conditions")] public string TermsAndConditionsId { get; set; }
        [DisplayName("Invoice")] public string InvoiceId { get; set; }
    }
}
