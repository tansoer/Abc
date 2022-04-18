using Abc.Data.Inventory;

namespace Abc.Domain.Inventory {
    public sealed class Reservation:InventoryItem<ReservationData> {
        public Reservation(): this(null) { }
        public Reservation(ReservationData d) : base(d) { }
        public string ReservationRequestId => get(Data?.ReservationRequestId);
        public ReservationRequest Request
            => item<IReservationRequestsRepo, ReservationRequest>(ReservationRequestId);
    }
}
