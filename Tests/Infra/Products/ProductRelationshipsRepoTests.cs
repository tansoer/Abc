using System;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class ProductRelationshipsRepoTests : ProductRepoTests<ProductRelationshipsRepo,
        ProductRelationship, ProductRelationshipData> {

        protected override Type getBaseClass() => typeof(EntityRepo<ProductRelationship, ProductRelationshipData>);

        protected override ProductRelationshipsRepo getObject(ProductDb c) => new ProductRelationshipsRepo(c);

        protected override DbSet<ProductRelationshipData> getSet(ProductDb c) => c.ProductRelationships;

    }

}