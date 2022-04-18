using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Processes;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Tests.Pages.Processes {
    [TestClass]
    public class TaskTypesPageTests :SealedViewFactoryPageTests<TaskTypesPage, 
        ITaskTypesRepo, TaskType, TaskTypeView, TaskTypeData, TaskTypeViewFactory> {
        protected override string pageTitle => ProcessTitles.TaskTypes;
        protected override string pageUrl => ProcessUrls.TaskTypes;

        private TaskTypeData data;
        private ThreadTypeData threadTypeData;
        private ActionTypeData actionTypeData;
        protected override TaskType toObject(TaskTypeData d) => new(d);

        private class testRepo :mockRepo<TaskType, TaskTypeData>, ITaskTypesRepo { }
        private class threadTypesRepo :mockRepo<ThreadType, ThreadTypeData>, IThreadTypesRepo { }
        private class actionTypesRepo :mockRepo<ActionType, ActionTypeData>, IActionTypesRepo { }
        private testRepo repo;
        private threadTypesRepo threadTypes;
        private actionTypesRepo actionTypes;

        protected override TaskTypesPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new TaskTypesPage(repo, threadTypes, actionTypes);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, threadTypes, actionTypes);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            threadTypes = new();
            actionTypes = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<TaskTypeData>();
            threadTypeData = GetRandom.ObjectOf<ThreadTypeData>();
            actionTypeData = GetRandom.ObjectOf<ActionTypeData>();
        }
        private void addRandomDataToRepos() {
            addRandomTaskTypes();
            addRandomThreadTypes();
            addRandomActionTypes();
        }

        private void addRandomTaskTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<TaskTypeData>();
                repo.Add(new TaskType(d));
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
        private void addRandomActionTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? actionTypeData : GetRandom.ObjectOf<ActionTypeData>();
                actionTypes.Add(new ActionType(d));
            }
        }

        [TestMethod]
        public void ThreadTypesTest() {
            var list = threadTypes.Get();
            areEqual(list.Count + 1, obj.ThreadTypes.Count());
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
        public void ActionTypesTest() {
            isInstanceOfType(obj.ActionTypes, typeof(List<ActionTypeView>));
            areEqual(0, obj.ActionTypes.Count);
        }

        [TestMethod]
        public void ThreadTypeNameTest() {
            var name = obj.ThreadTypeName(threadTypeData.Id);
            areEqual(threadTypeData.Name, name);
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
    }
}