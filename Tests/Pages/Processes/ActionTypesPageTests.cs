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
    public class ActionTypesPageTests :SealedViewFactoryPageTests<ActionTypesPage, 
        IActionTypesRepo, ActionType, ActionTypeView, ActionTypeData, ActionTypeViewFactory> {
        protected override string pageTitle => ProcessTitles.ActionTypes;
        protected override string pageUrl => ProcessUrls.ActionTypes;
        private ActionTypeData data;
        private TaskTypeData taskTypeData;
        private OutcomeTypeData outcomeTypeData;
        protected override ActionType toObject(ActionTypeData d) => new(d);

        private class Repo :mockRepo<ActionType, ActionTypeData>, IActionTypesRepo { }
        private class TaskTypesRepo :mockRepo<TaskType, TaskTypeData>, ITaskTypesRepo { }
        private class OutcomeTypesRepo :mockRepo<OutcomeType, OutcomeTypeData>, IOutcomeTypesRepo { }
        private Repo repo;
        private TaskTypesRepo taskTypes;
        private OutcomeTypesRepo outcomeTypes;

        protected override ActionTypesPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new ActionTypesPage(repo, taskTypes, outcomeTypes);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, taskTypes, outcomeTypes);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            taskTypes = new();
            outcomeTypes = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<ActionTypeData>();
            taskTypeData = GetRandom.ObjectOf<TaskTypeData>();
            outcomeTypeData = GetRandom.ObjectOf<OutcomeTypeData>();
        }
        private void addRandomDataToRepos() {
            addRandomActionTypes();
            addRandomTaskTypes();
            addRandomOutcomeTypes();
        }

        private void addRandomActionTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<ActionTypeData>();
                repo.Add(new ActionType(d));
            }
        }
        private void addRandomTaskTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? taskTypeData : GetRandom.ObjectOf<TaskTypeData>();
                taskTypes.Add(new TaskType(d));
            }
        }
        private void addRandomOutcomeTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? outcomeTypeData : GetRandom.ObjectOf<OutcomeTypeData>();
                outcomeTypes.Add(new OutcomeType(d));
            }
        }

        [TestMethod]
        public void TaskTypesTest() {
            var list = taskTypes.Get();
            areEqual(list.Count + 1, obj.TaskTypes.Count());
        }

        [TestMethod]
        public void OutcomeTypesTest() {
            isInstanceOfType(obj.OutcomeTypes, typeof(List<OutcomeTypeView>));
            areEqual(0, obj.OutcomeTypes.Count);
        }

        [TestMethod]
        public void TaskTypeNameTest() {
            var name = obj.TaskTypeName(taskTypeData.Id);
            areEqual(taskTypeData.Name, name);
        }
    }
}