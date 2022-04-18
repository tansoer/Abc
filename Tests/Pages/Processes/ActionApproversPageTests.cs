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
using Action = Abc.Domain.Processes.Action;

namespace Abc.Tests.Pages.Processes {
    [TestClass]
    public class ActionApproversPageTests :SealedViewFactoryPageTests<ActionApproversPage, 
        IActionApproversRepo, ActionApprover, ActionApproverView, ActionApproverData, ActionApproverViewFactory> {
        protected override string pageTitle => ProcessTitles.ActionApprovers;
        protected override string pageUrl => ProcessUrls.ActionApprovers;

        private ActionApproverData data;
        private ActionData actionData;
        private PartySignatureData approverSignatureData;
        protected override ActionApprover toObject(ActionApproverData d) => new(d);

        private class Repo :mockRepo<ActionApprover, ActionApproverData>, IActionApproversRepo { }
        private class actionsRepo :mockRepo<Action, ActionData>, IActionsRepo { }
        private class approverSignaturesRepo :mockRepo<PartySignature, PartySignatureData>, IPartySignaturesRepo { }
        private Repo repo;
        private actionsRepo actions;
        private approverSignaturesRepo approverSignatures;

        protected override ActionApproversPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new ActionApproversPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, actions, approverSignatures);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            actions = new();
            approverSignatures = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<ActionApproverData>();
            actionData = GetRandom.ObjectOf<ActionData>();
            approverSignatureData = GetRandom.ObjectOf<PartySignatureData>();
        }
        private void addRandomDataToRepos() {
            addRandomActionApprovers();
            addRandomActions();
            addRandomApproverSignatures();
        }

        private void addRandomActionApprovers() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<ActionApproverData>();
                repo.Add(new ActionApprover(d));
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
        private void addRandomApproverSignatures() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? approverSignatureData : GetRandom.ObjectOf<PartySignatureData>();
                approverSignatures.Add(new PartySignature(d));
            }
        }

        [TestMethod]
        public void ActionsTest() {
            var list = actions.Get();
            areEqual(list.Count + 1, obj.Actions.Count());
        }

        [TestMethod]
        public void ApproverSignaturesTest() {
            var list = approverSignatures.Get();
            areEqual(list.Count + 1, obj.ApproverSignatures.Count());
        }

        [TestMethod]
        public void ActionNameTest() {
            var name = obj.ActionName(actionData.Id);
            areEqual(actionData.Name, name);
        }

        [TestMethod]
        public void ApproverSignatureNameTest() {
            var name = obj.ApproverSignatureName(approverSignatureData.Id);
            areEqual(approverSignatureData.Name, name);
        }
    }
}