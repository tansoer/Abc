using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Facade.Rules;
using Abc.Pages.Rules;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Rules.Pages.Overrides {
    public abstract class RuleOverridesTests :BaseRuleTests<RuleOverrideView, RuleOverrideData> {
        protected List<SelectListItem> ruleSets => createSelectList(db.RuleSets);
        protected List<SelectListItem> rules => createSelectList(db.Rules);
        protected List<SelectListItem> ruleContexts => createSelectList(db.RuleContexts);
        protected override string baseUrl() => RuleUrls.Overrides;
        protected override RuleOverrideView toView(RuleOverrideData d)
            => new RuleOverrideViewFactory().Create(new Abc.Domain.Rules.RuleOverride(d));
        protected override void changeValuesExceptId(RuleOverrideData d) {
            var id = d.Id;
            d = random<RuleOverrideData>();
            d.Id = id;
        }
        protected override string getItemId(RuleOverrideData d) => d.Id;
        protected override void setItemId(RuleOverrideData d, string id) => d.Id = id;
        protected override void addRelatedItems(RuleOverrideData d) {
            if (d is null) return;
            addSet(d.RuleSetId);
            addRule(d.RuleId);
            addRuleContext(d.RuleContextId);
        }
        protected override void validateItems(RuleOverrideData d1, RuleOverrideData d2) {
            allPropertiesAreEqual(d1, d2,
                nameof(d1.VariableId),
                nameof(d1.PartyAuthenticationId),
                nameof(d1.PartySummaryId),
                nameof(d1.SignedEntityId),
                nameof(d1.SignedEntityTypeId),
                nameof(d1.PartyId));
        }
        protected override IEnumerable<Expression<Func<RuleOverrideView, object>>> indexPageColumns =>
            new Expression<Func<RuleOverrideView, object>>[] {
            x => x.Name,
            x => x.RuleSetId,
            x => x.RuleId,
            x => x.RuleContextId,
            x => x.Code,
            x => x.Details,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.Name);
            canView(item, x => x.RuleSetId, Unspecified.String);
            canView(item, x => x.RuleId, Unspecified.String);
            canView(item, x => x.RuleContextId, Unspecified.String);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, x => x.Name, true);
            canSelect(item, x => x.RuleSetId, ruleSets);
            canSelect(item, x => x.RuleId, rules);
            canSelect(item, x => x.RuleContextId, ruleContexts);
            canEdit(item, x => x.Code);
            canEdit(item, x => x.Details);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() => hasHidden(item, x => x.Id, true);
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canSelect(item, x => x.RuleSetId, ruleSets);
            canSelect(item, x => x.RuleId, rules);
            canSelect(item, x => x.RuleContextId, ruleContexts);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
    }
    [TestClass] public class CreatePageTests :RuleOverridesTests { }
    [TestClass] public class DeletePageTests :RuleOverridesTests { }
    [TestClass] public class DetailsPageTests :RuleOverridesTests { }
    [TestClass] public class EditPageTests :RuleOverridesTests { }
    [TestClass] public class IndexPageTests :RuleOverridesTests { }
}
