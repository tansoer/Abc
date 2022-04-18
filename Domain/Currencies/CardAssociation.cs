using Abc.Data.Currencies;
using Abc.Domain.Common;

namespace Abc.Domain.Currencies {
    public sealed class CardAssociation : Entity<CardAssociationData> {
        public CardAssociation() : this(null) { }
        public CardAssociation(CardAssociationData d) : base(d) { }

    }
}