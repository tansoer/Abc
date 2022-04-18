using Abc.Data.Orders;
using Abc.Domain.Orders.Terms;
using Abc.Facade.Orders;
using Abc.Infra.Common;
using Abc.Infra.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Orders {

    [TestClass]
    public class TermsAndConditionsRepoTests :OrderReposTests<TermsAndConditionsRepo, TermsAndConditions,
        TermsAndConditionsData> {

        protected override Type getBaseClass() => typeof(EntityRepo<TermsAndConditions, TermsAndConditionsData>);

        protected override TermsAndConditionsRepo getObject(OrderDb c) => new TermsAndConditionsRepo(c);

        protected override DbSet<TermsAndConditionsData> getSet(OrderDb c) => c.TermsAndConditions;
    }
}