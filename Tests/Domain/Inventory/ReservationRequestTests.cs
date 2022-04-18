
using Abc.Data.Inventory;
using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Inventory;
using Abc.Domain.Roles;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Inventory {
    [TestClass]
    public class ReservationRequestTests: SealedTests<ReservationRequest, 
        InventoryItem<ReservationRequestData>> {
        protected override ReservationRequest createObject() => new (random<ReservationRequestData>());
        [TestMethod] public void RequesterIdTest() => isReadOnly(obj.Data.RequesterPartySummaryId);
        [TestMethod] public async Task RequesterTest() {
            var d = random<PartySummaryData>();
            d.Id = obj.RequesterId;
            d.RoleInOrder = PartyRoleInOrder.Unspecified;
            await testItemAsync<PartySummaryData, IPartySummary, IPartySummariesRepo>(
                d, () => obj.Requester.Data, x => PartySummaryFactory.Create(x) as PartySummary);
            obj = createObject();
            isNotNull(obj.Requester);
            isNotNull(obj.Requester.Data);
        }
        [TestMethod] public async Task ReceiversTest() {
            isReadOnly();
            areEqual(0, obj.Receivers.Count);
            areEqual(0, obj.receivers.Count);
            for (int i = 0; i < 10; i++) {
                await addReceivers();
            }
            areNotEqual(0, obj.Receivers.Count);
            areNotEqual(0, obj.receivers.Count);
            areEqual(obj.receivers.Count, obj.Receivers.Count);
        }
        private async Task addReceivers() {
            var receiverData = random<ReservationReceiverData>();
            var summaryData = random<PartySummaryData>();
            summaryData.RoleInOrder = PartyRoleInOrder.Unspecified;
            receiverData.ReceiverPartySummaryId = summaryData.Id;
            receiverData.ReservationRequestId = obj.Id;
            await addAsync<IPartySummariesRepo, IPartySummary>(PartySummaryFactory.Create(summaryData));
            await addAsync<IReservationReceiversRepo, ReservationReceiver>(new ReservationReceiver(receiverData));
        }
    }
}
