using System;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class ProductsRepoTests : ProductRepoTests<ProductsRepo,
        IProduct, ProductData> {
        protected override Type getBaseClass() => typeof(PagedRepo<IProduct, ProductData>);
        protected override ProductsRepo getObject(ProductDb c) => new ProductsRepo(c);
        protected override DbSet<ProductData> getSet(ProductDb c) => c.Products;
    }
}