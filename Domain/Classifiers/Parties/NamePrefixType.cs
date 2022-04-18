using Abc.Data.Classifiers;

namespace Abc.Domain.Classifiers.Parties {
    public sealed class NamePrefixType :Classifier<NamePrefixType> {
        public NamePrefixType() : this(null) { }
        public NamePrefixType(ClassifierData d) : base(d) => data.ClassifierType = ClassifierType.PersonNamePrefix;
    }
}