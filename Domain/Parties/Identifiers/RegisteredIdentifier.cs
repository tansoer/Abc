using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Attributes;

namespace Abc.Domain.Parties.Identifiers {

    public sealed class RegisteredIdentifier : BasePartyAttribute<RegisteredIdentifierData> {
        public RegisteredIdentifier() : this(null) { }
        public RegisteredIdentifier(RegisteredIdentifierData d) : base(d) { }

        public IdentifierType Type => item<IClassifiersRepo, IClassifier>(typeId) as IdentifierType;
        public RegistrationAuthority Authority => item<IRegistrationAuthoritiesRepo, RegistrationAuthority>(authorityId);
        public string Identifier => base.Name;
        public override string Name => get(Type?.Name);
        internal string typeId => get(Data.RegisteredIdentifierTypeId);
        internal string authorityId => get(Data.RegistrationAuthorityId);
    }
}