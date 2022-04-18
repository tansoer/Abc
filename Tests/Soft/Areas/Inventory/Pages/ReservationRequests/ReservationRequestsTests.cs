using Abc.Data.Inventory;
using Abc.Data.Parties;
using Abc.Data.Products;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Domain.Products;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using Abc.Facade.Inventory;
using Abc.Infra.Parties;
using Abc.Infra.Products;
using Abc.Infra.Rules;
using Abc.Pages.Inventory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Inventory.Pages.ReservationRequests {
    public abstract class ReservationRequestsTests :BaseInventoryTests<ReservationRequestView, ReservationRequestData> {
        private PartyDb partyDb;
        private ProductDb productDb;
        private RuleDb ruleDb;
        protected List<SelectListItem> inventories => createSelectList(db.Inventories);
        protected List<SelectListItem> productTypes => createSelectList(productDb.ProductTypes);
        protected List<SelectListItem> partySummaries => createSelectList(partyDb.PartySummaries);
        protected List<SelectListItem> ruleContexts => createSelectList(ruleDb.RuleContexts);
        protected List<SelectListItem> ruleOverrides => createSelectList(ruleDb.RuleOverrides);
        protected override ReservationRequestView toView(ReservationRequestData d)
            => new ReservationRequestViewFactory().Create(new ReservationRequest(d));
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            partyDb = getContext<PartyDb>();
            productDb = getContext<ProductDb>();
            ruleDb = getContext<RuleDb>();
        }
        protected override string baseUrl() => InventoryUrls.ReservationRequests;
        protected override void changeValuesExceptId(ReservationRequestData d) {
            var id = d.Id;
            d = random<ReservationRequestData>();
            d.Id = id;
        }
        protected override string getItemId(ReservationRequestData d) => d.Id;
        protected override void setItemId(ReservationRequestData d, string id) => d.Id = id;
        protected override void addRelatedItems(ReservationRequestData d) {
            if (d is null) return;
            addInventory(d.InventoryId);
            addProductType(d.ProductTypeId);
            addPartySummary(d.RequesterPartySummaryId);
            addRuleContext(d.RuleContextId);
            addRuleOverride(d.RuleOverridesId);
        }
        private void addProductType(string id) {
            var d = random<ProductTypeData>();
            d.Id = id;
            add<IProductTypesRepo, IProductType>(ProductTypeFactory.Create(d));
        }
        private void addPartySummary(string id) {
            var d = random<PartySummaryData>();
            d.Id = id;
            add<IPartySummariesRepo, IPartySummary>(PartySummaryFactory.Create(d));
        }
        private void addRuleContext(string id) {
            var d = random<RuleContextData>();
            d.Id = id;
            add<IRuleContextsRepo, RuleContext>(new(d));
        }
        private void addRuleOverride(string id) {
            var d = random<RuleOverrideData>();
            d.Id = id;
            add<IRuleOverridesRepo, RuleOverride>(new(d));
        }
        protected override IEnumerable<Expression<Func<ReservationRequestView, object>>> indexPageColumns =>
            new Expression<Func<ReservationRequestView, object>>[] {
                x => x.RequesterPartySummaryId,
                x => x.InventoryId,
                x => x.ProductTypeId,
                x => x.NumberRequested,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.RequesterPartySummaryId, Unspecified.String);
            canView(item, m => m.InventoryId, Unspecified.String);
            canView(item, m => m.ProductTypeId, Unspecified.String);
            canView(item, m => m.NumberRequested);
            canView(item, m => m.RuleContextId, Unspecified.String);
            canView(item, m => m.RuleOverridesId, Unspecified.String);
            canView(item, m => m.Code);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canSelect(item, m => m.RequesterPartySummaryId, partySummaries);
            canSelect(item, m => m.InventoryId, inventories);
            canSelect(item, m => m.ProductTypeId, productTypes);
            canEdit(item, m => m.NumberRequested, true, false, 0);
            canSelect(item, m => m.RuleContextId, ruleContexts);
            canSelect(item, m => m.RuleOverridesId, ruleOverrides);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canSelect(null, m => m.RequesterPartySummaryId, partySummaries);
            canSelect(null, m => m.InventoryId, inventories);
            canSelect(null, m => m.ProductTypeId, productTypes);
            canEdit(null, m => m.NumberRequested, true, false, 0);
            canSelect(null, m => m.RuleContextId, ruleContexts);
            canSelect(null, m => m.RuleOverridesId, ruleOverrides);
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
    [TestClass] public class CreatePageTests :ReservationRequestsTests { }
    [TestClass] public class DeletePageTests :ReservationRequestsTests { }
    [TestClass] public class DetailsPageTests :ReservationRequestsTests { }
    [TestClass] public class EditPageTests :ReservationRequestsTests { }
    [TestClass] public class IndexPageTests :ReservationRequestsTests { }
}
