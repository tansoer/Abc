using Abc.Data.Processes;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using System.Collections.Generic;

namespace Abc.Domain.Processes {
    public sealed class TaskType : PartyRelationshipBaseType<TaskTypeData>, IProcessElementType<TaskType> {
        public TaskType() : this(null) { }
        public TaskType(TaskTypeData d) : base(d) { }
        public TaskType BaseType => item<ITaskTypesRepo, TaskType>(baseTypeId);
        public ThreadType Thread => item<IThreadTypesRepo, ThreadType>(threadTypeId);
        public TaskType Next => item<ITaskTypesRepo, TaskType>(nextId);
        public TaskType Previous => item<ITaskTypesRepo, TaskType>(prevId);
        public IReadOnlyList<AttributeType> Attributes => list<IAttributeTypesRepo, AttributeType>(elementTypeId, Id);
        public bool IsSuitable(IProcessElement e) => isSuitable(e?.Context);
        internal bool isSuitable(RuleContext c)
            => (Requirements?.Evaluate(c) as BooleanVariable)?.Value ?? false;
        internal string elementTypeId => nameOf<AttributeTypeData>(x => x.ElementTypeId);
        internal string threadTypeId => get(Data?.ThreadTypeId);
        internal string nextId => get(Data?.NextElementId);
        internal string prevId => get(Data?.PreviousElementId);
        internal string baseTypeId => get(Data?.BaseTypeId);
    }
}
