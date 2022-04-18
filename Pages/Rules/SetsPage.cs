using System;
using System.Threading.Tasks;
using Abc.Aids.Reflection;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Facade.Rules;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc;

namespace Abc.Pages.Rules {

    public sealed class SetsPage: ViewPage<SetsPage, IRuleSetsRepo, IRuleSet, RuleSetView, RuleSetData> {
        protected override string title => RuleTitles.RuleSets;
        public SetsPage(IRuleSetsRepo s) : base(s) { }
        protected override void tableColumns() {
            tableColumn(x => x.Item.Code);
            tableColumn(x => x.Item.Name);
            tableColumn(x => x.Item.Details);
            tableColumn(x => x.Item.ValidFrom);
            tableColumn(x => x.Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => RuleUrls.RuleSets;
        protected internal override IRuleSet toObject(RuleSetView view) =>
            new RuleSetViewFactory().Create(view);
        protected internal override RuleSetView toView(IRuleSet obj) =>
            new RuleSetViewFactory().Create(obj);
        public override async Task<IActionResult> OnGetDetailsAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<RuleUsageData>(x => x.RuleSetId);
            var page = RuleUrls.Usages;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate)
            => Item = new();
    }
}

