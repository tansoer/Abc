using Abc.Data.Inventory;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Domain.Rules;
using Abc.Facade.Inventory;
using Abc.Infra.Rules;
using Abc.Pages.Inventory;
using AngleSharp.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Inventory.Pages.RestockPolicies {
    public abstract class RestockPoliciesTests :BaseInventoryTests<RestockPolicyView, RestockPolicyData> {
        private RuleDb ruleDb;
        protected List<SelectListItem> ruleSets => createSelectList(ruleDb.RuleSets);
        protected List<SelectListItem> ruleContexts => createSelectList(ruleDb.RuleContexts);
        protected override RestockPolicyView toView(RestockPolicyData d)
            => new RestockPolicyViewFactory().Create(new RestockPolicy(d));
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            ruleDb = getContext<RuleDb>();
        }
        protected override string baseUrl() => InventoryUrls.RestockPolicies;
        protected override void changeValuesExceptId(RestockPolicyData d) {
            var id = d.Id;
            d = random<RestockPolicyData>();
            d.Id = id;
        }
        protected override string getItemId(RestockPolicyData d) => d.Id;
        protected override void setItemId(RestockPolicyData d, string id) => d.Id = id;
        protected override void addRelatedItems(RestockPolicyData d) {
            if (d is null) return;
            addRuleSet(d.RestockRuleSetId);
            addRuleContext(d.RestockRuleContextId);
        }
        private void addRuleSet(string id) {
            var d = random<RuleSetData>();
            d.Id = id;
            add<IRuleSetsRepo, IRuleSet>(new RuleSet(d));
        }
        private void addRuleContext(string id) {
            var d = random<RuleContextData>();
            d.Id = id;
            add<IRuleContextsRepo, RuleContext>(new(d));
        }
        protected override string baseClassName() {
            var n = GetType().BaseType?.Name;
            n = n?.ReplaceFirst("Base", string.Empty);
            n = n?.ReplaceFirst("iesTests", "y");
            return n;
        }
        protected override string performTitleCorrection(string n) => n.Replace("ies", "y");
        protected override IEnumerable<Expression<Func<RestockPolicyView, object>>> indexPageColumns =>
            new Expression<Func<RestockPolicyView, object>>[] {
                x => x.RestockRuleSetId,
                x => x.RestockRuleContextId,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.RestockRuleSetId, Unspecified.String);
            canView(item, m => m.RestockRuleContextId, Unspecified.String);
            canView(item, m => m.Code);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canSelect(item, m => m.RestockRuleSetId, ruleSets);
            canSelect(item, m => m.RestockRuleContextId, ruleContexts);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canSelect(null, m => m.RestockRuleSetId, ruleSets);
            canSelect(null, m => m.RestockRuleContextId, ruleContexts);
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
    [TestClass] public class CreatePageTests :RestockPoliciesTests { }
    [TestClass] public class DeletePageTests :RestockPoliciesTests { }
    [TestClass] public class DetailsPageTests :RestockPoliciesTests { }
    [TestClass] public class EditPageTests :RestockPoliciesTests { }
    [TestClass] public class IndexPageTests :RestockPoliciesTests { }
}
