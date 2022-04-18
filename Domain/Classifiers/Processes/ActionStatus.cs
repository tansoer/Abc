using Abc.Data.Classifiers;

namespace Abc.Domain.Classifiers.Processes {
    public sealed class ActionStatus :Classifier<ActionStatus> {
        public ActionStatus() : this(null) { }
        public ActionStatus(ClassifierData d) : base(d) => data.ClassifierType = ClassifierType.ActionStatus;
    }
}

