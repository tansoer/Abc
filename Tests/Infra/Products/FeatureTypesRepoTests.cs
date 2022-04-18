using System;
using Abc.Data.Products;
using Abc.Domain.Products.Features;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class FeatureTypesRepoTests : ProductRepoTests<FeatureTypesRepo,
        FeatureType, FeatureTypeData> {

        protected override Type getBaseClass() => typeof(EntityRepo<FeatureType, FeatureTypeData>);

        protected override FeatureTypesRepo getObject(ProductDb c) => new FeatureTypesRepo(c);

        protected override DbSet<FeatureTypeData> getSet(ProductDb c) => c.FeatureTypes;

    }

}