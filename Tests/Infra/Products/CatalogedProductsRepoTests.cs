using System;
using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class CatalogedProductsRepoTests : ProductRepoTests<CatalogedProductsRepo,
        CatalogedProduct, CatalogedProductData> {
        protected override Type getBaseClass() => typeof(EntityRepo<CatalogedProduct, CatalogedProductData>);
        protected override CatalogedProductsRepo getObject(ProductDb c) => new CatalogedProductsRepo(c);
        protected override DbSet<CatalogedProductData> getSet(ProductDb c) => c.CatalogedProducts;
    }
}