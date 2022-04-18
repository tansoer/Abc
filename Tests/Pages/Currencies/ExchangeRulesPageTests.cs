using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Rules;
using Abc.Facade.Currencies;
using Abc.Pages.Currencies;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Currencies {
    [TestClass] public class ExchangeRulesPageTests : SealedViewPageTests<ExchangeRulesPage, 
        IExchangeRulesRepo, ExchangeRule, ExchangeRuleView, ExchangeRuleData> {
        protected override string pageUrl => MoneyUrls.ExchangeRules;
        protected override string pageTitle => MoneyTitles.ExchangeRules;
        protected override ExchangeRule toObject(ExchangeRuleData d) => new(d);

        private class testRepo : mockRepo<ExchangeRule, ExchangeRuleData>, IExchangeRulesRepo { }
        private testRepo repo;
        private ExchangeRuleData Data;

        private class rateTypesRepo :mockRepo<RateType, RateTypeData>, IRateTypesRepo { }
        private rateTypesRepo rateTypes;
        private RateTypeData rateTypeData;

        private class ruleSetsRepo :mockRepo<IRuleSet, RuleSetData>, IRuleSetsRepo { }
        private ruleSetsRepo ruleSets;
        private RuleSetData ruleSetData;

        protected override ExchangeRulesPage createObject() {
            repo = new ();
            rateTypes = new();
            ruleSets = new();
            Data = random<ExchangeRuleData>();
            rateTypeData = random<RateTypeData>();
            ruleSetData = random<RuleSetData>();
            addRandomExchangeRules();
            addRandomRateTypes();
            addRandomRuleSets();
            return new ExchangeRulesPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, rateTypes, ruleSets);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void addRandomExchangeRules() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? Data : GetRandom.ObjectOf<ExchangeRuleData>();
                repo.Add(new (d));
            }
        }

        private void addRandomRateTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? rateTypeData : GetRandom.ObjectOf<RateTypeData>();
                rateTypes.Add(new(d));
            }
        }

        private void addRandomRuleSets() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? ruleSetData : GetRandom.ObjectOf<RuleSetData>();
                ruleSets.Add(new RuleSet(d));
            }
        }

        [TestMethod]
        public override void ToObjectTest() {
            var view = GetRandom.ObjectOf<ExchangeRuleView>();
            var o = obj.toObject(view);
            allPropertiesAreEqual(view, o.Data);
        }

        [TestMethod]
        public override void ToViewTest() {
            var d = GetRandom.ObjectOf<ExchangeRuleData>();
            var view = obj.toView(toObject(d));
            allPropertiesAreEqual(view, d);
        }

        [TestMethod]
        public void RateTypesTest() {
            var list = rateTypes.Get();
            Assert.AreEqual(list.Count + 1, obj.RateTypes.Count());
        }

        [TestMethod]
        public void RuleSetsTest() {
            var list = ruleSets.Get();
            Assert.AreEqual(list.Count + 1, obj.RuleSets.Count());
        }

        [TestMethod]
        public void RateTypeNameTest() {
            var name = obj.RateTypeName(rateTypeData.Id);
            Assert.AreEqual(rateTypeData.Name, name);
        }

        [TestMethod]
        public void RuleSetNameTest() {
            var name = obj.RuleSetName(ruleSetData.Id);
            Assert.AreEqual(ruleSetData.Name, name);
        }
    }
}
