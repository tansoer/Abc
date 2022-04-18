using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Processes;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Processes {
    [TestClass]
    public class ThreadTypesPageTests :SealedViewFactoryPageTests<ThreadTypesPage, 
        IThreadTypesRepo, ThreadType, ThreadTypeView, ThreadTypeData, ThreadTypeViewFactory> {
        protected override string pageTitle => ProcessTitles.ThreadTypes;
        protected override string pageUrl => ProcessUrls.ThreadTypes;

        private ThreadTypeData data;
        private ProcessTypeData processTypeData;
        protected override ThreadType toObject(ThreadTypeData d) => new(d);

        private class testRepo :mockRepo<ThreadType, ThreadTypeData>, IThreadTypesRepo { }
        private class processTypesRepo :mockRepo<ProcessType, ProcessTypeData>, IProcessTypesRepo { }
        private class taskTypesRepo :mockRepo<TaskType, TaskTypeData>, ITaskTypesRepo { }
        private testRepo repo;
        private processTypesRepo processTypes;
        private taskTypesRepo taskTypes;

        protected override ThreadTypesPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new ThreadTypesPage(repo, processTypes, taskTypes);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, processTypes, taskTypes);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            processTypes = new();
            taskTypes = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<ThreadTypeData>();
            processTypeData = GetRandom.ObjectOf<ProcessTypeData>();
        }
        private void addRandomDataToRepos() {
            addRandomThreadTypes();
            addRandomProcessTypes();
        }

        private void addRandomThreadTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<ThreadTypeData>();
                repo.Add(new ThreadType(d));
            }
        }
        private void addRandomProcessTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? processTypeData : GetRandom.ObjectOf<ProcessTypeData>();
                processTypes.Add(new ProcessType(d));
            }
        }

        [TestMethod]
        public void ProcessTypesTest() {
            var list = processTypes.Get();
            areEqual(list.Count + 1, obj.ProcessTypes.Count());
        }

        [TestMethod]
        public void ProcessTypeNameTest() {
            var name = obj.ProcessTypeName(processTypeData.Id);
            areEqual(processTypeData.Name, name);
        }
    }
}