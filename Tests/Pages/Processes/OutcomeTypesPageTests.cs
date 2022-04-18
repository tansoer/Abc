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
    public class OutcomeTypesPageTests :SealedViewFactoryPageTests<OutcomeTypesPage, 
        IOutcomeTypesRepo, OutcomeType, OutcomeTypeView, OutcomeTypeData, OutcomeTypeViewFactory> {
        protected override string pageTitle => ProcessTitles.OutcomeTypes;
        protected override string pageUrl => ProcessUrls.OutcomeTypes;

        private OutcomeTypeData data;
        private ActionTypeData actionTypeData;
        protected override OutcomeType toObject(OutcomeTypeData d) => new(d);

        private class Repo :mockRepo<OutcomeType, OutcomeTypeData>, IOutcomeTypesRepo { }
        private class ActionTypesRepo :mockRepo<ActionType, ActionTypeData>, IActionTypesRepo { }
        private Repo repo;
        private ActionTypesRepo actionTypes;

        protected override OutcomeTypesPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new OutcomeTypesPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, actionTypes);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            actionTypes = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<OutcomeTypeData>();
            actionTypeData = GetRandom.ObjectOf<ActionTypeData>();
        }
        private void addRandomDataToRepos() {
            addRandomOutcomeTypes();
            addRandomActionTypes();
        }

        private void addRandomOutcomeTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<OutcomeTypeData>();
                repo.Add(new OutcomeType(d));
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
        public void ActionTypesTest() {
            var list = actionTypes.Get();
            areEqual(list.Count + 1, obj.ActionTypes.Count());
        }

        [TestMethod]
        public void ActionTypeNameTest() {
            var name = obj.ActionTypeName(actionTypeData.Id);
            areEqual(actionTypeData.Name, name);
        }
    }
}