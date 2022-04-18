using Abc.Data.Inventory;
using Abc.Data.Parties;
using Abc.Data.Products;
using Abc.Data.Rules;
using Abc.Domain.Inventory;
using Abc.Domain.Products;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using Abc.Facade.Inventory;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Inventory {
    public sealed class ReservationRequestsPage :ViewFactoryPage<ReservationRequestsPage, IReservationRequestsRepo,
        ReservationRequest, ReservationRequestView, ReservationRequestData, ReservationRequestViewFactory> {
        protected override string title => InventoryTitles.ReservationRequests;
        public ReservationRequestsPage(IReservationRequestsRepo r) :base(r) {}
        private IEnumerable<SelectListItem> inventories;
        public IEnumerable<SelectListItem> Inventories => inventories ??= selectListByName<IInventoriesRepo, Domain.Inventory.Inventory, InventoryData>();
        private IEnumerable<SelectListItem> productTypes;
        public IEnumerable<SelectListItem> ProductTypes => productTypes ??= selectListByName<IProductTypesRepo, IProductType, ProductTypeData>();
        private IEnumerable<SelectListItem> partySummaries;
        public IEnumerable<SelectListItem> PartySummaries => partySummaries ??= selectListByName<IPartySummariesRepo, IPartySummary, PartySummaryData>();
        private IEnumerable<SelectListItem> ruleContexts;
        public IEnumerable<SelectListItem> RuleContexts => ruleContexts ??= selectListByName<IRuleContextsRepo, RuleContext, RuleContextData>();
        private IEnumerable<SelectListItem> ruleOverrides;
        public IEnumerable<SelectListItem> RuleOverrides => ruleOverrides ??= selectListByName<IRuleOverridesRepo, RuleOverride, RuleOverrideData>();
        protected override void tableColumns() {
            tableColumn(x => Item.RequesterPartySummaryId, () => PartySummaryName(Item.RequesterPartySummaryId));
            tableColumn(x => Item.InventoryId, () => InventoryName(Item.InventoryId));
            tableColumn(x => Item.ProductTypeId, () => ProductTypeName(Item.ProductTypeId));
            tableColumn(x => Item.NumberRequested);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.RequesterPartySummaryId, () => PartySummaryName(Item.RequesterPartySummaryId));
            valueViewer(x => Item.InventoryId, () => InventoryName(Item.InventoryId));
            valueViewer(x => Item.ProductTypeId, () => ProductTypeName(Item.ProductTypeId));
            valueViewer(x => Item.NumberRequested);
            valueViewer(x => Item.RuleContextId, () => RuleContextName(Item.RuleContextId));
            valueViewer(x => Item.RuleOverridesId, () => RuleOverrideName(Item.RuleOverridesId));
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.RequesterPartySummaryId, () => PartySummaries);
            valueEditor(x => Item.InventoryId, () => Inventories);
            valueEditor(x => Item.ProductTypeId, () => ProductTypes);
            valueEditor(x => Item.NumberRequested);
            valueEditor(x => Item.RuleContextId, () => RuleContexts);
            valueEditor(x => Item.RuleOverridesId, () => RuleOverrides);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => InventoryUrls.ReservationRequests;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate)
            => Item = new();
        public string InventoryName(string id) => itemName(Inventories, id);
        public string ProductTypeName(string id) => itemName(ProductTypes, id);
        public string PartySummaryName(string id) => itemName(PartySummaries, id);
        public string RuleContextName(string id) => itemName(RuleContexts, id);
        public string RuleOverrideName(string id) => itemName(RuleOverrides, id);
    }
}
