using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;
using Abc.Infra.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Roles {

    [TestClass]
    public class AssignedResponsibilitiesRepoTests :RoleRepoTests<AssignedResponsibilitiesRepo,
        AssignedResponsibility, AssignedResponsibilityData> {

        protected override Type getBaseClass() => typeof(EntityRepo<AssignedResponsibility, AssignedResponsibilityData>);

        protected override AssignedResponsibilitiesRepo getObject(RoleDb c) => new AssignedResponsibilitiesRepo(c);

        protected override DbSet<AssignedResponsibilityData> getSet(RoleDb c) => c.AssignedResponsibilities;
    }
}