using Abc.Data.Classifiers;

namespace Abc.Domain.Classifiers.Parties {
    public sealed class NameSuffixType :Classifier<NameSuffixType> {
        public NameSuffixType() : this(null) { }
        public NameSuffixType(ClassifierData d) : base(d) => data.ClassifierType = ClassifierType.PersonNameSuffix;
    }
}