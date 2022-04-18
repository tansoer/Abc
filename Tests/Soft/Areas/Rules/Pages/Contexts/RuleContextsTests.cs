using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Abc.Aids.Random;
using Abc.Aids.Reflection;
using Abc.Data.Rules;
using Abc.Facade.Rules;
using Abc.Pages.Rules;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Rules.Pages.Contexts {

    public abstract class RuleContextsTests: BaseRuleTests<RuleContextView, RuleContextData> {
        protected override string baseUrl() => RuleUrls.Contexts;
        protected override RuleContextView toView(RuleContextData d) 
            => new RuleContextViewFactory().Create(new Abc.Domain.Rules.RuleContext(d));
        protected override string getItemId(RuleContextData d) => d.Id;
        protected override void setItemId(RuleContextData d, string id) => d.Id = id;
        protected override void addRelatedItems(RuleContextData d) {
            if (d is null) return;
            addRule(d.RuleId);
            addSet(d.RuleSetId);
        }
        protected override void changeValuesExceptId(RuleContextData d) {
            var id = d.Id;
            d = GetRandom.ObjectOf<RuleContextData>();
            d.Id = id;
        }
        protected override IEnumerable<Expression<Func<RuleContextView, object>>> indexPageColumns =>
            new Expression<Func<RuleContextView, object>>[] {
                x => x.Code,
                x => x.Name,
                x => x.Details,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataHiddenInPage() { 
            hasHidden(item, x => x.Id, true);
            hasHidden(item, x => x.RuleSetId, false);
        }
        protected override void dataInDetailsPage() {
            Assert.IsTrue(pageHtml.Contains($"<h1>{RuleTitles.RuleElements}</h1>"));
            Assert.IsTrue(pageHtml.Contains(
                $"<form id=\"indexForm\" method=\"get\" action=\"{RuleUrls.RuleElements}\">"));
            Assert.IsTrue(pageHtml.Contains(
                "<input type=\"hidden\" name=\"FixedFilter\" " +
                $"value=\"{GetMember.Name<RuleElementView>(x => x.RuleContextId)}\" />"));
            Assert.IsTrue(pageHtml.Contains(
                $"<input type=\"hidden\" name=\"FixedValue\" value=\"{item.Id}\" />"));
        }
        protected override void dataInDeletePage() {
            canView(item, m => m.Code);
            canView(item, m => m.Name);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
            canSelect(item, m => m.RuleId, rules);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
            canSelect(null, m => m.RuleId, rules);
        }
        protected List<SelectListItem> rules => createSelectList(db.Rules);
        protected override void canClickEditButtonInDetailsTest() {
            if (!isDetailsPage) return;
            var url = pageUrl();
            htmlDocument = sendRequest(url);
            var form = htmlDocument.FindFormElement("form[id='indexForm']");
            Assert.IsNotNull(form);
        }
    }
    [TestClass] public class IndexPageTests :RuleContextsTests { }
    [TestClass] public class EditPageTests :RuleContextsTests { }
    [TestClass] public class DetailsPageTests :RuleContextsTests {

        [TestMethod] public override void CanClickBackToListLinkTest() { }

        protected override void isCorrectPageName() {
            var n = "Ruleelement";
            var title = pageTitle();
            Assert.AreEqual(title, n);
        }

    }
    [TestClass] public class DeletePageTests :RuleContextsTests { }
    [TestClass] public class CreatePageTests :RuleContextsTests { }
}
