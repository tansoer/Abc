using System;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Infra.Common;
using Abc.Infra.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Currencies {

    [TestClass]
    public class ExchangeRulesRepoTests : MoneyRepoTests<ExchangeRulesRepo, ExchangeRule,
        ExchangeRuleData> {
        protected override Type getBaseClass() => typeof(EntityRepo<ExchangeRule, ExchangeRuleData>);
        protected override ExchangeRulesRepo getObject(MoneyDb c) => new ExchangeRulesRepo(c);
        protected override DbSet<ExchangeRuleData> getSet(MoneyDb c) => c.RateRules;
    }
}