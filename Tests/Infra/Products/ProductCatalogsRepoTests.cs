using System;
using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class ProductCatalogsRepoTests : ProductRepoTests<ProductCatalogsRepo,
        ProductCatalog, ProductCatalogData> {

        protected override Type getBaseClass() => typeof(EntityRepo<ProductCatalog, ProductCatalogData>);

        protected override ProductCatalogsRepo getObject(ProductDb c) => new ProductCatalogsRepo(c);

        protected override DbSet<ProductCatalogData> getSet(ProductDb c) => c.ProductCatalogs;

    }

}