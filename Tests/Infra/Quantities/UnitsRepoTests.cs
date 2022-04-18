using System;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;
using Abc.Infra.Quantities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Quantities {

    [TestClass]
    public class UnitsRepoTests : QuantityRepoTests<UnitsRepo, Unit, UnitData> {
        protected override Type getBaseClass() => typeof(PagedRepo<Unit, UnitData>);
        protected override UnitsRepo getObject(QuantityDb c) => new UnitsRepo(c);
        protected override DbSet<UnitData> getSet(QuantityDb c) => c.Units;
    }
}