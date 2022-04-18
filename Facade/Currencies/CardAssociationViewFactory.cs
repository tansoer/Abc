using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Common;

namespace Abc.Facade.Currencies {

    public sealed class CardAssociationViewFactory :
        AbstractViewFactory<CardAssociationData, CardAssociation, CardAssociationView> {
        internal protected override CardAssociation toObject(CardAssociationData d)
            => new CardAssociation(d);

    }
}
