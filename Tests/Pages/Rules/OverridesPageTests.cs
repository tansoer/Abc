using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Rules;
using Abc.Facade.Rules;
using Abc.Pages.Common;
using Abc.Pages.Rules;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Rules {
    [TestClass] public class OverridesPageTests :SealedViewPageTests<OverridesPage, IRuleOverridesRepo, 
        RuleOverride, RuleOverrideView, RuleOverrideData> {

        protected override Type getBaseClass() =>
            typeof(ViewPage<OverridesPage, IRuleOverridesRepo,
                RuleOverride, RuleOverrideView, RuleOverrideData>);

        protected override string pageTitle => RuleTitles.Overrides;
        protected override string pageUrl => RuleUrls.Overrides;

        private RuleOverrideData data;
        private RuleSetData ruleSetData;
        private RuleData ruleData;
        private RuleContextData ruleContextData;
        protected override RuleOverride toObject(RuleOverrideData d) => new(d);

        private class testRepo :mockRepo<RuleOverride, RuleOverrideData>, IRuleOverridesRepo { }
        private class ruleSetsRepo :mockRepo<IRuleSet, RuleSetData>, IRuleSetsRepo { }
        private class rulesRepo :mockRepo<BaseRule, RuleData>, IRulesRepo { }
        private class rulecontextsRepo :mockRepo<RuleContext, RuleContextData>, IRuleContextsRepo {
            public string GetRuleId(string id) {
                var e = list.Find(x => x.Id == id);
                return e?.RuleId;
            }
        }
        private testRepo Repo;
        private ruleSetsRepo RuleSets;
        private rulesRepo Rules;
        private rulecontextsRepo RuleContexts;

        protected override OverridesPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new OverridesPage(Repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(Repo, RuleSets, Rules, RuleContexts);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            Repo = new();
            RuleSets = new();
            Rules = new();
            RuleContexts = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<RuleOverrideData>();
            ruleSetData = GetRandom.ObjectOf<RuleSetData>();
            ruleData = GetRandom.ObjectOf<RuleData>();
            ruleContextData = GetRandom.ObjectOf<RuleContextData>();
        }
        private void addRandomDataToRepos() {
            addRandomRuleOverrides();
            addRandomRuleSets();
            addRandomRules();
            addRandomRuleContexts();
        }

        private void addRandomRuleOverrides() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<RuleOverrideData>();
                Repo.Add(new RuleOverride(d));
            }
        }
        private void addRandomRuleSets() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? ruleSetData : GetRandom.ObjectOf<RuleSetData>();
                RuleSets.Add(new RuleSet(d));
            }
        }
        private void addRandomRules() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? ruleData : GetRandom.ObjectOf<RuleData>();
                Rules.Add(new Rule(d));
            }
        }
        private void addRandomRuleContexts() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? ruleContextData : GetRandom.ObjectOf<RuleContextData>();
                RuleContexts.Add(new RuleContext(d));
            }
        }

        [TestMethod]
        public void RuleSetsTest() {
            var list = RuleSets.Get();
            areEqual(list.Count + 1, obj.RuleSets.Count());
        }
        [TestMethod]
        public void RulesTest() {
            var list = Rules.Get();
            areEqual(list.Count + 1, obj.Rules.Count());
        }
        [TestMethod]
        public void RuleContextsTest() {
            var list = RuleContexts.Get();
            areEqual(list.Count + 1, obj.RuleContexts.Count());
        }

        [TestMethod]
        public void RuleSetNameTest() {
            var name = obj.RuleSetName(ruleSetData.Id);
            areEqual(ruleSetData.Name, name);
        }
        [TestMethod]
        public void RuleNameTest() {
            var name = obj.RuleName(ruleData.Id);
            areEqual(ruleData.Name, name);
        }
        [TestMethod]
        public void RuleContextNameTest() {
            var name = obj.RuleContextName(ruleContextData.Id);
            areEqual(ruleContextData.Name, name);
        }
    }
}