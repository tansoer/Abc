using System;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;
using Abc.Infra.Quantities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Quantities {

    [TestClass]
    public class
        SystemsOfUnitsRepoTests : QuantityRepoTests<SystemsOfUnitsRepo, SystemOfUnits,
            SystemOfUnitsData> {

        protected override Type getBaseClass() => typeof(EntityRepo<SystemOfUnits, SystemOfUnitsData>);

        protected override SystemsOfUnitsRepo getObject(QuantityDb c) => new SystemsOfUnitsRepo(c);

        protected override DbSet<SystemOfUnitsData> getSet(QuantityDb c) => c.SystemsOfUnits;

    }

}
