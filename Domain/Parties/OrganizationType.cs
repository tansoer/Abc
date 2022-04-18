using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;

namespace Abc.Domain.Parties {

    public sealed class OrganizationType : Classifier<OrganizationType> {

        public OrganizationType() : this(null) { }
        public OrganizationType(ClassifierData d) : base(d) => data.ClassifierType = ClassifierType.Organization;
    }
}
