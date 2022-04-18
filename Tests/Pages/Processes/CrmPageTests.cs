using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Processes;
using Abc.Data.Roles;
using Abc.Data.Rules;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Processes;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using Abc.Facade.Processes;
using Abc.Pages.Processes;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Pages.Processes {
    [TestClass]
    public class CrmPageTests :SealedViewFactoryPageTests<CrmPage, 
        IProcessesRepo, Process, ProcessView, ProcessData, ProcessViewFactory> {
        protected override string pageTitle => ProcessTitles.Processes;
        protected override string pageUrl => ProcessUrls.Processes;
        private ProcessData data;
        private ProcessTypeData processTypeData;
        private RuleContextData ruleContextData;
        private PartyRoleData managerPartyRoleData;
        private PartyRoleData initiatorSignatureData;
        private ClassifierData priorityClassifierData;
        protected override Process toObject(ProcessData d) => new(d);

        private class testRepo :mockRepo<Process, ProcessData>, IProcessesRepo { }
        private class processTypesRepo :mockRepo<ProcessType, ProcessTypeData>, IProcessTypesRepo { }
        private class ruleContextsRepo :mockRepo<RuleContext, RuleContextData>, IRuleContextsRepo {
            public string GetRuleId(string id) {
                var e = list.Find(x => x.Id == id);
                return e?.RuleId;
            }
        }
        private class partyRolesRepo :mockRepo<PartyRole, PartyRoleData>, IPartyRolesRepo { }
        private class priorityClassifiersRepo :mockRepo<IClassifier, ClassifierData>, IClassifiersRepo { }
        private testRepo repo;
        private processTypesRepo processTypes;
        private ruleContextsRepo ruleContexts;
        private partyRolesRepo partyRoles;
        private priorityClassifiersRepo priorityClassifiers;

        protected override CrmPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new CrmPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, processTypes, ruleContexts, partyRoles, priorityClassifiers);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            processTypes = new();
            ruleContexts = new();
            partyRoles = new();
            priorityClassifiers = new();
        }
        private void setRandomData() {
            data = GetRandom.ObjectOf<ProcessData>();
            processTypeData = GetRandom.ObjectOf<ProcessTypeData>();
            ruleContextData = GetRandom.ObjectOf<RuleContextData>();
            managerPartyRoleData = GetRandom.ObjectOf<PartyRoleData>();
            initiatorSignatureData = GetRandom.ObjectOf<PartyRoleData>();
            priorityClassifierData = GetRandom.ObjectOf<ClassifierData>();
        }
        private void addRandomDataToRepos() {
            addRandomProcesses();
            addRandomProcessTypes();
            addRandomRuleContexts();
            addRandomManagerPartyRoles();
            addRandomInitiatorSignatures();
            addRandomPriorityClassifiers();
        }

        private void addRandomProcesses() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<ProcessData>();
                repo.Add(new Process(d));
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
        private void addRandomRuleContexts() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? ruleContextData : GetRandom.ObjectOf<RuleContextData>();
                ruleContexts.Add(new RuleContext(d));
            }
        }
        private void addRandomManagerPartyRoles() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? managerPartyRoleData : GetRandom.ObjectOf<PartyRoleData>();
                partyRoles.Add(new PartyRole(d));
            }
        }
        private void addRandomInitiatorSignatures() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? initiatorSignatureData : GetRandom.ObjectOf<PartyRoleData>();
                partyRoles.Add(new PartyRole(d));
            }
        }
        private void addRandomPriorityClassifiers() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? priorityClassifierData : GetRandom.ObjectOf<ClassifierData>();
                d.ClassifierType = ClassifierType.ProcessPriority;
                priorityClassifiers.Add(new ProcessPriority(d));
            }
        }

        [TestMethod]
        public void ProcessTypesTest() {
            var list = processTypes.Get();
            areEqual(list.Count + 1, obj.ProcessTypes.Count());
        }
        [TestMethod]
        public void RuleContextsTest() {
            var list = ruleContexts.Get();
            areEqual(list.Count + 1, obj.RuleContexts.Count());
        }
        [TestMethod] public void ManagerPartyRolesTest() {
            var list = partyRoles.Get();
            areEqual(list.Count + 1, obj.ManagerPartyRoles.Count());
        }
        [TestMethod]
        public void InitiatorPartyRolesTest() {
            var list = partyRoles.Get();
            areEqual(list.Count + 1, obj.InitiatorPartyRoles.Count());
        }
        [TestMethod]
        public void PriorityClassifiersTest() {
            var list = priorityClassifiers.Get();
            areEqual(list.Count + 1, obj.PriorityClassifiers.Count());
        }
        [TestMethod]
        public void ProcessTypeNameTest() {
            var name = obj.ProcessTypeName(processTypeData.Id);
            areEqual(processTypeData.Name, name);
        }
        [TestMethod]
        public void RuleContextNameTest() {
            var name = obj.RuleContextName(ruleContextData.Id);
            areEqual(ruleContextData.Name, name);
        }
        [TestMethod]
        public void ManagerPartyRoleNameTest() {
            var name = obj.ManagerPartyRoleName(managerPartyRoleData.Id);
            areEqual(managerPartyRoleData.Name, name);
        }
        [TestMethod]
        public void InitiatorPartyRoleNameTest() {
            var name = obj.InitiatorPartyRoleName(initiatorSignatureData.Id);
            areEqual(initiatorSignatureData.Name, name);
        }
        [TestMethod]
        public void PriorityClassifierNameTest() {
            var name = obj.PriorityClassifierName(priorityClassifierData.Id);
            areEqual(priorityClassifierData.Name, name);
        }

        [TestMethod]
        public async Task OnGetShowThreadsAsyncTest() {
            var page = await obj.OnGetShowThreadsAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            Assert.IsInstanceOfType(page, typeof(RedirectResult));
        }
    }
}