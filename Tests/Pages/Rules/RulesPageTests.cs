using System;
using System.Collections.Generic;
using Abc.Aids.Random;
using Abc.Aids.Reflection;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Facade.Rules;
using Abc.Pages.Common;
using Abc.Pages.Rules;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Rules {
    [TestClass] public class RulesPageTests : SealedViewsPageTests<RulesPage, IRulesRepo, 
        BaseRule, RuleView, RuleData, RuleKind> {

        protected override Type getBaseClass() =>
            typeof(ViewsPage<RulesPage, IRulesRepo,
                BaseRule, RuleView, RuleData, RuleKind>);

        internal class testRepo : mockRepo<BaseRule, RuleData>, IRulesRepo { }

        private testRepo Repo;

        protected override RulesPage createObject() {
        Repo = new testRepo();
            sortOrder = rndStr;
            searchString = rndStr;
            pageIndex = GetRandom.UInt8();
            fixedFilter = rndStr;
            fixedValue = rndStr;
            createSwitch = GetRandom.UInt8(0, 10);
            return new RulesPage(Repo);
        }

        protected override string pageUrl => RuleUrls.Rules;

        protected override string pageTitle => RuleTitles.Rules;

        protected override BaseRule toObject(RuleData d) {
            return d.RuleKind switch
            {
                RuleKind.ActivityRule => new ActivityRule(d),
                RuleKind.Rule => new Rule(d),
                _ => new Rule(d)
            };
        }
        [TestMethod]
        public void OnGetDetailsAsyncTest() {
            var id = rndStr;
            var r = obj.OnGetDetailsAsync(id,
                rndStr,
                rndStr,
                GetRandom.UInt8(),
                rndStr,
                rndStr).GetAwaiter().GetResult() as RedirectResult;

            Assert.IsNotNull(r);

            var name = GetMember.Name<RuleElementData>(x => x.RuleId);
            var page = RuleUrls.RuleElements;
            var index = GetMember.Name<RuleElementView>(x => x.Index);
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder={index}", UriKind.Relative);

            Assert.AreEqual(url.ToString(), r.Url);
        }

        [TestMethod]
        public void RuleNameTest() {
            var c = GetRandom.UInt8(10, 20);
            var l = new List<SelectListItem>();
            for (var i = 0; i <= c; i++)
                l.Add(new SelectListItem(rndStr, rndStr));
            (obj.Rules as List<SelectListItem>)?.AddRange(l);
            var idx = GetRandom.UInt8(0, c);
            var name = l[idx].Text;
            var index = l[idx].Value;
            Assert.AreEqual(name, obj.RuleName(index));
        }
        [TestMethod] public void RulesTest() => Assert.IsNotNull(obj.Rules as List<SelectListItem>);

        [TestMethod]
        public void CreateUriTest() {
            const string uri = "/Rules/Rules/Create?handler=Create&pageIndex=0" +
              "&sortOrder=&searchString=&currentFilter=&fixedFilter=&fixedValue=&switchOfCreate={0}";
            Assert.AreEqual(string.Format(uri, 0), obj.CreateUri(RuleKind.Unspecified).ToString());
            Assert.AreEqual(string.Format(uri, 1), obj.CreateUri(RuleKind.Rule).ToString());
            Assert.AreEqual(string.Format(uri, 2), obj.CreateUri(RuleKind.ActivityRule).ToString());
        }

        [TestMethod]
        public void RuleTypeTest() {
            obj.RuleType = RuleKind.ActivityRule;
            Assert.AreEqual(RuleKind.ActivityRule, obj.RuleType);
            obj.RuleType = RuleKind.Rule;
            Assert.AreEqual(RuleKind.Rule, obj.RuleType);
        }
    }
}
