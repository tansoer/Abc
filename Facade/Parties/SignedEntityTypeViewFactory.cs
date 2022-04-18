using Abc.Data.Parties;
using Abc.Domain.Parties.Signatures;
using Abc.Facade.Common;

namespace Abc.Facade.Parties {
    public sealed class SignedEntityTypeViewFactory : 
        AbstractViewFactory<SignedEntityTypeData, SignedEntityType, SignedEntityTypeView> {

        internal protected override SignedEntityType toObject(SignedEntityTypeData d)
            => new SignedEntityType(d);
    }
}
