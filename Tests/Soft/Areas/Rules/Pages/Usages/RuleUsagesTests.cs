using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Facade.Rules;
using Abc.Pages.Rules;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Rules.Pages.Usages {
    public abstract class RuleUsagesTests :BaseRuleTests<RuleUsageView, RuleUsageData> {
        protected List<SelectListItem> ruleSets => createSelectList(db.RuleSets);
        protected List<SelectListItem> rules => createSelectList(db.Rules);
        protected override string baseUrl() => RuleUrls.Usages;
        protected override RuleUsageView toView(RuleUsageData d)
            => new RuleUsageViewFactory().Create(new Abc.Domain.Rules.RuleUsage(d));
        protected override void changeValuesExceptId(RuleUsageData d) {
            var id = d.Id;
            d = random<RuleUsageData>();
            d.Id = id;
        }
        protected override string getItemId(RuleUsageData d) => d.Id;
        protected override void setItemId(RuleUsageData d, string id) => d.Id = id;
        protected override void addRelatedItems(RuleUsageData d) {
            if (d is null) return;
            addSet(d.RuleSetId);
            addRule(d.RuleId);
        }
        protected override IEnumerable<Expression<Func<RuleUsageView, object>>> indexPageColumns =>
            new Expression<Func<RuleUsageView, object>>[] {
            x => x.RuleSetId,
            x => x.RuleId,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void validateItems(RuleUsageData d1, RuleUsageData d2) {
            allPropertiesAreEqual(d1, d2,
                nameof(d1.Code),
                nameof(d1.Details));
        }
        protected override void dataInDetailsPage() {
            Assert.IsTrue(pageHtml.Contains($"<h1>{RuleTitles.Rules}</h1>"));
            Assert.IsTrue(pageHtml.Contains(
                $"<form id=\"indexForm\" method=\"get\" action=\"{RuleUrls.Rules}\">"));
            Assert.IsTrue(pageHtml.Contains(
                $"<input type=\"hidden\" name=\"FixedValue\" value=\"{item.RuleId}\" />"));
        }
        protected override void dataInDeletePage() {
            canView(item, m => m.Name);
            canView(item, m => m.RuleSetId, Unspecified.String);
            canView(item, m => m.RuleId, Unspecified.String);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canSelect(item, x => x.RuleSetId, ruleSets);
            canSelect(item, x => x.RuleId, rules);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canSelect(null, x => x.RuleSetId, ruleSets);
            canSelect(null, x => x.RuleId, rules);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage()
            => hasHidden(item, x => x.Id, true);
        protected override void canClickEditButtonInDetailsTest() {
            if (!isDetailsPage) return;
            var url = pageUrl();
            htmlDocument = sendRequest(url);
            var form = htmlDocument.FindFormElement("form[id='indexForm']");
            Assert.IsNotNull(form);
        }
    }
    [TestClass] public class CreatePageTests :RuleUsagesTests { }
    [TestClass] public class DeletePageTests :RuleUsagesTests { }
    [TestClass] public class DetailsPageTests :RuleUsagesTests {
        [TestMethod] public override void CanClickBackToListLinkTest() { }
        protected override void isCorrectPageName() {
            var n = "Rule".ToLower();
            var title = pageTitle().ToLower();
            Assert.AreEqual(title, n);
        }
    }
    [TestClass] public class EditPageTests :RuleUsagesTests { }
    [TestClass] public class IndexPageTests :RuleUsagesTests { }
}
