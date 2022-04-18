using Abc.Data.Common;

namespace Abc.Data.Inventory {
    public sealed class ReservationReceiverData :EntityBaseData {
        public string ReceiverPartySummaryId { get; set; }
        public string ReservationRequestId { get; set; }
    }
}
