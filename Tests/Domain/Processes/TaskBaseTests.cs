using Abc.Data.Parties;
using Abc.Data.Processes;
using Abc.Data.Roles;
using Abc.Data.Rules;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Processes;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Domain.Processes {
    [TestClass] public class TaskBaseTests :AbstractTests<TaskBase, PartyBaseRelationship<TaskData>> {
        private class testClass :TaskBase {
            public testClass(TaskData d) : base(d) { }
        }
        protected override TaskBase createObject() => new testClass(random<TaskData>());
        [TestMethod] public void GetNameTest() => areEqual(obj.Name, obj.GetName());
        [TestMethod] public async Task ThreadTest()
            => await testItemAsync<ThreadData, Thread, IThreadsRepo>(
                obj.threadId, () => obj.Thread.Data, d => new Thread(d));
        [TestMethod] public async Task TypeTest()
            => await testItemAsync<TaskTypeData, TaskType, ITaskTypesRepo>(
                obj.RelationshipTypeId, () => obj.Type.Data, d => new TaskType(d));
        [TestMethod] public async Task ContextTest()
            => await testItemAsync<RuleContextData, RuleContext, IRuleContextsRepo>(
                obj.ruleContextId, () => obj.Context.Data, d => new RuleContext(d));
        [TestMethod] public async Task NextTest()
            => await testItemAsync<TaskData, ITask, ITasksRepo>(
                obj.nextId, () => obj.Next.Data, d => new Abc.Domain.Processes.Task(d));
        [TestMethod] public async Task PreviousTest()
            => await testItemAsync<TaskData, ITask, ITasksRepo>(
                obj.prevId, () => obj.Previous.Data, d => new Abc.Domain.Processes.Task(d));
        [TestMethod] public async Task ActionsTest()
            => await testListAsync<Action, ActionData, IActionsRepo>(
                x => x.TaskId = obj.Id, d => new Action(d));
        [TestMethod] public async Task participantsTest()
            => await testListAsync<TaskParticipant, TaskParticipantData, ITaskParticipantsRepo>(
                x => x.TaskId = obj.Id, d => new TaskParticipant(d));
        [TestMethod] public void ParticipantsTest() =>
            testRelatedList<PartyRole, PartyRoleData, TaskParticipant, IPartyRolesRepo>(
                () => obj.Participants,
                () => obj.participants,
                (d, e) => {
                    d.Id = e.partyRoleId;
                    return new PartyRole(d);
                }, participantsTest, (x, r) => x.Id == r.partyRoleId);
        [TestMethod] public async Task AttributesTest() =>
            await testListAsync<AttributeValue, AttributeValueData, IAttributeValuesRepo>(
                obj, nameof(obj.Attributes),
                x => {
                    x.ProcessElementId = obj.Id;
                    x.Type = AttributeValueType.AttributeValue;
                },
                AttributeValuesFactory.Create);
        [TestMethod] public async Task FromAddressTest()
            => await testItemAsync<PartyContactData, IPartyContact, IPartyContactsRepo>(
                obj.fromAdrId, () => obj.FromAddress.Data, PartyContactFactory.Create);
        [TestMethod] public async Task ToAddressTest()
            => await testItemAsync<PartyContactData, IPartyContact, IPartyContactsRepo>(
                obj.toAdrId, () => obj.ToAddress.Data, PartyContactFactory.Create);
        [TestMethod] public void threadIdTest() => isReadOnly(obj.Data.ThreadId);
        [TestMethod] public void ruleContextIdTest() => isReadOnly(obj.Data.RuleContextId);
        [TestMethod] public void nextIdTest() => isReadOnly(obj.Data.NextElementId);
        [TestMethod] public void prevIdTest() => isReadOnly(obj.Data.PreviousElementId);
        [TestMethod] public void fromAdrIdTest() => isReadOnly(obj.Data.FromPartyAddressId);
        [TestMethod] public void toAdrIdTest() => isReadOnly(obj.Data.ToPartyAddressId);
    }
}