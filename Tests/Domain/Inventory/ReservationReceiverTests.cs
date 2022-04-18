using Abc.Data.Inventory;
using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Domain.Roles;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Inventory {
    [TestClass]
    public class ReservationReceiverTests: SealedTests<ReservationReceiver, Entity<ReservationReceiverData>> {
        protected override ReservationReceiver createObject() => new (random<ReservationReceiverData>());
        [TestMethod] public void ReceiverIdTest() => isReadOnly(obj.Data.ReceiverPartySummaryId);
        [TestMethod] public void RequestIdTest() => isReadOnly(obj.Data.ReservationRequestId);
        [TestMethod] public async Task ReceiverTest() {
            var d = random<PartySummaryData>();
            d.Id = obj.ReceiverId;
            d.RoleInOrder = PartyRoleInOrder.Unspecified;
            await testItemAsync<PartySummaryData, IPartySummary, IPartySummariesRepo>(
                d, () => obj.Receiver.Data, x => PartySummaryFactory.Create(x) as PartySummary);
            obj = createObject();
            isNotNull(obj.Receiver);
            isNotNull(obj.Receiver.Data);
        }
    }
}
