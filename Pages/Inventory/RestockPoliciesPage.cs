using Abc.Data.Inventory;
using Abc.Data.Rules;
using Abc.Domain.Inventory;
using Abc.Domain.Rules;
using Abc.Facade.Inventory;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Inventory {
    public sealed class RestockPoliciesPage :ViewFactoryPage<RestockPoliciesPage, IRestockPoliciesRepo,
        RestockPolicy, RestockPolicyView, RestockPolicyData, RestockPolicyViewFactory> {
        protected override string title => InventoryTitles.RestockPolicies;
        public RestockPoliciesPage(IRestockPoliciesRepo r) :base(r) {}
        private IEnumerable<SelectListItem> ruleSets;
        public IEnumerable<SelectListItem> RuleSets => ruleSets ??= selectListByName<IRuleSetsRepo, IRuleSet, RuleSetData>();
        private IEnumerable<SelectListItem> ruleContexts;
        public IEnumerable<SelectListItem> RuleContexts => ruleContexts ??= selectListByName<IRuleContextsRepo, RuleContext, RuleContextData>();
        protected override void tableColumns() {
            tableColumn(x => Item.RestockRuleSetId, () => RuleSetName(Item.RestockRuleSetId));
            tableColumn(x => Item.RestockRuleContextId, () => RuleContextName(Item.RestockRuleContextId));
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.RestockRuleSetId, () => RuleSetName(Item.RestockRuleSetId));
            valueViewer(x => Item.RestockRuleContextId, () => RuleContextName(Item.RestockRuleContextId));
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.RestockRuleSetId, () => RuleSets);
            valueEditor(x => Item.RestockRuleContextId, () => RuleContexts);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => InventoryUrls.RestockPolicies;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate)
            => Item = new();
        public string RuleSetName(string id) => itemName(RuleSets, id);
        public string RuleContextName(string id) => itemName(RuleContexts, id);
    }
}
