using Abc.Data.Common;

namespace Abc.Data.Orders {
    public sealed class OrderEventData: EntityBaseData {
        public OrderEventType OrderEventType { get; set; }
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public string ProductDeliveryId { get; set; }
        public bool IsProcessed { get; set; }
        public string AuthorizedPartySignatureId { get; set; }
        public string OldOrderLineId { get; set; }
        public string OrderLineId { get; set; }
        public string OldPartySummaryId { get; set; }
        public string PartySummaryId { get; set; }
        public string OldTermsAndConditionsId { get; set; }
        public string TermsAndConditionsId { get; set; }
        public string InvoiceId { get; set; }
    }
}
