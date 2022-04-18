using System;
using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class ProductSetsRepoTests : ProductRepoTests<ProductSetsRepo,
        ProductSet, ProductSetData> {

        protected override Type getBaseClass() => typeof(EntityRepo<ProductSet, ProductSetData>);

        protected override ProductSetsRepo getObject(ProductDb c) => new ProductSetsRepo(c);

        protected override DbSet<ProductSetData> getSet(ProductDb c) => c.ProductSets;

    }

}