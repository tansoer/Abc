using System;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Infra.Common;
using Abc.Infra.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Currencies {

    [TestClass]
    public class CurrencyRepoTests :MoneyRepoTests<CurrencyRepo, Currency, CurrencyData> {
        protected override Type getBaseClass() => typeof(EntityRepo<Currency, CurrencyData>);
        protected override CurrencyRepo getObject(MoneyDb c) => new CurrencyRepo(c);
        protected override DbSet<CurrencyData> getSet(MoneyDb c) => c.Currencies;
    }
}