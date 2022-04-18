using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;
using Abc.Infra.Quantities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Quantities {

    [TestClass]
    public class UnitTermsRepoTests :QuantityRepoTests<UnitTermsRepo, UnitTerm, CommonTermData> {
        protected override Type getBaseClass() => typeof(EntityRepo<UnitTerm, CommonTermData>);
        protected override UnitTermsRepo getObject(QuantityDb c) => new (c);
        protected override DbSet<CommonTermData> getSet(QuantityDb c) => c.CommonTerms;
    }
}