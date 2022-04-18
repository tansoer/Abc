using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;
using Abc.Infra.Quantities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Quantities {

    [TestClass]
    public class UnitRulesRepoTests :QuantityRepoTests<UnitRulesRepo, UnitRules, UnitRulesData> {
        protected override Type getBaseClass() => typeof(EntityRepo<UnitRules, UnitRulesData>);
        protected override UnitRulesRepo getObject(QuantityDb c) => new(c);
        protected override DbSet<UnitRulesData> getSet(QuantityDb c) => c.UnitRules;
    }
}