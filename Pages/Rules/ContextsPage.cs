using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abc.Aids.Reflection;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Facade.Rules;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Rules {
    public sealed class ContextsPage : ViewPage<ContextsPage, IRuleContextsRepo, RuleContext, RuleContextView, RuleContextData> {
        protected override string title => RuleTitles.Contexts;
        public ContextsPage(IRuleContextsRepo c) : base(c) {}
        private IEnumerable<SelectListItem> rules;
        public IEnumerable<SelectListItem> Rules => rules ??= selectListByName<IRulesRepo, BaseRule, RuleData>();
        protected override void tableColumns() {
            tableColumn(x => x.Item.RuleId);
            tableColumn(x => x.Item.Name);
            tableColumn(x => x.Item.Code);
            tableColumn(x => x.Item.Details);
            tableColumn(x => x.Item.ValidFrom);
            tableColumn(x => x.Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.RuleId, () => RuleName(Item.RuleId));
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.RuleId, () => Rules);
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override RuleContext toObject(RuleContextView v) => 
            new RuleContextViewFactory().Create(v);
        protected internal override RuleContextView toView(RuleContext o) => 
            new RuleContextViewFactory().Create(o);
        protected internal override string pageUrl => RuleUrls.Contexts;
        public string RuleName(string id) => itemName(Rules, id);
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new RuleContextView {
                Id = Guid.NewGuid().ToString()
            };
        }
        public override async Task<IActionResult> OnGetDetailsAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var ruleId = db.GetRuleId(id);
            getRepo<IRuleElementsRepo>().CreateContextElements(id, ruleId);
            var name = GetMember.Name<RuleElementData>(x => x.RuleContextId);
            var page = RuleUrls.RuleElements;
            var index = GetMember.Name<RuleElementView>(x => x.Index);
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder={index}", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
    }
}
