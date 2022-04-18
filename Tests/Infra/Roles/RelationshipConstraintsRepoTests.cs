using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;
using Abc.Infra.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Roles {

    [TestClass]
    public class RelationshipConstraintsRepoTests :RoleRepoTests<RelationshipConstraintsRepo,
        RelationshipConstraint, RelationshipConstraintData> {

        protected override Type getBaseClass() => typeof(EntityRepo<RelationshipConstraint, RelationshipConstraintData>);

        protected override RelationshipConstraintsRepo getObject(RoleDb c) => new RelationshipConstraintsRepo(c);

        protected override DbSet<RelationshipConstraintData> getSet(RoleDb c) => c.RelationshipConstraints;
    }
}