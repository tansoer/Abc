using Abc.Aids.Random;
using Abc.Data.Inventory;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Domain.Rules;
using Abc.Facade.Inventory;
using Abc.Pages.Inventory;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Inventory {
    [TestClass]
    public class RestockPoliciesPageTests :SealedViewFactoryPageTests<RestockPoliciesPage, IRestockPoliciesRepo,
        RestockPolicy, RestockPolicyView, RestockPolicyData, RestockPolicyViewFactory> {
        protected override string pageTitle => InventoryTitles.RestockPolicies;
        protected override string pageUrl => InventoryUrls.RestockPolicies;
        protected override RestockPolicy toObject(RestockPolicyData d) => new(d);

        private RestockPolicyData restockPolicyData;
        private RuleSetData ruleSetData;
        private RuleContextData ruleContextData;

        private class repo :mockRepo<RestockPolicy, RestockPolicyData>, IRestockPoliciesRepo {}
        private class ruleSetsRepo :mockRepo<IRuleSet, RuleSetData>, IRuleSetsRepo {}
        private class ruleContextsRepo :mockRepo<RuleContext, RuleContextData>, IRuleContextsRepo {
            public string GetRuleId(string id) => Get(id).RuleId;
        }

        private repo restockPolicies;
        private ruleSetsRepo ruleSets;
        private ruleContextsRepo ruleContexts;

        protected override RestockPoliciesPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new RestockPoliciesPage(restockPolicies);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(restockPolicies, ruleSets, ruleContexts);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void initializeRepos() {
            restockPolicies = new();
            ruleSets = new();
            ruleContexts = new();
        }
        private void setRandomData() {
            restockPolicyData = random<RestockPolicyData>();
            ruleSetData = random<RuleSetData>();
            ruleContextData = random<RuleContextData>();
        }
        private void addRandomDataToRepos() {
            addRandomRestockPolicies();
            addRandomRuleSets();
            addRandomRuleContexts();
        }
        private void addRandomRestockPolicies() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? restockPolicyData : random<RestockPolicyData>();
                restockPolicies.Add(new(d));
            }
        }
        private void addRandomRuleSets() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? ruleSetData : random<RuleSetData>();
                ruleSets.Add(new RuleSet(d));
            }
        }
        private void addRandomRuleContexts() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? ruleContextData : random<RuleContextData>();
                ruleContexts.Add(new(d));
            }
        }

        [TestMethod]
        public void RuleSetsTest() {
            var list = ruleSets.Get();
            areEqual(list.Count + 1, obj.RuleSets.Count());
        }

        [TestMethod]
        public void RuleContextsTest() {
            var list = ruleContexts.Get();
            areEqual(list.Count + 1, obj.RuleContexts.Count());
        }

        [TestMethod]
        public void RuleSetNameTest() {
            var name = obj.RuleSetName(ruleSetData.Id);
            areEqual(ruleSetData.Name, name);
        }

        [TestMethod]
        public void RuleContextNameTest() {
            var name = obj.RuleContextName(ruleContextData.Id);
            areEqual(ruleContextData.Name, name);
        }
    }
}
