using System;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Infra.Common;
using Abc.Infra.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Currencies {

    [TestClass]
    public class ExchangeTypesRepoTests : MoneyRepoTests<ExchangeTypesRepo, RateType,
        RateTypeData> {

        protected override Type getBaseClass() => typeof(EntityRepo<RateType, RateTypeData>);

        protected override ExchangeTypesRepo getObject(MoneyDb c) => new ExchangeTypesRepo(c);

        protected override DbSet<RateTypeData> getSet(MoneyDb c) => c.RateTypes;

    }

}