using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Facade.Common;

namespace Abc.Facade.Inventory {
    public sealed class ReservationReceiverViewFactory :AbstractViewFactory<ReservationReceiverData, 
        ReservationReceiver, ReservationReceiverView> {
        protected internal override ReservationReceiver toObject(ReservationReceiverData d) => new(d);
    }
}
