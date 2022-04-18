using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;
using Abc.Infra.Quantities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Quantities {

    [TestClass]
    public class MeasureTermsRepoTests 
        :QuantityRepoTests<MeasureTermsRepo, MeasureTerm, CommonTermData> {
        protected override Type getBaseClass() => typeof(EntityRepo<MeasureTerm, CommonTermData>);
        protected override MeasureTermsRepo getObject(QuantityDb c) => new(c);
        protected override DbSet<CommonTermData> getSet(QuantityDb c) => c.CommonTerms;
    }
}