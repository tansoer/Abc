using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Facade.Orders;
using Abc.Infra.Common;
using Abc.Infra.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Orders {

    [TestClass]
    public class SalesTaxPoliciesRepoTests :OrderReposTests<SalesTaxPoliciesRepo, SalesTaxPolicy,
        SalesTaxPolicyData> {

        protected override Type getBaseClass() => typeof(EntityRepo<SalesTaxPolicy, SalesTaxPolicyData>);

        protected override SalesTaxPoliciesRepo getObject(OrderDb c) => new(c);

        protected override DbSet<SalesTaxPolicyData> getSet(OrderDb c) => c.SalesTaxPolicies;
    }
}