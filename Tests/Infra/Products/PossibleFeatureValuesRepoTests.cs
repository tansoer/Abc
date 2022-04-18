using System;
using Abc.Data.Products;
using Abc.Domain.Products.Features;
using Abc.Infra.Common;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {

    [TestClass]
    public class PossibleFeatureValuesRepoTests : ProductRepoTests<PossibleFeatureValuesRepo,
        PossibleFeatureValue, PossibleFeatureValueData> {

        protected override Type getBaseClass() => typeof(EntityRepo<PossibleFeatureValue, PossibleFeatureValueData>);

        protected override PossibleFeatureValuesRepo getObject(ProductDb c) => new PossibleFeatureValuesRepo(c);

        protected override DbSet<PossibleFeatureValueData> getSet(ProductDb c) => c.PossibleFeatureValues;

    }

}