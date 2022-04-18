using Abc.Data.Inventory;
using Abc.Domain.Roles;
using System.Collections.Generic;

namespace Abc.Domain.Inventory {
    public sealed class ReservationRequest :InventoryItem<ReservationRequestData> {
        internal static string requestId => nameOf<ReservationReceiverData>(x => x.ReservationRequestId);
        public ReservationRequest() : this(null) { }
        public ReservationRequest(ReservationRequestData d) : base(d) { }
        public string RequesterId => get(Data?.RequesterPartySummaryId);
        public PartySummary Requester 
            => item<IPartySummariesRepo, IPartySummary>(RequesterId) as PartySummary;
        public IReadOnlyList<PartySummary> Receivers => list(receivers, x => x.Receiver);
        internal IReadOnlyList<ReservationReceiver> receivers
            => list<IReservationReceiversRepo, ReservationReceiver>(requestId, Id);
    }
}
