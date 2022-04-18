using System;
using Abc.Data.Products;
using Abc.Domain.Products.Price;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class PricesRepoTests :ProductRepoTests<PricesRepo, IPrice, PriceData> {
        protected override Type getBaseClass() => typeof(PagedRepo<IPrice, PriceData>);
        protected override PricesRepo getObject(ProductDb c) => new(c);
        protected override DbSet<PriceData> getSet(ProductDb c) => c.Prices;
    }
}