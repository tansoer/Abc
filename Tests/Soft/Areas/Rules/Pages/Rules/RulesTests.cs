using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Abc.Aids.Random;
using Abc.Aids.Reflection;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Facade.Rules;
using Abc.Pages.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Rules.Pages.Rules {
    public abstract class RulesTests
        : BaseRuleTests<RuleView, RuleData> {
        protected override string baseUrl() => RuleUrls.Rules;
        protected override RuleView toView(RuleData d) 
            => new RuleViewFactory().Create(RuleFactory.Create(d));
        protected override RuleData randomItem() {
            var d = base.randomItem();
            d.RuleKind = random<bool>() ? RuleKind.Rule : RuleKind.ActivityRule;
            return d;
        }
        protected override string getItemId(RuleData d) => d.Id;
        protected override void setItemId(RuleData d, string id) => d.Id = id;
        protected override void addRelatedItems(RuleData d) {
            if (d is null) return;
            addRule(d.RuleId);
        }
        protected override void changeValuesExceptId(RuleData d) {
            var id = d.Id;
            var t = d.RuleKind;
            d = GetRandom.ObjectOf<RuleData>();
            d.RuleKind = t;
            d.Id = id;
            addRule(d.RuleId);
        }
        protected override IEnumerable<Expression<Func<RuleView, object>>> indexPageColumns =>
            new Expression<Func<RuleView, object>>[] {
                x => x.Code,
                x => x.Name,
                x => x.Details,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            Assert.IsTrue(pageHtml.Contains($"<h1>{RuleTitles.RuleElements}</h1>"));
            Assert.IsTrue(pageHtml.Contains(
              $"<form id=\"indexForm\" method=\"get\" action=\"{RuleUrls.RuleElements}\">"));
            Assert.IsTrue(pageHtml.Contains(
              "<input type=\"hidden\" name=\"FixedFilter\" " +
              $"value=\"{GetMember.Name<RuleElementView>(x => x.RuleId)}\" />"));
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
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            hasHidden(item, x => x.RuleKind, true);
            hasHidden(item, x => x.RuleId, false);
        }
        protected override void canClickEditButtonInDetailsTest() {
            if (!isDetailsPage) return;
            var url = pageUrl();
            htmlDocument = sendRequest(url);
            var form = htmlDocument.FindFormElement("form[id='indexForm']");
            Assert.IsNotNull(form);
        }
    }
    [TestClass] public class IndexPageTests :RulesTests { }
    [TestClass] public class EditPageTests :RulesTests { }
    [TestClass] public class DetailsPageTests :RulesTests {

        [TestMethod] public override void CanClickBackToListLinkTest() { }

        protected override void isCorrectPageName() {
            var n = "Ruleelement";
            var title = pageTitle();
            Assert.AreEqual(title, n);
        }

    }
    [TestClass] public class DeletePageTests :RulesTests { }
    [TestClass] public class CreatePageTests :RulesTests { }
}
