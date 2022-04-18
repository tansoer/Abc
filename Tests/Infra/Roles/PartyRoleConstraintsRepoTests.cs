using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;
using Abc.Infra.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Roles {

    [TestClass]
    public class PartyRoleConstraintsRepoTests :RoleRepoTests<PartyRoleConstraintsRepo,
        PartyRoleConstraint, PartyRoleConstraintData> {

        protected override Type getBaseClass() => typeof(EntityRepo<PartyRoleConstraint, PartyRoleConstraintData>);

        protected override PartyRoleConstraintsRepo getObject(RoleDb c) => new PartyRoleConstraintsRepo(c);

        protected override DbSet<PartyRoleConstraintData> getSet(RoleDb c) => c.RoleConstraints;
    }
}