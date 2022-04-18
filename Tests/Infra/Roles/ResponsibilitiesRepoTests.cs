using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;
using Abc.Infra.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Roles {

    [TestClass]
    public class ResponsibilitiesRepoTests :RoleRepoTests<ResponsibilitiesRepo,
        Responsibility, ResponsibilityData> {

        protected override Type getBaseClass() => typeof(EntityRepo<Responsibility, ResponsibilityData>);

        protected override ResponsibilitiesRepo getObject(RoleDb c) => new ResponsibilitiesRepo(c);

        protected override DbSet<ResponsibilityData> getSet(RoleDb c) => c.Responsibilities;
    }
}