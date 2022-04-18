using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Roles;

namespace Abc.Domain.Parties.Signatures {
    public abstract class BasePartySignature<T> :BasePartyAttribute<T> where T : PartySignatureBaseData, new() {
        protected internal BasePartySignature(T d = null) : base(d) { }
        public Authentication Authentication => item<IAuthenticationsRepo, Authentication>(PartyAuthenticationId);
        public PartySummary PartySummary {
            get {
                var i = item<IPartySummariesRepo, IPartySummary>(PartySummaryId);
                var s = i as PartySummary;
                return s;
            }
        }
        public IEntity SignedEntity => new GetSignedEntity(SignedEntityType).ById(SignedEntityId);
        public SignedEntityType SignedEntityType => item<ISignedEntityTypesRepo, SignedEntityType>(SignedEntityTypeId);
        public bool IsSigned() {
            if (IsUnspecified) return false;
            if (isNull(PartyId)) return false;
            if (isNull(ValidFrom)) return false;
            return isNull(ValidTo, false);
        }
        public string PartyAuthenticationId => get(Data?.PartyAuthenticationId);
        public string SignedEntityId => get(Data?.SignedEntityId);
        public string PartySummaryId => get(Data?.PartySummaryId);
        public string SignedEntityTypeId => get(Data?.SignedEntityTypeId);
    }
}
