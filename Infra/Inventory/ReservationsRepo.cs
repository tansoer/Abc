using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Infra.Common;

namespace Abc.Infra.Inventory {
    public sealed class ReservationsRepo
        :EntityRepo<Reservation, ReservationData>, IReservationsRepo {
        public ReservationsRepo(InventoryDb c = null) : base(c, c?.Reservations) { }
    }
}