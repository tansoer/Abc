namespace Abc.Data.Inventory {
    public sealed class ReservationData: InventoryItemData {
        public string ReservationRequestId { get; set; }
        public string CancellationPolicyRuleSetId { get; set; }
    }
}
