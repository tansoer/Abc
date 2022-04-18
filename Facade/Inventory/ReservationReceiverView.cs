using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Inventory {
    public sealed class ReservationReceiverView :EntityBaseView {
        [DisplayName("Receiver Party Summary")]
        public string ReceiverPartySummaryId { get; set; }
        [DisplayName("Reservation Request")]
        public string ReservationRequestId { get; set; }
    }
}
