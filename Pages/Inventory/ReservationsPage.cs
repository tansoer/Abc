using Abc.Data.Inventory;
using Abc.Data.Rules;
using Abc.Domain.Inventory;
using Abc.Domain.Rules;
using Abc.Facade.Inventory;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Inventory {
    public sealed class ReservationsPage :ViewFactoryPage<ReservationsPage, IReservationsRepo, Reservation,
        ReservationView, ReservationData, ReservationViewFactory> {
        protected override string title => InventoryTitles.Reservations;
        public ReservationsPage(IReservationsRepo r) :base(r) {}
        private IEnumerable<SelectListItem> inventories;
        public IEnumerable<SelectListItem> Inventories => inventories ??= selectListByName<IInventoriesRepo, Domain.Inventory.Inventory, InventoryData>();
        private IEnumerable<SelectListItem> reservationRequests;
        public IEnumerable<SelectListItem> ReservationRequests => reservationRequests ??= selectListByName<IReservationRequestsRepo, ReservationRequest, ReservationRequestData>();
        private IEnumerable<SelectListItem> ruleSets;
        public IEnumerable<SelectListItem> RuleSets => ruleSets ??= selectListByName<IRuleSetsRepo, IRuleSet, RuleSetData>();
        protected override void tableColumns() {
            tableColumn(x => Item.ReservationRequestId, () => ReservationRequestName(Item.ReservationRequestId));
            tableColumn(x => Item.InventoryId, () => InventoryName(Item.InventoryId));
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.ReservationRequestId, () => ReservationRequestName(Item.ReservationRequestId));
            valueViewer(x => Item.InventoryId, () => InventoryName(Item.InventoryId));
            valueViewer(x => Item.CancellationPolicyRuleSetId, () => RuleSetName(Item.CancellationPolicyRuleSetId));
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.ReservationRequestId, () => ReservationRequests);
            valueEditor(x => Item.InventoryId, () => Inventories);
            valueEditor(x => Item.CancellationPolicyRuleSetId, () => RuleSets);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => InventoryUrls.Reservations;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate)
            => Item = new();
        public string InventoryName(string id) => itemName(Inventories, id);
        public string ReservationRequestName(string id) => itemName(ReservationRequests, id);
        public string RuleSetName(string id) => itemName(RuleSets, id);
    }
}
