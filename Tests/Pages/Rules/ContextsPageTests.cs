using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Random;
using Abc.Aids.Reflection;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Rules;
using Abc.Facade.Rules;
using Abc.Pages.Common;
using Abc.Pages.Rules;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Rules {
    [TestClass] public class ContextsPageTests : SealedViewPageTests<ContextsPage, IRuleContextsRepo,
        RuleContext, RuleContextView, RuleContextData> {
        protected override Type getBaseClass() =>
            typeof(ViewPage<ContextsPage, IRuleContextsRepo,
                RuleContext, RuleContextView, RuleContextData>);
        protected override string pageTitle => RuleTitles.Contexts;
        protected override string pageUrl => RuleUrls.Contexts;
        protected override RuleContext toObject(RuleContextData d) => new(d);
        internal class testRepo : mockRepo<RuleContext, RuleContextData>, IRuleContextsRepo {
            public string GetRuleId(string id) {
                var e = list.Find(x => x.Id == id);
                return e?.RuleId;
            }
        }
        private testRepo contexts;
        private RulesPageTests.testRepo rules;
        private ElementsPageTests.testRepo elements;
        protected override ContextsPage createObject() {
            contexts = new testRepo();
            rules = new RulesPageTests.testRepo();
            elements = new ElementsPageTests.testRepo();
            addRandomItems();
            sortOrder = rndStr;
            searchString = rndStr;
            pageIndex = GetRandom.UInt8();
            fixedFilter = rndStr;
            fixedValue = rndStr;
            createSwitch = GetRandom.UInt8(0, 10);
            return new ContextsPage(contexts);
        }
        private IServiceProvider services;
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(contexts, rules, elements);
        }
        [TestCleanup] public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void addRandomItems() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var c = new RuleContext(GetRandom.ObjectOf<RuleContextData>());
                addRule(c.RuleId);
                contexts.Add(c);
            }
        }
        private void addRule(string ruleId) {
            var d = GetRandom.ObjectOf<RuleData>();
            d.Id = ruleId;
            var c = new Rule(d);
            rules.Add(c);
        }
        [TestMethod] public void RuleNameTest() {
            var r = rules.list[GetRandom.Int32(0, rules.list.Count - 1)];
            var n = obj.RuleName(r.Id);
            Assert.AreEqual(n, r.Name);
        }
        [TestMethod] public void OnGetDetailsAsyncTest() {
            var id = rndStr;
            var r = obj.OnGetDetailsAsync(id, rndStr, rndStr,
                GetRandom.UInt8(), rndStr, rndStr).GetAwaiter().GetResult() as RedirectResult;
            Assert.IsNotNull(r);
            var name = GetMember.Name<RuleElementData>(x => x.RuleContextId);
            var page = RuleUrls.RuleElements;
            var index = GetMember.Name<RuleElementView>(x => x.Index);
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder={index}", UriKind.Relative);
            Assert.AreEqual(url.ToString(), r.Url);
        }
        [TestMethod] public void RulesTest() {
            isReadOnly();
            Assert.IsInstanceOfType(obj.Rules, typeof(List<SelectListItem>));
            Assert.AreEqual(obj.Rules.Count() - 1, rules.list.Count);
        }
    }
}