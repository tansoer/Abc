using System;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class ProductTypesRepoTests : ProductRepoTests<ProductTypesRepo,
        IProductType, ProductTypeData> {
        protected override Type getBaseClass() => typeof(PagedRepo<IProductType, ProductTypeData>);
        protected override ProductTypesRepo getObject(ProductDb c) => new ProductTypesRepo(c);
        protected override DbSet<ProductTypeData> getSet(ProductDb c) => c.ProductTypes;
    }
}