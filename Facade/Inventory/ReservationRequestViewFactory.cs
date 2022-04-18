using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Facade.Common;

namespace Abc.Facade.Inventory {
    public sealed class ReservationRequestViewFactory :AbstractViewFactory<ReservationRequestData, 
        ReservationRequest, ReservationRequestView> {
        protected internal override ReservationRequest toObject(ReservationRequestData d) => new(d);
    }
}
