using Abc.Data.Parties;
using Abc.Domain.Classifiers;

namespace Abc.Domain.Parties.Attributes {
    public sealed class PersonEthnicity : BasePartyAttribute<PersonEthnicityData> {
        public PersonEthnicity() : this(null) { }
        public PersonEthnicity(PersonEthnicityData d) : base(d) { }
        public Ethnicity Ethnicity => item<IClassifiersRepo, IClassifier>(EthnicityId) as Ethnicity;
        public string EthnicityId => get(Data?.EthnicityId);
    }
}