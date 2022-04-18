using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Names;

namespace Abc.Domain.Parties {

    public sealed class Organization : Party<OrganizationName> {

        public Organization() : this(null) { }
        public Organization(PartyData d) : base(d) { }

        public OrganizationType Type => item<IClassifiersRepo, IClassifier>(typeId) as OrganizationType;

        internal string typeId => get(Data?.OrganizationTypeId);
    }
}
