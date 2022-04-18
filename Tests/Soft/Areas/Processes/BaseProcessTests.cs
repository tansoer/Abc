using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Common;
using Abc.Data.Parties;
using Abc.Data.Processes;
using Abc.Data.Roles;
using Abc.Data.Rules;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using Abc.Facade.Common;
using Abc.Infra.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Processes {

    public abstract class BaseProcessTests<TView, TData> :BaseAcceptanceTests<TView, TData, ProcessDb>
        where TData : EntityBaseData where TView : EntityBaseView {
        protected override void doOnCreated(ProcessDb c) => clearAll(c);
        [TestCleanup]
        public override void TestCleanup() {
            base.TestCleanup();
            clearAll(db);
        }
        protected void clearAll(ProcessDb c) {
            clear(c.Processes);
            clear(c.ProcessTypes);
            clear(c.Threads);
            clear(c.ThreadTypes);
            clear(c.Tasks);
            clear(c.TaskTypes);
            clear(c.Actions);
            clear(c.ActionTypes);
            clear(c.Outcomes);
            clear(c.OutcomeTypes);
            clear(c.AttributeValues);
            clear(c.ActionApprovers);
            clear(c.OutcomeApprovers);
            clear(c.AttributeTypes);
            clear(c.TaskParticipants);
            clear(classifiersDb.Classifiers, classifiersDb);
        }
        protected void addProcess(string id) => addRandomRecord<ProcessData>(id);
        protected void addProcessType(string id) => addRandomRecord<ProcessTypeData>(id);
        protected void addThread(string id) => addRandomRecord<ThreadData>(id);
        protected void addThreadType(string id) => addRandomRecord<ThreadTypeData>(id);
        protected void addTask(string id) => addRandomRecord<TaskData>(id);
        protected void addTaskType(string id) => addRandomRecord<TaskTypeData>(id);
        protected void addAction(string id) => addRandomRecord<ActionData>(id);
        protected void addActionType(string id) => addRandomRecord<ActionTypeData>(id);
        protected void addOutcome(string id) => addRandomRecord<OutcomeData>(id);
        protected void addOutcomeType(string id) => addRandomRecord<OutcomeTypeData>(id);
        protected void addActionStatusClassifier(string id) {
            var d = random<ClassifierData>();
            d.Id = id;
            d.ClassifierType = ClassifierType.ActionStatus;
            add<IClassifiersRepo, IClassifier>(ClassifierFactory.Create(d));
        }
        protected void addPartySignature(string id) {
            var d = GetRandom.ObjectOf<PartySignatureData>();
            d.Id = id;
            add<IPartySignaturesRepo, PartySignature>(new PartySignature(d));
        }
        protected void addRuleContext(string id) {
            var d = GetRandom.ObjectOf<RuleContextData>();
            d.Id = id;
            add<IRuleContextsRepo, RuleContext>(new RuleContext(d));
        }
        protected void addRuleSet(string id) {
            var d = GetRandom.ObjectOf<RuleSetData>();
            d.Id = id;
            add<IRuleSetsRepo, IRuleSet>(new RuleSet(d));
        }
        protected void addPartyRole(string id) {
            var d = GetRandom.ObjectOf<PartyRoleData>();
            d.Id = id;
            add<IPartyRolesRepo, PartyRole>(new PartyRole(d));
        }
        protected void addPriorityClassifier(string id) {
            var d = random<ClassifierData>();
            d.Id = id;
            d.ClassifierType = ClassifierType.ProcessPriority;
            add<IClassifiersRepo, IClassifier>(ClassifierFactory.Create(d));
        }
        protected void addPartyContact(string id) {
            var d = GetRandom.ObjectOf<PartyContactData>();
            d.Id = id;
            add<IPartyContactsRepo, IPartyContact>(PartyContactFactory.Create(d));
        }
    }

}
