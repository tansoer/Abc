using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Infra.Common;
using Abc.Infra.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Inventory {

    [TestClass]
    public class RestockPoliciesRepoTests :InventoryRepoTests<RestockPoliciesRepo, RestockPolicy,
        RestockPolicyData> {
        protected override Type getBaseClass() => typeof(PagedRepo<RestockPolicy, RestockPolicyData>);
        protected override RestockPoliciesRepo getObject(InventoryDb c) => new (c);
        protected override DbSet<RestockPolicyData> getSet(InventoryDb c) => c.RestockPolicies;
    }
}