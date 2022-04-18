using System;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Abc.Infra.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Quantities {

    [TestClass]
    public class QuantityDbReposTests : TestsHost {

        [TestInitialize] public void TestInitialize() => type = typeof(QuantityDbRepos);

        [DataTestMethod]
        [DataRow(typeof(IMeasuresRepo))]
        [DataRow(typeof(IUnitsRepo))]
        [DataRow(typeof(ISystemsOfUnitsRepo))]
        [DataRow(typeof(IUnitTermsRepo))]
        [DataRow(typeof(IUnitFactorsRepo))]
        [DataRow(typeof(IMeasureTermsRepo))]
        public void RegisterTest(Type t) => Assert.IsNotNull(GetRepo.Instance(t));

    }

}
