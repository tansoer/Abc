using Abc.Data.Inventory;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Domain.Rules;
using Abc.Facade.Inventory;
using Abc.Infra.Rules;
using Abc.Pages.Inventory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Inventory.Pages.Reservations {
    public abstract class ReservationsTests :BaseInventoryTests<ReservationView, ReservationData> {
        private RuleDb ruleDb;
        protected List<SelectListItem> inventories => createSelectList(db.Inventories);
        protected List<SelectListItem> reservationRequests => createSelectList(db.ReservationRequests);
        protected List<SelectListItem> ruleSets => createSelectList(ruleDb.RuleSets);
        protected override ReservationView toView(ReservationData d)
            => new ReservationViewFactory().Create(new Reservation(d));
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            ruleDb = getContext<RuleDb>();
        }
        protected override string baseUrl() => InventoryUrls.Reservations;
        protected override void changeValuesExceptId(ReservationData d) {
            var id = d.Id;
            d = random<ReservationData>();
            d.Id = id;
        }
        protected override string getItemId(ReservationData d) => d.Id;
        protected override void setItemId(ReservationData d, string id) => d.Id = id;
        protected override void addRelatedItems(ReservationData d) {
            if (d is null) return;
            addInventory(d.InventoryId);
            addReservationRequest(d.ReservationRequestId);
            addRuleSet(d.CancellationPolicyRuleSetId);
        }
        private void addRuleSet(string id) {
            var d = random<RuleSetData>();
            d.Id = id;
            add<IRuleSetsRepo, IRuleSet>(new RuleSet(d));
        }
        protected override IEnumerable<Expression<Func<ReservationView, object>>> indexPageColumns =>
            new Expression<Func<ReservationView, object>>[] {
                x => x.ReservationRequestId,
                x => x.InventoryId,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.ReservationRequestId, Unspecified.String);
            canView(item, m => m.InventoryId, Unspecified.String);
            canView(item, m => m.CancellationPolicyRuleSetId, Unspecified.String);
            canView(item, m => m.Code);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canSelect(item, m => m.ReservationRequestId, reservationRequests);
            canSelect(item, m => m.InventoryId, inventories);
            canSelect(item, m => m.CancellationPolicyRuleSetId, ruleSets);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canSelect(null, m => m.ReservationRequestId, reservationRequests);
            canSelect(null, m => m.InventoryId, inventories);
            canSelect(null, m => m.CancellationPolicyRuleSetId, ruleSets);
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
    [TestClass] public class CreatePageTests :ReservationsTests { }
    [TestClass] public class DeletePageTests :ReservationsTests { }
    [TestClass] public class DetailsPageTests :ReservationsTests { }
    [TestClass] public class EditPageTests :ReservationsTests { }
    [TestClass] public class IndexPageTests :ReservationsTests { }
}
