using System.ComponentModel;

namespace Abc.Facade.Inventory {
    public sealed class ReservationView :InventoryItemView {
        [DisplayName("Reservation Request")]
        public string ReservationRequestId { get; set; }
        [DisplayName("Cancellation Policy Rule Set")]
        public string CancellationPolicyRuleSetId { get; set; }
    }
}
