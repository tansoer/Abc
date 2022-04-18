using Abc.Data.Inventory;
using Abc.Data.Parties;
using Abc.Domain.Inventory;
using Abc.Domain.Roles;
using Abc.Facade.Inventory;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Inventory {
    public sealed class ReservationReceiversPage :ViewFactoryPage<ReservationReceiversPage, IReservationReceiversRepo,
        ReservationReceiver, ReservationReceiverView, ReservationReceiverData, ReservationReceiverViewFactory> {
        protected override string title => InventoryTitles.ReservationReceivers;
        public ReservationReceiversPage(IReservationReceiversRepo r) :base(r) {}
        private IEnumerable<SelectListItem> partySummaries;
        public IEnumerable<SelectListItem> PartySummaries => partySummaries ??= selectListByName<IPartySummariesRepo, IPartySummary, PartySummaryData>();
        private IEnumerable<SelectListItem> reservationRequests;
        public IEnumerable<SelectListItem> ReservationRequests 
            => reservationRequests ??= selectListByName<IReservationRequestsRepo, ReservationRequest, ReservationRequestData>();
        protected override void tableColumns() {
            tableColumn(x => Item.ReservationRequestId, () => ReservationRequestName(Item.ReservationRequestId));
            tableColumn(x => Item.ReceiverPartySummaryId, () => PartySummaryName(Item.ReceiverPartySummaryId));
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.ReservationRequestId, () => ReservationRequestName(Item.ReservationRequestId));
            valueViewer(x => Item.ReceiverPartySummaryId, () => ReservationRequestName(Item.ReceiverPartySummaryId));
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.ReservationRequestId, () => ReservationRequests);
            valueEditor(x => Item.ReceiverPartySummaryId, () => PartySummaries);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => InventoryUrls.ReservationReceivers;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate)
            => Item = new();
        public string PartySummaryName(string id) => itemName(PartySummaries, id);
        public string ReservationRequestName(string id) => itemName(ReservationRequests, id);
    }
}
