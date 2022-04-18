using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;

namespace Abc.Domain.Parties.Identifiers {
    public sealed class AuthorityType : Classifier<AuthorityType> {
        public AuthorityType() : this(null) { }
        public AuthorityType(ClassifierData d) : base(d) => data.ClassifierType = ClassifierType.RegistrationAuthority;
    }
}
