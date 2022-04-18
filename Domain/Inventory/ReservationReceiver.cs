using Abc.Data.Inventory;
using Abc.Domain.Common;
using Abc.Domain.Roles;

namespace Abc.Domain.Inventory {
    public sealed class ReservationReceiver :Entity<ReservationReceiverData> {
        public ReservationReceiver() : this(null) { }
        public ReservationReceiver(ReservationReceiverData d) : base(d) { }
        public string ReceiverId => get(Data?.ReceiverPartySummaryId);
        public string RequestId => get(Data?.ReservationRequestId);
        public PartySummary Receiver => 
            item<IPartySummariesRepo, IPartySummary>(ReceiverId) as PartySummary;
    }
}
