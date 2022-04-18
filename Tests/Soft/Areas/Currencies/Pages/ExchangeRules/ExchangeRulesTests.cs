using Abc.Data.Currencies;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Rules;
using Abc.Facade.Currencies;
using Abc.Infra.Rules;
using Abc.Pages.Currencies;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Currencies.Pages.ExchangeRules {
    public abstract class ExchangeRulesTests :BaseCurrenciesTests<ExchangeRuleView, ExchangeRuleData> {

        private RuleDb ruleDb;
        protected List<SelectListItem> ruleSets => createSelectList(ruleDb.RuleSets);
        protected List<SelectListItem> rateTypes => createSelectList(db.RateTypes);
        protected override ExchangeRuleView toView(ExchangeRuleData d) => new ExchangeRuleViewFactory().Create(new ExchangeRule(d));
        [TestInitialize] public override void TestInitialize() {
            ruleDb = getContext<RuleDb>();
            base.TestInitialize();
        }
        protected override string baseUrl() => MoneyUrls.ExchangeRules;
        protected override void changeValuesExceptId(ExchangeRuleData d) {
            var id = d.Id;
            d = random<ExchangeRuleData>();
            d.Id = id;
        }
        protected override string getItemId(ExchangeRuleData d) => d.Id;
        protected override void setItemId(ExchangeRuleData d, string id) => d.Id = id;
        protected override void addRelatedItems(ExchangeRuleData d) {
            if (d is null) return;
            addRateType(d.RateTypeId);
            addRuleSet(d.RuleSetId);
            d.Code = null;
        }
        private void addRuleSet(string id) {
            var d = random<RuleSetData>();
            d.Id = id;
            add<IRuleSetsRepo, IRuleSet>(new RuleSet(d));
        }
        protected override IEnumerable<Expression<Func<ExchangeRuleView, object>>> indexPageColumns =>
            new Expression<Func<ExchangeRuleView, object>>[] {
                x => x.RateTypeId,
                x => x.RuleSetId,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.RateTypeId, Unspecified.String);
            canView(item, m => m.RuleSetId, Unspecified.String);
            canView(item, m => m.Name);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canSelect(item, m => m.RateTypeId, rateTypes);
            canSelect(item, m => m.RuleSetId, ruleSets);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canSelect(null, m => m.RateTypeId, rateTypes);
            canSelect(null, m => m.RuleSetId, ruleSets);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            HiddenInputs.Clear();
        }
    }
    [TestClass] public class CreatePageTests :ExchangeRulesTests { }
    [TestClass] public class DeletePageTests :ExchangeRulesTests { }
    [TestClass] public class DetailsPageTests :ExchangeRulesTests { }
    [TestClass] public class EditPageTests :ExchangeRulesTests { }
    [TestClass] public class IndexPageTests :ExchangeRulesTests { }
}
