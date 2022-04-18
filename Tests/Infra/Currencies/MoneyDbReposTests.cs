using System;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Infra.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Currencies {
    [TestClass]
    public class MoneyDbReposTests : TestsHost {
        [TestInitialize] public void TestInitialize() => type = typeof(MoneyDbRepos);
        [DataTestMethod]
        [DataRow(typeof(IPaymentMethodsRepo))]
        [DataRow(typeof(ICurrencyUsagesRepo))]
        [DataRow(typeof(IExchangeRatesRepo))]
        [DataRow(typeof(IExchangeRulesRepo))]
        [DataRow(typeof(IRateTypesRepo))]
        [DataRow(typeof(ICurrencyRepo))]
        [DataRow(typeof(ICardAssociationsRepo))]
        public void RegisterTest(Type t) => Assert.IsNotNull(GetRepo.Instance(t));
    }
}
