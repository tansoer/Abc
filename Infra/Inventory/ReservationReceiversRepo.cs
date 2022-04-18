using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Infra.Common;

namespace Abc.Infra.Inventory {
    public sealed class ReservationReceiversRepo:
        EntityRepo<ReservationReceiver, ReservationReceiverData>, IReservationReceiversRepo{
        public ReservationReceiversRepo(InventoryDb c) : base(c, c?.ReservationReceivers) { }
    }
}
