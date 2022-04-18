using Abc.Data.Classifiers;

namespace Abc.Domain.Classifiers {
    public sealed class ClassifierError :Classifier<ClassifierError> {
        public ClassifierError() : this(null) { }
        public ClassifierError(ClassifierData d) : base(d) { }
    }
}