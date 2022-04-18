
using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Inventory {
    [TestClass]
    public class ReservationTests: SealedTests<Reservation, InventoryItem<ReservationData>> {
        protected override Reservation createObject() => new (random<ReservationData>());
        [TestMethod] public void ReservationRequestIdTest() => isReadOnly(obj.Data.ReservationRequestId);
        [TestMethod] public async Task RequestTest() {
            await testItemAsync<ReservationRequestData, ReservationRequest, IReservationRequestsRepo>(
                obj.ReservationRequestId,
                () => obj.Request.Data,
                d => new ReservationRequest(d));
            obj = createObject();
            isNotNull(obj.Request);
            isNotNull(obj.Request.Data);
        }
    }
}
