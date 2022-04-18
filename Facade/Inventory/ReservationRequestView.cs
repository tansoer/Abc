using System.ComponentModel;

namespace Abc.Facade.Inventory {
    public sealed class ReservationRequestView :InventoryItemView {
        [DisplayName("Requester Party Summary")]
        public string RequesterPartySummaryId { get; set; }
        [DisplayName("Number Requested")]
        public uint NumberRequested { get; set; }
        [DisplayName("Product Type")]
        public string ProductTypeId { get; set; }
        [DisplayName("Rule Context")]
        public string RuleContextId { get; set; }
        [DisplayName("Rule Overrides")]
        public string RuleOverridesId { get; set; }
    }
}
