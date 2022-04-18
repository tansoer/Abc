using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Data.Processes;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Processes;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Processes;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Action = Abc.Domain.Processes.Action;

namespace Abc.Tests.Pages.Processes {
    [TestClass]
    public class ActionsPageTests :SealedViewFactoryPageTests<ActionsPage, 
        IActionsRepo, Action, ActionView, ActionData, ActionViewFactory> {
        protected override string pageTitle => ProcessTitles.Actions;
        protected override string pageUrl => ProcessUrls.Actions;

        private ActionData data;
        private ActionTypeData actionTypeData;
        private TaskData taskData;
        private PartySignatureData initiatorSignatureData;
        private ClassifierData actionStatusClassifierData;
        protected override Action toObject(ActionData d) => new(d);

        private class Repo :mockRepo<Action, ActionData>, IActionsRepo { }
        private class actionTypesRepo :mockRepo<ActionType, ActionTypeData>, IActionTypesRepo { }
        private class tasksRepo :mockRepo<ITask, TaskData>, ITasksRepo { }
        private class initiatorSignaturesRepo :mockRepo<PartySignature, PartySignatureData>, IPartySignaturesRepo { }
        private class actionStatusClassifiersRepo :mockRepo<IClassifier, ClassifierData>, IClassifiersRepo { }
        private Repo repo;
        private actionTypesRepo actionTypes;
        private tasksRepo tasks;
        private initiatorSignaturesRepo initiatorSignatures;
        private actionStatusClassifiersRepo actionStatusClassifiers;

        protected override ActionsPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new ActionsPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, actionTypes, tasks, initiatorSignatures, actionStatusClassifiers);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            actionTypes = new();
            tasks = new();
            initiatorSignatures = new();
            actionStatusClassifiers = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<ActionData>();
            actionTypeData = GetRandom.ObjectOf<ActionTypeData>();
            taskData = GetRandom.ObjectOf<TaskData>();
            initiatorSignatureData = GetRandom.ObjectOf<PartySignatureData>();
            actionStatusClassifierData = GetRandom.ObjectOf<ClassifierData>();
        }
        private void addRandomDataToRepos() {
            addRandomActions();
            addRandomActionTypes();
            addRandomTasks();
            addRandomInitiatorSignatures();
            addRandomActionStatusClassifiers();
        }

        private void addRandomActions() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<ActionData>();
                repo.Add(new Action(d));
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
        private void addRandomTasks() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? taskData : GetRandom.ObjectOf<TaskData>();
                tasks.Add(new Task(d));
            }
        }
        private void addRandomInitiatorSignatures() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? initiatorSignatureData : GetRandom.ObjectOf<PartySignatureData>();
                initiatorSignatures.Add(new PartySignature(d));
            }
        }
        private void addRandomActionStatusClassifiers() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? actionStatusClassifierData : GetRandom.ObjectOf<ClassifierData>();
                d.ClassifierType = ClassifierType.ActionStatus;
                actionStatusClassifiers.Add(new ActionStatus(d));
            }
        }

        [TestMethod]
        public void ActionTypesTest() {
            var list = actionTypes.Get();
            areEqual(list.Count + 1, obj.ActionTypes.Count());
        }
        [TestMethod]
        public void TasksTest() {
            var list = tasks.Get();
            areEqual(list.Count + 1, obj.Tasks.Count());
        }
        [TestMethod]
        public void InitiatorSignaturesTest() {
            var list = initiatorSignatures.Get();
            areEqual(list.Count + 1, obj.InitiatorSignatures.Count());
        }
        [TestMethod]
        public void ActionStatusClassifiersTest() {
            var list = actionStatusClassifiers.Get();
            areEqual(list.Count + 1, obj.ActionStatusClassifiers.Count());
        }

        [TestMethod]
        public void ActionTypeNameTest() {
            var name = obj.ActionTypeName(actionTypeData.Id);
            areEqual(actionTypeData.Name, name);
        }
        [TestMethod]
        public void TaskNameTest() {
            var name = obj.TaskName(taskData.Id);
            areEqual(taskData.Name, name);
        }
        [TestMethod]
        public void InitiatorSignatureNameTest() {
            var name = obj.InitiatorSignatureName(initiatorSignatureData.Id);
            areEqual(initiatorSignatureData.Name, name);
        }
        [TestMethod]
        public void ActionStatusClassifierNameTest() {
            var name = obj.ActionStatusClassifierName(actionStatusClassifierData.Id);
            areEqual(actionStatusClassifierData.Name, name);
        }
    }
}