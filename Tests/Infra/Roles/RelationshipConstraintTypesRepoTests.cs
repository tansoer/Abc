using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;
using Abc.Infra.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Roles {

    [TestClass]
    public class RelationshipConstraintTypesRepoTests :RoleRepoTests<RelationshipConstraintTypesRepo,
        RelationshipConstraintType, RelationshipConstraintTypeData> {

        protected override Type getBaseClass() => typeof(EntityRepo<RelationshipConstraintType, RelationshipConstraintTypeData>);

        protected override RelationshipConstraintTypesRepo getObject(RoleDb c) => new RelationshipConstraintTypesRepo(c);

        protected override DbSet<RelationshipConstraintTypeData> getSet(RoleDb c) => c.RelationshipConstraintTypes;
    }
}