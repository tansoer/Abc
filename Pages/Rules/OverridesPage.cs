using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Facade.Rules;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Rules {

    public sealed class OverridesPage : ViewPage<OverridesPage, IRuleOverridesRepo, 
        RuleOverride, RuleOverrideView, RuleOverrideData> {
        protected override string title => RuleTitles.Overrides;
        public OverridesPage(IRuleOverridesRepo r) :base(r) {}
        private IEnumerable<SelectListItem> ruleSets;
        private IEnumerable<SelectListItem> rules;
        private IEnumerable<SelectListItem> ruleContexts;
        public IEnumerable<SelectListItem> RuleSets => ruleSets ??= selectListByName<IRuleSetsRepo, IRuleSet, RuleSetData>();
        public IEnumerable<SelectListItem> Rules => rules ??= selectListByName<IRulesRepo, BaseRule, RuleData>();
        public IEnumerable<SelectListItem> RuleContexts => ruleContexts ??= selectListByName<IRuleContextsRepo, RuleContext, RuleContextData>();
        protected override void tableColumns() {
            tableColumn(x => x.Item.Name);
            tableColumn(x => x.Item.RuleSetId, () => RuleSetName(Item.RuleSetId));
            tableColumn(x => x.Item.RuleId, () => RuleName(Item.RuleId));
            tableColumn(x => x.Item.RuleContextId, () => RuleContextName(Item.RuleContextId));
            tableColumn(x => x.Item.Code);
            tableColumn(x => x.Item.Details);
            tableColumn(x => x.Item.ValidFrom);
            tableColumn(x => x.Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Name);
            valueViewer(x => Item.RuleSetId, () => RuleSetName(Item.RuleSetId));
            valueViewer(x => Item.RuleId, () => RuleName(Item.RuleId));
            valueViewer(x => Item.RuleContextId, () => RuleContextName(Item.RuleContextId));
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.RuleSetId, () => RuleSets);
            valueEditor(x => Item.RuleId, () => Rules);
            valueEditor(x => Item.RuleContextId, () => RuleContexts);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override RuleOverride toObject(RuleOverrideView v) =>
            new RuleOverrideViewFactory().Create(v);
        protected internal override RuleOverrideView toView(RuleOverride o) =>
            new RuleOverrideViewFactory().Create(o);
        protected internal override string pageUrl => RuleUrls.Overrides;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate)
            => Item = new();
        public string RuleSetName(string id) => itemName(RuleSets, id);
        public string RuleName(string id) => itemName(Rules, id);
        public string RuleContextName(string id) => itemName(RuleContexts, id);
    }
}
