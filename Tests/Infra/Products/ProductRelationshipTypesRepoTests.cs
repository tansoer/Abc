using System;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class ProductRelationshipTypesRepoTests : ProductRepoTests<ProductRelationshipTypesRepo,
        ProductRelationshipType, ProductRelationshipTypeData> {

        protected override Type getBaseClass() => typeof(EntityRepo<ProductRelationshipType, ProductRelationshipTypeData>);

        protected override ProductRelationshipTypesRepo getObject(ProductDb c) => new ProductRelationshipTypesRepo(c);

        protected override DbSet<ProductRelationshipTypeData> getSet(ProductDb c) => c.ProductRelationshipTypes;

    }

}