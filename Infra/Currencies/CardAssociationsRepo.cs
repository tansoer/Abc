using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Infra.Common;

namespace Abc.Infra.Currencies {
    public sealed class CardAssociationsRepo : EntityRepo<CardAssociation, CardAssociationData>, ICardAssociationsRepo {
        public CardAssociationsRepo(MoneyDb c = null) : base(c, c?.CardAssociations) { }
    }
}