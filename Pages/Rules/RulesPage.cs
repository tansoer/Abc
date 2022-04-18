using Abc.Aids.Methods;
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
    public sealed class RulesPage : ViewsPage<RulesPage, IRulesRepo, BaseRule, RuleView, RuleData, RuleKind> {
        protected override string title => RuleTitles.Rules;
        internal static string ruleSetId;
        public RulesPage(IRulesRepo r) : base(r) { }
        private IEnumerable<SelectListItem> rules;
        public IEnumerable<SelectListItem> Rules
            => rules ??= selectListByName<IRulesRepo, BaseRule, RuleData>( x => x.Data.RuleKind == RuleKind.Rule);
        public RuleKind RuleType { get; internal set; }
        protected override void tableColumns() {
            tableColumn(x => x.Item.RuleKind);
            tableColumn(x => x.Item.Name);
            tableColumn(x => x.Item.Code);
            tableColumn(x => x.Item.Details);
            tableColumn(x => x.Item.ValidFrom);
            tableColumn(x => x.Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.RuleKind);
            valueViewer(x => Item.RuleId, () => RuleName(Item.RuleId));
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            if(Item.RuleKind == RuleKind.ActivityRule) valueEditor(x => x.Item.RuleId, () => Rules);
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => RuleUrls.Rules;
        protected internal override BaseRule toObject(RuleView v) => new RuleViewFactory().Create(v);
        protected internal override RuleView toView(BaseRule o) => new RuleViewFactory().Create(o);
        public string RuleName(string id) => itemName(Rules, id);
        public override async Task<IActionResult> OnGetDetailsAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<RuleElementData>(x => x.RuleId);
            var page = RuleUrls.RuleElements;
            var index = GetMember.Name<RuleElementView>(x => x.Index);
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder={index}", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
        protected internal override string subtitle => $"{RuleName(FixedValue)}";
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new RuleView {
                RuleKind = Safe.Run(() => (RuleKind)(switchOfCreate ?? 1000), RuleKind.Unspecified)
            };
        }
    }
}

