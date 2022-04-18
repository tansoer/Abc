using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;
using Abc.Infra.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Roles {
    [TestClass]
    public class PartyRoleTypesRepoTests :RoleRepoTests<PartyRoleTypesRepo,
        PartyRoleType, PartyRoleTypeData> {
        protected override Type getBaseClass()
            => typeof(EntityRepo<PartyRoleType, PartyRoleTypeData>);
        protected override PartyRoleTypesRepo getObject(RoleDb c) =>
            new PartyRoleTypesRepo(c);
        protected override DbSet<PartyRoleTypeData> getSet(RoleDb c) => c.RoleTypes;
    }
}
