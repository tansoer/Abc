using Abc.Domain.Common;
using Abc.Data.Processes;
using System.Collections.Generic;
using Abc.Domain.Rules;

namespace Abc.Domain.Processes {
    public sealed class ProcessType :EntityType<IProcessTypesRepo, ProcessType, ProcessTypeData>, IProcessElementType {
        public ProcessType() : this(null) { }
        public ProcessType(ProcessTypeData d) : base(d) { }
        public override ProcessType BaseType => base.BaseType;
        public IReadOnlyList<ThreadType> Threads => list<IThreadTypesRepo, ThreadType>(processId, Id);
        public IReadOnlyList<AttributeType> Attributes => list<IAttributeTypesRepo, AttributeType>(elementTypeId, Id);
        public bool IsSuitable(IProcessElement p) => isSuitable(p?.Context);
        internal bool isSuitable(RuleContext c) => (Requirements?.Evaluate(c) as BooleanVariable)?.Value ?? false;

        public IRuleSet Requirements => item<IRuleSetsRepo, IRuleSet>(ruleSetId);
        internal string ruleSetId => get(Data?.RuleSetId);
        internal static string elementTypeId => nameOf<AttributeTypeData>(x => x.ElementTypeId);
        internal static string processId => nameOf<ThreadTypeData>(x => x.ProcessTypeId);
    }
}
