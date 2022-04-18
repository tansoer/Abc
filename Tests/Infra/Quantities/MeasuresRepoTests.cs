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
        MeasuresRepoTests : QuantityRepoTests<MeasuresRepo, Measure,
            MeasureData> {
        protected override Type getBaseClass() => typeof(PagedRepo<Measure, MeasureData>);
        protected override MeasuresRepo getObject(QuantityDb c) => new MeasuresRepo(c);
        protected override DbSet<MeasureData> getSet(QuantityDb c) => c.Measures;
    }
}