using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Processes;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Processes;
using Abc.Domain.Rules;
using Abc.Facade.Processes;
using Abc.Pages.Processes;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Processes {
    [TestClass]
    public class TasksPageTests :SealedViewPageTests<
        TasksPage, ITasksRepo, ITask, TaskView, TaskData> {
        protected override string pageTitle => ProcessTitles.Tasks;
        protected override string pageUrl => ProcessUrls.Tasks;

        private TaskData data;
        private ThreadData threadData;
        private RuleContextData ruleContextData;
        private PartyContactData partyContactData;
        protected override Task toObject(TaskData d) => new(d);

        private class testRepo :mockRepo<ITask, TaskData>, ITasksRepo { }
        private class threadsRepo :mockRepo<Thread, ThreadData>, IThreadsRepo { }

        private class ruleContextsRepo :mockRepo<RuleContext, RuleContextData>, IRuleContextsRepo {
            public string GetRuleId(string id) {
                var e = list.Find(x => x.Id == id);
                return e?.RuleId;
            }
        }
        private class partyContactsRepo :mockRepo<IPartyContact, PartyContactData>, IPartyContactsRepo { }
        private testRepo repo;
        private threadsRepo threads;
        private ruleContextsRepo ruleContexts;
        private partyContactsRepo partyContacts;

        protected override TasksPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new TasksPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, threads, ruleContexts, partyContacts);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            threads = new();
            ruleContexts = new();
            partyContacts = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<TaskData>();
            threadData = GetRandom.ObjectOf<ThreadData>();
            ruleContextData = GetRandom.ObjectOf<RuleContextData>();
            partyContactData = GetRandom.ObjectOf<PartyContactData>();
        }
        private void addRandomDataToRepos() {
            addRandomTasks();
            addRandomThreads();
            addRandomRuleContexts();
            addRandomPartyContacts();
        }

        private void addRandomTasks() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<TaskData>();
                repo.Add(new Task(d));
            }
        }
        private void addRandomThreads() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? threadData : GetRandom.ObjectOf<ThreadData>();
                threads.Add(new Thread(d));
            }
        }
        private void addRandomRuleContexts() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? ruleContextData : GetRandom.ObjectOf<RuleContextData>();
                ruleContexts.Add(new RuleContext(d));
            }
        }
        private void addRandomPartyContacts() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? partyContactData : GetRandom.ObjectOf<PartyContactData>();
                partyContacts.Add(PartyContactFactory.Create(d));
            }
        }

        [TestMethod]
        public void ThreadsTest() {
            var list = threads.Get();
            areEqual(list.Count + 1, obj.Threads.Count());
        }
        [TestMethod]
        public void RuleContextsTest() {
            var list = ruleContexts.Get();
            areEqual(list.Count + 1, obj.RuleContexts.Count());
        }
        [TestMethod]
        public void NextElementsTest() {
            var list = repo.Get();
            areEqual(list.Count + 1, obj.NextElements.Count());
        }
        [TestMethod]
        public void PreviousElementsTest() {
            var list = repo.Get();
            areEqual(list.Count + 1, obj.PreviousElements.Count());
        }
        [TestMethod]
        public void FromPartyAddressesTest() {
            var list = partyContacts.Get();
            areEqual(list.Count + 1, obj.FromPartyAddresses.Count());
        }
        [TestMethod]
        public void ToPartyAddressesTest() {
            var list = partyContacts.Get();
            areEqual(list.Count + 1, obj.ToPartyAddresses.Count());
        }

        [TestMethod]
        public void ThreadNameTest() {
            var name = obj.ThreadName(threadData.Id);
            areEqual(threadData.Name, name);
        }
        [TestMethod]
        public void RuleContextNameTest() {
            var name = obj.RuleContextName(ruleContextData.Id);
            areEqual(ruleContextData.Name, name);
        }
        [TestMethod]
        public void NextElementNameTest() {
            var name = obj.NextElementName(data.Id);
            areEqual(data.Name, name);
        }
        [TestMethod]
        public void PreviousElementNameTest() {
            var name = obj.PreviousElementName(data.Id);
            areEqual(data.Name, name);
        }
        [TestMethod]
        public void FromPartyAddressNameTest() {
            var name = obj.FromPartyAddressName(partyContactData.Id);
            areEqual(partyContactData.Name, name);
        }
        [TestMethod]
        public void ToPartyAddressNameTest() {
            var name = obj.ToPartyAddressName(partyContactData.Id);
            areEqual(partyContactData.Name, name);
        }
    }
}