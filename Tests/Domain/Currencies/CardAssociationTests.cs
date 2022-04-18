using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Currencies {

    [TestClass]
    public class CardAssociationTests : SealedTests<CardAssociation, Entity<CardAssociationData>> {

        protected override CardAssociation createObject() 
            => new CardAssociation(GetRandom.ObjectOf<CardAssociationData>());
    }

}
