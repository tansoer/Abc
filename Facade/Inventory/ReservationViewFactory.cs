using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Facade.Common;

namespace Abc.Facade.Inventory {
    public sealed class ReservationViewFactory :AbstractViewFactory<ReservationData, Reservation, ReservationView> {
        protected internal override Reservation toObject(ReservationData d) => new(d);
    }
}
