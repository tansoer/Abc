namespace Abc.Data.Inventory {
    public sealed class ReservationRequestData: InventoryItemData {
        public string RequesterPartySummaryId { get; set; }
        public uint NumberRequested { get; set; }
        public string ProductTypeId { get; set; }
        public string RuleContextId { get; set; }
        public string RuleOverridesId { get; set; }
    }
}
