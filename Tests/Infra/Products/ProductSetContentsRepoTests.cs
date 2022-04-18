using System;
using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class ProductSetContentsRepoTests : ProductRepoTests<ProductSetContentsRepo,
        ProductSetContent, ProductSetContentData> {
        protected override Type getBaseClass() => typeof(EntityRepo<ProductSetContent, ProductSetContentData>);

        protected override ProductSetContentsRepo getObject(ProductDb c) => new ProductSetContentsRepo(c);

        protected override DbSet<ProductSetContentData> getSet(ProductDb c) => c.ProductSetContents;

    }

}