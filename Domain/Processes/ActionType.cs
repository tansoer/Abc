using Abc.Data.Processes;

namespace Abc.Domain.Processes {
    public sealed class ActionType :
        ProcessElementType<IActionTypesRepo, ActionType, ActionTypeData > {
        public ActionType() : this(null) { }
        public ActionType(ActionTypeData d) : base(d) { }
        public override ActionType BaseType => base.BaseType;
        public TaskType Task => item<ITaskTypesRepo, TaskType>(taskId);
        internal string taskId => get(Data?.TaskTypeId);
    }
}
