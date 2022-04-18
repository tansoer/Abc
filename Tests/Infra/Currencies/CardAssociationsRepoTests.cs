using System;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Infra.Common;
using Abc.Infra.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Currencies {

    [TestClass]
    public class CardAssociationsRepoTests : MoneyRepoTests<CardAssociationsRepo, CardAssociation,
        CardAssociationData> {

        protected override Type getBaseClass() => typeof(EntityRepo<CardAssociation, CardAssociationData>);

        protected override CardAssociationsRepo getObject(MoneyDb c) => new CardAssociationsRepo(c);

        protected override DbSet<CardAssociationData> getSet(MoneyDb c) => c.CardAssociations;

    }

}
