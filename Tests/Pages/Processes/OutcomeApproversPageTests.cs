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
    public class OutcomeApproversPageTests :SealedViewFactoryPageTests<OutcomeApproversPage, 
        IOutcomeApproversRepo, OutcomeApprover, OutcomeApproverView, OutcomeApproverData, OutcomeApproverViewFactory> {
        protected override string pageTitle => ProcessTitles.OutcomeApprovers;
        protected override string pageUrl => ProcessUrls.OutcomeApprovers;

        private OutcomeApproverData data;
        private OutcomeData outcomeData;
        private PartySignatureData approverSignatureData;
        protected override OutcomeApprover toObject(OutcomeApproverData d) => new(d);

        private class Repo :mockRepo<OutcomeApprover, OutcomeApproverData>, IOutcomeApproversRepo { }
        private class outcomesRepo :mockRepo<Outcome, OutcomeData>, IOutcomesRepo { }
        private class approverSignaturesRepo :mockRepo<PartySignature, PartySignatureData>, IPartySignaturesRepo { }
        private Repo repo;
        private outcomesRepo outcomes;
        private approverSignaturesRepo approverSignatures;

        protected override OutcomeApproversPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new OutcomeApproversPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, outcomes, approverSignatures);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            outcomes = new();
            approverSignatures = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<OutcomeApproverData>();
            outcomeData = GetRandom.ObjectOf<OutcomeData>();
            approverSignatureData = GetRandom.ObjectOf<PartySignatureData>();
        }
        private void addRandomDataToRepos() {
            addRandomOutcomeApprovers();
            addRandomOutcomes();
            addRandomApproverSignatures();
        }

        private void addRandomOutcomeApprovers() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<OutcomeApproverData>();
                repo.Add(new OutcomeApprover(d));
            }
        }
        private void addRandomOutcomes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? outcomeData : GetRandom.ObjectOf<OutcomeData>();
                outcomes.Add(new Outcome(d));
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
        public void OutcomesTest() {
            var list = outcomes.Get();
            areEqual(list.Count + 1, obj.Outcomes.Count());
        }
        [TestMethod]
        public void ApproverSignaturesTest() {
            var list = approverSignatures.Get();
            areEqual(list.Count + 1, obj.ApproverSignatures.Count());
        }

        [TestMethod]
        public void OutcomeNameTest() {
            var name = obj.OutcomeName(outcomeData.Id);
            areEqual(outcomeData.Name, name);
        }

        [TestMethod]
        public void ApproverSignatureNameTest() {
            var name = obj.ApproverSignatureName(approverSignatureData.Id);
            areEqual(approverSignatureData.Name, name);
        }
    }
}