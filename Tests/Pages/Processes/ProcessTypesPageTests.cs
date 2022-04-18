using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Abc.Domain.Rules;
using Abc.Facade.Processes;
using Abc.Pages.Processes;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Pages.Processes {
    [TestClass]
    public class ProcessTypesPageTests :SealedViewFactoryPageTests<ProcessTypesPage, 
        IProcessTypesRepo, ProcessType, ProcessTypeView, ProcessTypeData, ProcessTypeViewFactory> {
        protected override string pageTitle =>ProcessTitles.ProcessTypes;
        protected override string pageUrl => ProcessUrls.ProcessTypes;
        private ProcessTypeData data;
        private RuleSetData ruleSetData;
        protected override ProcessType toObject(ProcessTypeData d) => new(d);

        private class testRepo :mockRepo<ProcessType, ProcessTypeData>, IProcessTypesRepo { }
        private class ruleSetsRepo :mockRepo<IRuleSet, RuleSetData>, IRuleSetsRepo { }
        private class threadTypesRepo :mockRepo<ThreadType, ThreadTypeData>, IThreadTypesRepo { }
        private testRepo repo;
        private ruleSetsRepo ruleSets;
        private threadTypesRepo threadTypes;

        protected override ProcessTypesPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new ProcessTypesPage(repo, threadTypes);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, ruleSets, threadTypes);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            ruleSets = new();
            threadTypes = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<ProcessTypeData>();
            ruleSetData = GetRandom.ObjectOf<RuleSetData>();
        }
        private void addRandomDataToRepos() {
            addRandomProcessTypes();
            addRandomRuleSets();
        }

        private void addRandomProcessTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<ProcessTypeData>();
                repo.Add(new ProcessType(d));
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
        public void RuleSetsTest() {
            var list = ruleSets.Get();
            areEqual(list.Count + 1, obj.RuleSets.Count());
        }

        [TestMethod]
        public void RuleSetNameTest() {
            var name = obj.RuleSetName(ruleSetData.Id);
            areEqual(ruleSetData.Name, name);
        }

        [TestMethod]
        public async Task OnGetShowThreadTypesAsyncTest() {
            var page = await obj.OnGetShowThreadTypesAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            Assert.IsInstanceOfType(page, typeof(RedirectResult));
        }
    }
}