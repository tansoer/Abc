using System;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Infra.Common;
using Abc.Infra.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Currencies {

    [TestClass]
    public class ExchangeRatesRepoTests : MoneyRepoTests<ExchangeRatesRepo, ExchangeRate,
        ExchangeRateData> {

        protected override Type getBaseClass() => typeof(EntityRepo<ExchangeRate, ExchangeRateData>);

        protected override ExchangeRatesRepo getObject(MoneyDb c) => new ExchangeRatesRepo(c);

        protected override DbSet<ExchangeRateData> getSet(MoneyDb c) => c.Rates;

    }

}