using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abc.Aids.Extensions;
using Abc.Aids.Reflection;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Facade.Rules;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Abc.Pages.Rules {

    public sealed class UsagesPage: ViewPage<UsagesPage, IRuleUsagesRepo, RuleUsage, RuleUsageView, RuleUsageData> {
        internal IRuleUsagesRepo usages;
        protected override string title => RuleTitles.Usages;
        public UsagesPage(IRuleUsagesRepo u) :base(u) => usages = u;
        private IEnumerable<SelectListItem> rules;
        private IEnumerable<SelectListItem> sets;
        public IEnumerable<SelectListItem> Rules => rules ??= selectListByName<IRulesRepo, BaseRule, RuleData>();
        public IEnumerable<SelectListItem> Sets => sets ??= selectListByName<IRuleSetsRepo, IRuleSet, RuleSetData>();
        protected override void tableColumns() {
            tableColumn(x => x.Item.RuleSetId, () => RuleSetName(Item.RuleSetId));
            tableColumn(x => x.Item.RuleId, () => RuleName(Item.RuleId));
            tableColumn(x => x.Item.ValidFrom);
            tableColumn(x => x.Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Name);
            valueViewer(x => Item.RuleSetId, () => RuleSetName(Item.RuleSetId));
            valueViewer(x => Item.RuleId, () => RuleName(Item.RuleId));
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.RuleSetId, () => Sets);
            valueEditor(x => Item.RuleId, () => Rules);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => RuleUrls.Usages;
        protected internal override RuleUsage toObject(RuleUsageView v) => 
            new RuleUsageViewFactory().Create(v);
        protected internal override RuleUsageView toView(RuleUsage o) => 
            new RuleUsageViewFactory().Create(o);
        public string RuleName(string id) => itemName(Rules, id);
        public string RuleSetName(string id) => itemName(Sets, id);
        protected internal override string subtitle => $"{RuleSetName(FixedValue)}";
        public override async Task<IActionResult> OnGetDetailsAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<RuleData>(x => x.Id);
            var page = RuleUrls.Rules;
            var usage = await usages.GetAsync(id);
            var ruleId = usage.RuleId;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={ruleId}", UriKind.Relative);
            await Task.CompletedTask;
            RulesPage.ruleSetId = id.GetHead();
            return Redirect(url.ToString());
        }
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate)
            => Item = new();
    }
}


