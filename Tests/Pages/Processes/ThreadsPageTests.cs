using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Processes;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Processes {
    [TestClass]
    public class ThreadsPageTests :SealedViewFactoryPageTests<ThreadsPage, 
        IThreadsRepo, Thread, ThreadView, ThreadData, ThreadViewFactory> {
        protected override string pageTitle => ProcessTitles.Threads;
        protected override string pageUrl => ProcessUrls.Threads;
        private ThreadData data;
        private ThreadTypeData threadTypeData;
        private ProcessData processData;
        private PartySignatureData partySignatureData;
        protected override Thread toObject(ThreadData d) => new(d);

        private class testRepo :mockRepo<Thread, ThreadData>, IThreadsRepo { }
        private class threadTypesRepo :mockRepo<ThreadType, ThreadTypeData>, IThreadTypesRepo { }
        private class processesRepo :mockRepo<Process, ProcessData>, IProcessesRepo { }
        private class partySignaturesRepo :mockRepo<PartySignature, PartySignatureData>, IPartySignaturesRepo { }
        private testRepo repo;
        private threadTypesRepo threadTypes;
        private processesRepo processes;
        private partySignaturesRepo partySignatures;

        protected override ThreadsPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new ThreadsPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, threadTypes, processes, partySignatures);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            threadTypes = new();
            processes = new();
            partySignatures = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<ThreadData>();
            threadTypeData = GetRandom.ObjectOf<ThreadTypeData>();
            processData = GetRandom.ObjectOf<ProcessData>();
            partySignatureData = GetRandom.ObjectOf<PartySignatureData>();
        }
        private void addRandomDataToRepos() {
            addRandomThreads();
            addRandomThreadTypes();
            addRandomProcesses();
            addRandomPartySignatures();
        }

        private void addRandomThreads() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<ThreadData>();
                repo.Add(new Thread(d));
            }
        }
        private void addRandomThreadTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? threadTypeData : GetRandom.ObjectOf<ThreadTypeData>();
                threadTypes.Add(new ThreadType(d));
            }
        }
        private void addRandomProcesses() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? processData : GetRandom.ObjectOf<ProcessData>();
                processes.Add(new Process(d));
            }
        }
        private void addRandomPartySignatures() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? partySignatureData : GetRandom.ObjectOf<PartySignatureData>();
                partySignatures.Add(new PartySignature(d));
            }
        }

        [TestMethod]
        public void ThreadTypesTest() {
            var list = threadTypes.Get();
            areEqual(list.Count + 1, obj.ThreadTypes.Count());
        }
        [TestMethod]
        public void ProcessesTest() {
            var list = processes.Get();
            areEqual(list.Count + 1, obj.Processes.Count());
        }
        [TestMethod]
        public void TerminatorSignaturesTest() {
            var list = partySignatures.Get();
            areEqual(list.Count + 1, obj.TerminatorSignatures.Count());
        }

        [TestMethod]
        public void ThreadTypeNameTest() {
            var name = obj.ThreadTypeName(threadTypeData.Id);
            areEqual(threadTypeData.Name, name);
        }
        [TestMethod]
        public void ProcessNameTest() {
            var name = obj.ProcessName(processData.Id);
            areEqual(processData.Name, name);
        }
        [TestMethod]
        public void TerminatorSignatureNameTest() {
            var name = obj.TerminatorSignatureName(partySignatureData.Id);
            areEqual(partySignatureData.Name, name);
        }
    }
}