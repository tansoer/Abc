using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;
using Abc.Infra.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Roles {

    [TestClass]
    public class PartyRoleConstraintTypesRepoTests :RoleRepoTests<PartyRoleConstraintTypesRepo,
        PartyRoleConstraintType, PartyRoleConstraintTypeData> {

        protected override Type getBaseClass() => typeof(EntityRepo<PartyRoleConstraintType, PartyRoleConstraintTypeData>);

        protected override PartyRoleConstraintTypesRepo getObject(RoleDb c) => new PartyRoleConstraintTypesRepo(c);

        protected override DbSet<PartyRoleConstraintTypeData> getSet(RoleDb c) => c.RoleConstraintTypes;
    }
}