using Abc.Data.Inventory;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Domain.Roles;
using Abc.Facade.Inventory;
using Abc.Infra.Parties;
using Abc.Pages.Inventory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Inventory.Pages.ReservationReceivers {
    public abstract class ReservationReceiversTests :BaseInventoryTests<ReservationReceiverView, ReservationReceiverData> {

        private PartyDb partyDb;
        protected List<SelectListItem> reservationRequests => createSelectList(db.ReservationRequests);
        protected List<SelectListItem> partySummaries => createSelectList(partyDb.PartySummaries);
        protected override ReservationReceiverView toView(ReservationReceiverData d)
            => new ReservationReceiverViewFactory().Create(new ReservationReceiver(d));
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            partyDb = getContext<PartyDb>();
        }
        protected override string baseUrl() => InventoryUrls.ReservationReceivers;
        protected override void changeValuesExceptId(ReservationReceiverData d) {
            var id = d.Id;
            d = random<ReservationReceiverData>();
            d.Id = id;
        }
        protected override string getItemId(ReservationReceiverData d) => d.Id;
        protected override void setItemId(ReservationReceiverData d, string id) => d.Id = id;
        protected override void addRelatedItems(ReservationReceiverData d) {
            if (d is null) return;
            addReservationRequest(d.ReservationRequestId);
            addPartySummary(d.ReceiverPartySummaryId);
        }
        private void addPartySummary(string id) {
            var d = random<PartySummaryData>();
            d.Id = id;
            add<IPartySummariesRepo, IPartySummary>(PartySummaryFactory.Create(d));
        }
        protected override IEnumerable<Expression<Func<ReservationReceiverView, object>>> indexPageColumns =>
            new Expression<Func<ReservationReceiverView, object>>[] {
                x => x.ReservationRequestId,
                x => x.ReceiverPartySummaryId,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.ReservationRequestId, Unspecified.String);
            canView(item, m => m.ReceiverPartySummaryId, Unspecified.String);
            canView(item, m => m.Code);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canSelect(item, m => m.ReservationRequestId, reservationRequests);
            canSelect(item, m => m.ReceiverPartySummaryId, partySummaries);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canSelect(null, m => m.ReservationRequestId, reservationRequests);
            canSelect(null, m => m.ReceiverPartySummaryId, partySummaries);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            HiddenInputs.Clear();
        }
    }
    [TestClass] public class CreatePageTests :ReservationReceiversTests { }
    [TestClass] public class DeletePageTests :ReservationReceiversTests { }
    [TestClass] public class DetailsPageTests :ReservationReceiversTests { }
    [TestClass] public class EditPageTests :ReservationReceiversTests { }
    [TestClass] public class IndexPageTests :ReservationReceiversTests { }
}
