using System;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Features;
using Abc.Infra.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {
    [TestClass]
    public class ProductDbReposTests : TestsHost {

        [TestInitialize] public void TestInitialize() => type = typeof(ProductDbRepos);

        [DataTestMethod]
        [DataRow(typeof(IProductsRepo))]
        [DataRow(typeof(IProductTypesRepo))]
        [DataRow(typeof(IFeaturesRepo))]
        [DataRow(typeof(IFeatureTypesRepo))]
        [DataRow(typeof(IPossibleFeatureValuesRepo))]
        public void RegisterTest(Type t) => Assert.IsNotNull(GetRepo.Instance(t));

    }

}

