using System;
using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class ProductInclusionsRepoTests : ProductRepoTests<ProductInclusionsRepo,
        IProductInclusionRule, ProductInclusionRuleData> {
        protected override Type getBaseClass() => typeof(PagedRepo<IProductInclusionRule, ProductInclusionRuleData>);
        protected override ProductInclusionsRepo getObject(ProductDb c) => new ProductInclusionsRepo(c);
        protected override DbSet<ProductInclusionRuleData> getSet(ProductDb c) => c.ProductInclusions;
    }
}