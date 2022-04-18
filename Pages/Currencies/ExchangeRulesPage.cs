using Abc.Data.Currencies;
using Abc.Data.Rules;
using Abc.Domain.Currencies;
using Abc.Domain.Rules;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Currencies {
    public sealed class ExchangeRulesPage : ViewPage<ExchangeRulesPage, IExchangeRulesRepo,
        ExchangeRule, ExchangeRuleView, ExchangeRuleData> {
        protected override string title => MoneyTitles.ExchangeRules;
        public ExchangeRulesPage(IExchangeRulesRepo r) :base(r) {
}
        private IEnumerable<SelectListItem> rateTypes;
        public IEnumerable<SelectListItem> RateTypes => rateTypes ??= selectListByName<IRateTypesRepo, RateType, RateTypeData>();
        private IEnumerable<SelectListItem> ruleSets;
        public IEnumerable<SelectListItem> RuleSets => ruleSets ??= selectListByName<IRuleSetsRepo, IRuleSet, RuleSetData>();
        protected override void tableColumns() {
            tableColumn(x => Item.RateTypeId, () => RateTypeName(Item.RateTypeId));
            tableColumn(x => Item.RuleSetId, () => RuleSetName(Item.RuleSetId));
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.RateTypeId, () => RateTypeName(Item.RateTypeId));
            valueViewer(x => Item.RuleSetId, () => RuleSetName(Item.RuleSetId));
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.RateTypeId, () => RateTypes);
            valueEditor(x => Item.RuleSetId, () => RuleSets);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => MoneyUrls.ExchangeRules;
        protected internal override ExchangeRule toObject(ExchangeRuleView v) => new ExchangeRuleViewFactory().Create(v);
        protected internal override ExchangeRuleView toView(ExchangeRule o) => new ExchangeRuleViewFactory().Create(o);
        public string RateTypeName(string id) => itemName(RateTypes, id);
        public string RuleSetName(string id) => itemName(RuleSets, id);
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate)
            => Item = new();
    }
}