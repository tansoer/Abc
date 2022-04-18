using Abc.Data.Processes;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Processes;
using Abc.Domain.Common;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using System.Collections.Generic;

namespace Abc.Domain.Processes {

    public sealed class Process :Entity<ProcessData>, IProcessElement {
        public Process() : this(null) { }
        public Process(ProcessData d) : base(d) { }
        public ProcessType Type => item<IProcessTypesRepo, ProcessType>(typeId);
        public PartyRole Manager => item<IPartyRolesRepo, PartyRole>(managerId);
        public PartyRole Initiator => item<IPartyRolesRepo, PartyRole>(initiatorId);
        public ProcessPriority Priority => item<IClassifiersRepo, IClassifier>(priorityId) as ProcessPriority;
        public IReadOnlyList<Thread> Threads => list<IThreadsRepo, Thread>(processId, Id);
        public RuleContext Context => item<IRuleContextsRepo, RuleContext>(contextId);
        internal IReadOnlyList<AttributeValue> attributeValues => list<IAttributeValuesRepo, AttributeValue>(elementId, Id);
        public IReadOnlyList<Attribute> Attributes => list<Attribute, AttributeValue>(attributeValues);
        internal string contextId => get(Data?.RuleContextId);
        internal string elementId => nameOf<AttributeValueData>(x => x.ProcessElementId);
        internal string typeId => get(Data?.ProcessTypeId);
        internal string managerId => get(Data?.ManagerPartyRoleId);
        internal string initiatorId => get(Data?.InitiatorPartyRoleId);
        internal string priorityId => get(Data?.PriorityClassifierId);
        internal string processId => nameOf<ThreadData>(x => x.ProcessId);
    }
}
