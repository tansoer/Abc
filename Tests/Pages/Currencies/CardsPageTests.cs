using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Abc.Pages.Currencies;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Pages.Currencies {

    [TestClass]
    public class CardsPageTests : SealedViewPageTests<
        CardsPage,
        ICardAssociationsRepo,
        CardAssociation,
        CardAssociationView,
        CardAssociationData> {

        protected override Type getBaseClass() =>
            typeof(ViewPage<CardsPage, ICardAssociationsRepo,
                CardAssociation, CardAssociationView, CardAssociationData>);

        private class testRepo
            : mockRepo<CardAssociation, CardAssociationData>,
                ICardAssociationsRepo { }

        private testRepo Repo;

        protected override CardsPage createObject() {
            Repo = new testRepo();
            addRandomCards();
            return new CardsPage(Repo);
        }
        private void addRandomCards() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                Repo.Add(new CardAssociation(GetRandom.ObjectOf<CardAssociationData>()));
        }
        protected override string pageUrl => MoneyUrls.Cards;
        protected override string pageTitle => MoneyTitles.Cards;
        protected override CardAssociation toObject(CardAssociationData d) => new (d);
    }
}
