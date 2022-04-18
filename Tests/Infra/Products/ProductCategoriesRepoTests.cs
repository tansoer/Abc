using System;
using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class ProductCategoriesRepoTests : ProductRepoTests<ProductCategoriesRepo,
        ProductCategory, ProductCategoryData> {

        protected override Type getBaseClass() => typeof(EntityRepo<ProductCategory, ProductCategoryData>);

        protected override ProductCategoriesRepo getObject(ProductDb c) =>
            new ProductCategoriesRepo(c);

        protected override DbSet<ProductCategoryData> getSet(ProductDb c) => c.ProductCategories;

    }

}