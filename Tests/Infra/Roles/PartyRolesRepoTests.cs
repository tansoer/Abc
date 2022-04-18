using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;
using Abc.Infra.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Roles {

    [TestClass] public class PartyRolesRepoTests : RoleRepoTests<PartyRolesRepo,
        PartyRole, PartyRoleData> {
        protected override Type getBaseClass()
            => typeof(EntityRepo<PartyRole, PartyRoleData>);
        protected override PartyRolesRepo getObject(RoleDb c) =>
            new PartyRolesRepo(c);
        protected override DbSet<PartyRoleData> getSet(RoleDb c) => c.Roles;
    }
}
