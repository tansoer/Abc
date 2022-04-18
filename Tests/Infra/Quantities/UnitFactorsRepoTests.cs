using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;
using Abc.Infra.Quantities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Quantities {

    [TestClass]
    public class UnitFactorsRepoTests :QuantityRepoTests<UnitFactorsRepo, UnitFactor, UnitFactorData> {
        protected override Type getBaseClass() => typeof(EntityRepo<UnitFactor, UnitFactorData>);
        protected override UnitFactorsRepo getObject(QuantityDb c) => new(c);
        protected override DbSet<UnitFactorData> getSet(QuantityDb c) => c.UnitFactors;
    }
}