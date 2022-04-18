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
using Action = Abc.Domain.Processes.Action;

namespace Abc.Tests.Pages.Processes {
    [TestClass]
    public class OutcomesPageTests :SealedViewFactoryPageTests<OutcomesPage, 
        IOutcomesRepo, Outcome, OutcomeView, OutcomeData, OutcomeViewFactory> {
        protected override string pageTitle => ProcessTitles.Outcomes;
        protected override string pageUrl => ProcessUrls.Outcomes;

        private OutcomeData data;
        private OutcomeTypeData outcomeTypeData;
        private ActionData actionData;
        protected override Outcome toObject(OutcomeData d) => new(d);

        private class Repo :mockRepo<Outcome, OutcomeData>, IOutcomesRepo { }
        private class outcomeTypesRepo :mockRepo<OutcomeType, OutcomeTypeData>, IOutcomeTypesRepo { }
        private class actionsRepo :mockRepo<Action, ActionData>, IActionsRepo { }
        private Repo repo;
        private outcomeTypesRepo outcomeTypes;
        private actionsRepo actions;

        protected override OutcomesPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new OutcomesPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, outcomeTypes, actions);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            outcomeTypes = new();
            actions = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<OutcomeData>();
            outcomeTypeData = GetRandom.ObjectOf<OutcomeTypeData>();
            actionData = GetRandom.ObjectOf<ActionData>();
        }
        private void addRandomDataToRepos() {
            addRandomOutcomes();
            addRandomOutcomeTypes();
            addRandomActions();
        }

        private void addRandomOutcomes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<OutcomeData>();
                repo.Add(new Outcome(d));
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
        private void addRandomActions() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? actionData : GetRandom.ObjectOf<ActionData>();
                actions.Add(new Action(d));
            }
        }

        [TestMethod]
        public void OutcomeTypesTest() {
            var list = outcomeTypes.Get();
            areEqual(list.Count + 1, obj.OutcomeTypes.Count());
        }

        [TestMethod]
        public void ActionsTest() {
            var list = actions.Get();
            areEqual(list.Count + 1, obj.Actions.Count());
        }

        [TestMethod]
        public void OutcomeTypeNameTest() {
            var name = obj.OutcomeTypeName(outcomeTypeData.Id);
            areEqual(outcomeTypeData.Name, name);
        }

        [TestMethod]
        public void ActionNameTest() {
            var name = obj.ActionName(actionData.Id);
            areEqual(actionData.Name, name);
        }
    }
}