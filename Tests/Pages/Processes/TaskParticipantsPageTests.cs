using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Abc.Domain.Roles;
using Abc.Facade.Processes;
using Abc.Pages.Processes;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Processes {
    [TestClass]
    public class TaskParticipantsPageTests :SealedViewFactoryPageTests<TaskParticipantsPage, 
        ITaskParticipantsRepo, TaskParticipant, TaskParticipantView, TaskParticipantData, TaskParticipantViewFactory> {
        protected override string pageTitle => ProcessTitles.TaskParticipants;
        protected override string pageUrl => ProcessUrls.TaskParticipants;

        private TaskParticipantData data;
        private PartyRoleData partyRoleData;
        private TaskData taskData;
        protected override TaskParticipant toObject(TaskParticipantData d) => new(d);

        private class testRepo :mockRepo<TaskParticipant, TaskParticipantData>, ITaskParticipantsRepo { }
        private class partyRolesRepo :mockRepo<PartyRole, PartyRoleData>, IPartyRolesRepo { }
        private class tasksRepo :mockRepo<ITask, TaskData>, ITasksRepo { }
        private testRepo repo;
        private partyRolesRepo partyRoles;
        private tasksRepo tasks;

        protected override TaskParticipantsPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new TaskParticipantsPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, partyRoles, tasks);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            partyRoles = new();
            tasks = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<TaskParticipantData>();
            partyRoleData = GetRandom.ObjectOf<PartyRoleData>();
            taskData = GetRandom.ObjectOf<TaskData>();
        }
        private void addRandomDataToRepos() {
            addRandomTaskParticipants();
            addRandomPartyRoles();
            addRandomTasks();
        }

        private void addRandomTaskParticipants() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<TaskParticipantData>();
                repo.Add(new TaskParticipant(d));
            }
        }
        private void addRandomPartyRoles() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? partyRoleData : GetRandom.ObjectOf<PartyRoleData>();
                partyRoles.Add(new PartyRole(d));
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

        [TestMethod]
        public void PartyRolesTest() {
            var list = partyRoles.Get();
            areEqual(list.Count + 1, obj.PartyRoles.Count());
        }

        [TestMethod]
        public void TasksTest() {
            var list = tasks.Get();
            areEqual(list.Count + 1, obj.Tasks.Count());
        }

        [TestMethod]
        public void PartyRoleNameTest() {
            var name = obj.PartyRoleName(partyRoleData.Id);
            areEqual(partyRoleData.Name, name);
        }

        [TestMethod]
        public void TaskNameTest() {
            var name = obj.TaskName(taskData.Id);
            areEqual(taskData.Name, name);
        }
    }
}