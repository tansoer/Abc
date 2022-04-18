using Abc.Data.Parties;
using Abc.Domain.Parties.Identifiers;
using Abc.Facade.Common;

namespace Abc.Facade.Parties {
    public sealed class RegisteredIdentifierViewFactory :
        AbstractViewFactory<RegisteredIdentifierData, RegisteredIdentifier, RegisteredIdentifierView> {
        protected internal override RegisteredIdentifier toObject(RegisteredIdentifierData d)
            => new RegisteredIdentifier(d);
    }
}
