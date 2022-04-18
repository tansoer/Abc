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

namespace Abc.Tests.Soft.Areas.Rules.Pages.Sets {
    public abstract class RuleSetsTests
        : BaseRuleTests<RuleSetView, RuleSetData> {
        protected override string baseUrl() => RuleUrls.RuleSets;
        protected override RuleSetView toView(RuleSetData d)
            => new RuleSetViewFactory().Create(new RuleSet(d));
        protected override string getItemId(RuleSetData d) => d.Id;
        protected override void setItemId(RuleSetData d, string id) => d.Id = id;
        protected override void changeValuesExceptId(RuleSetData d) {
            var id = d.Id;
            d = GetRandom.ObjectOf<RuleSetData>();
            d.Id = id;
        }
        protected override IEnumerable<Expression<Func<RuleSetView, object>>> indexPageColumns =>
            new Expression<Func<RuleSetView, object>>[] {
                x => x.Code,
                x => x.Name,
                x => x.Details,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            Assert.IsTrue(pageHtml.Contains($"<h1>{RuleTitles.Usages}</h1>"));
            Assert.IsTrue(pageHtml.Contains(
              $"<form id=\"indexForm\" method=\"get\" action=\"{RuleUrls.Usages}\">"));
            Assert.IsTrue(pageHtml.Contains(
              "<input type=\"hidden\" name=\"FixedFilter\" " +
              $"value=\"{GetMember.Name<RuleUsageView>(x => x.RuleSetId)}\" />"));
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
    [TestClass] public class IndexPageTests :RuleSetsTests { }
    [TestClass] public class EditPageTests :RuleSetsTests { }
    [TestClass] public class DetailsPageTests :RuleSetsTests {

        [TestMethod] public override void CanClickBackToListLinkTest() { }

        protected override void isCorrectPageName() {
            var n = "RuleUsage".ToLower();
            var title = pageTitle().ToLower();
            Assert.AreEqual(title, n);
        }
    }
    [TestClass] public class DeletePageTests :RuleSetsTests { }
    [TestClass] public class CreatePageTests :RuleSetsTests { }
}
