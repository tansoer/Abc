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

    [TestClass]
    public class UsagesPageTests : SealedViewPageTests<
        UsagesPage,
        IRuleUsagesRepo,
        RuleUsage,
        RuleUsageView,
        RuleUsageData> {

        protected override Type getBaseClass() =>
            typeof(ViewPage<UsagesPage, IRuleUsagesRepo,
                RuleUsage, RuleUsageView, RuleUsageData>);
        protected override string pageTitle => RuleTitles.Usages;
        protected override string pageUrl => RuleUrls.Usages;
        protected override RuleUsage toObject(RuleUsageData d) => new(d);

        private class testRepo : mockRepo<RuleUsage, RuleUsageData>, IRuleUsagesRepo {}

        private testRepo usages;
        private RulesPageTests.testRepo rules;
        private SetsPageTests.testRepo sets;
        protected override UsagesPage createObject() {
            usages = new testRepo();
            rules = new RulesPageTests.testRepo();
            sets = new SetsPageTests.testRepo();
            addRandomItems();
            return new UsagesPage(usages);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(usages, rules, sets);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void addRandomItems() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var u = new RuleUsage(GetRandom.ObjectOf<RuleUsageData>());
                usages.Add(u);
                addRule(u.RuleId);
                addSet(u.RuleSetId);
            }
        }
        private void addSet(string setId) {
            var d = GetRandom.ObjectOf<RuleSetData>();
            d.Id = setId;
            var c = new RuleSet(d);
            sets.Add(c);
        }
        private void addRule(string ruleId) {
            var d = GetRandom.ObjectOf<RuleData>();
            d.Id = ruleId;
            var c = new Rule(d);
            rules.Add(c);
        }

        [TestMethod]
        public void RuleNameTest() {
            var r = rules.list[GetRandom.Int32(0, rules.list.Count - 1)];
            var n = obj.RuleName(r.Id);
            Assert.AreEqual(n, r.Name);
        }
        [TestMethod]
        public void RuleSetNameTest() {
            var r = sets.list[GetRandom.Int32(0, sets.list.Count - 1)];
            var n = obj.RuleSetName(r.Id);
            Assert.AreEqual(n, r.Name);
        }
        [TestMethod]
        public void OnGetDetailsAsyncTest() {
            var id = rndStr;
            var d = GetRandom.ObjectOf<RuleUsageData>();
            d.Id = id;
            getRepo<IRuleUsagesRepo>().Add(new RuleUsage(d));
            var r = obj.OnGetDetailsAsync(id,
                rndStr,
                rndStr,
                GetRandom.UInt8(),
                rndStr,
                rndStr).GetAwaiter().GetResult() as RedirectResult;

            Assert.IsNotNull(r);

            var name = GetMember.Name<RuleData>(x => x.Id);
            var page = RuleUrls.Rules;
            var ruleId = getRepo<IRuleUsagesRepo>().Get(id).RuleId;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={ruleId}", UriKind.Relative);
            Assert.AreEqual(url.ToString(), r.Url);
        }
        [TestMethod]
        public void RulesTest() {
            isReadOnly();
            Assert.IsInstanceOfType(obj.Rules, typeof(List<SelectListItem>));
            Assert.AreEqual(obj.Rules.Count() - 1, rules.list.Count);
        }
        [TestMethod]
        public void SetsTest() {
            isReadOnly();
            Assert.IsInstanceOfType(obj.Sets, typeof(List<SelectListItem>));
            Assert.AreEqual(obj.Sets.Count() - 1, sets.list.Count);
        }
    }
}