using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;

namespace Abc.Domain.Parties.Identifiers {

    public sealed class IdentifierType : Classifier<IdentifierType> {
        public IdentifierType() : this(null) { }
        public IdentifierType(ClassifierData d) : base(d) => data.ClassifierType = ClassifierType.RegisteredIdentifier;
    }
}