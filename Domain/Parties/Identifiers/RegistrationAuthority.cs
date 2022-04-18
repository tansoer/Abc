using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Attributes;

namespace Abc.Domain.Parties.Identifiers {

    public sealed class RegistrationAuthority : BasePartyAttribute<RegistrationAuthorityData> {
        public RegistrationAuthority() : this(null) { }
        public RegistrationAuthority(RegistrationAuthorityData d) : base(d) { }
        public AuthorityType Type => item<IClassifiersRepo, IClassifier>(typeId) as AuthorityType;
        public override Organization Party => base.Party as Organization;
        internal string typeId => get(Data?.AuthorityTypeId);
    }
}