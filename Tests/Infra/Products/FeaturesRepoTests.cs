using System;
using Abc.Data.Products;
using Abc.Domain.Products.Features;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class FeaturesRepoTests : ProductRepoTests<FeaturesRepo,
        Feature, FeatureData> {

        protected override Type getBaseClass() => typeof(EntityRepo<Feature, FeatureData>);

        protected override FeaturesRepo getObject(ProductDb c) => new FeaturesRepo(c);

        protected override DbSet<FeatureData> getSet(ProductDb c) => c.Features;

    }

}