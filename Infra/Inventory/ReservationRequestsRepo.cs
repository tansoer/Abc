using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Infra.Common;

namespace Abc.Infra.Inventory {
    public sealed class ReservationRequestsRepo
        :PagedRepo<ReservationRequest, ReservationRequestData>, IReservationRequestsRepo {
        public ReservationRequestsRepo(InventoryDb c = null) : base(c, c?.ReservationRequests) { }
        protected internal override ReservationRequest toDomainObject(ReservationRequestData d)
            => new ReservationRequest(d);
    }
}