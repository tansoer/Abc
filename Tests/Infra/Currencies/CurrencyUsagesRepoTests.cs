using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Infra.Common;
using Abc.Infra.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Currencies {

    [TestClass]
    public class CurrencyUsagesRepoTests 
        : MoneyRepoTests<CurrencyUsagesRepo, CurrencyUsage,CurrencyUsageData> {
        protected override Type getBaseClass() => typeof(EntityRepo<CurrencyUsage, CurrencyUsageData>);
        protected override CurrencyUsagesRepo getObject(MoneyDb c) => new CurrencyUsagesRepo(c);
        protected override DbSet<CurrencyUsageData> getSet(MoneyDb c) => c.CurrencyUsages;
    }
}