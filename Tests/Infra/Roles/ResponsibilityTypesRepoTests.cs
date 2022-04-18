using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;
using Abc.Infra.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Roles {

    [TestClass]
    public class ResponsibilityTypesRepoTests :RoleRepoTests<ResponsibilityTypesRepo,
        ResponsibilityType, ResponsibilityTypeData> {

        protected override Type getBaseClass() => typeof(EntityRepo<ResponsibilityType, ResponsibilityTypeData>);

        protected override ResponsibilityTypesRepo getObject(RoleDb c) => new ResponsibilityTypesRepo(c);

        protected override DbSet<ResponsibilityTypeData> getSet(RoleDb c) => c.ResponsibilityTypes;
    }
}