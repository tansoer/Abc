using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;
using Abc.Infra.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Roles {

    [TestClass]
    public class PartyRelationshipsRepoTests :RoleRepoTests<PartyRelationshipsRepo,
        PartyRelationship, PartyRelationshipData> {

        protected override Type getBaseClass() => typeof(EntityRepo<PartyRelationship, PartyRelationshipData>);

        protected override PartyRelationshipsRepo getObject(RoleDb c) => new PartyRelationshipsRepo(c);

        protected override DbSet<PartyRelationshipData> getSet(RoleDb c) => c.Relationships;
    }
}