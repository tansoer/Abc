using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Parties.Contacts;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Domain.Processes {
    public abstract class TaskBase 
        : PartyBaseRelationship<TaskData>, ITask, IProcessElement<ITask> {
        protected TaskBase() : this(null) { }
        protected TaskBase(TaskData d) : base(d) { }
        public string GetName() => Name;
        public ITask Next => item<ITasksRepo, ITask>(nextId);
        public ITask Previous => item<ITasksRepo, ITask>(prevId);
        public Thread Thread => item<IThreadsRepo, Thread>(threadId);
        public TaskType Type => item<ITaskTypesRepo, TaskType>(RelationshipTypeId);
        public IReadOnlyList<Action> Actions => list<IActionsRepo, Action>(taskId, Id);
        public IPartyContact FromAddress => item<IPartyContactsRepo, IPartyContact>(fromAdrId);
        public IPartyContact ToAddress => item<IPartyContactsRepo, IPartyContact>(toAdrId);
        public IReadOnlyList<PartyRole> Participants => participants.Select(x => x.PartyRole).ToList().AsReadOnly();
        public RuleContext Context => item<IRuleContextsRepo, RuleContext>(ruleContextId);
        public IReadOnlyList<Attribute> Attributes => list<Attribute, AttributeValue>(attributeValues);
        internal IReadOnlyList<AttributeValue> attributeValues => list<IAttributeValuesRepo, AttributeValue>(elementId, Id);
        internal static string elementId => nameOf<AttributeValueData>(x => x.ProcessElementId);
        internal IReadOnlyList<TaskParticipant> participants => list<ITaskParticipantsRepo, TaskParticipant>(taskId, Id);
        internal string threadId => get(Data?.ThreadId);
        internal string ruleContextId => get(Data?.RuleContextId);
        internal string nextId => get(Data?.NextElementId);
        internal string prevId => get(Data?.PreviousElementId);
        internal string fromAdrId => get(Data?.FromPartyAddressId);
        internal string toAdrId => get(Data?.ToPartyAddressId);
        internal static string taskId => nameOf<ActionData>(x => x.TaskId);
    }
    public interface ITask: IEntity<TaskData> {
        string GetName();
        public Thread Thread { get; }
        public IReadOnlyList<Action> Actions { get; }
        public IReadOnlyList<PartyRole> Participants { get; }
        public TaskType Type { get; }
        public IPartyContact FromAddress { get; }
        public IPartyContact ToAddress { get; }
    }
    public interface ITasksRepo: IRepo<ITask> { }
}
