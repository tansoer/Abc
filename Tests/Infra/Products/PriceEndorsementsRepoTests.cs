using Abc.Data.Products;
using Abc.Domain.Products.Price;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class PriceEndorsementsRepoTests : ProductRepoTests<PriceEndorsementsRepo,
        PriceEndorsement, PriceEndorsementData> {
        protected override Type getBaseClass() => typeof(EntityRepo<PriceEndorsement, PriceEndorsementData>);

        protected override PriceEndorsementsRepo getObject(ProductDb c) => new PriceEndorsementsRepo(c);

        protected override DbSet<PriceEndorsementData> getSet(ProductDb c) => c.PriceEndorsements;

    }

}