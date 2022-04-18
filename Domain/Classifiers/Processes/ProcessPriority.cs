using Abc.Data.Classifiers;

namespace Abc.Domain.Classifiers.Processes {
    public sealed class ProcessPriority :Classifier<ProcessPriority> {
        public ProcessPriority() : this(null) { }
        public ProcessPriority(ClassifierData d) : base(d) => data.ClassifierType = ClassifierType.ProcessPriority;
    }
}
