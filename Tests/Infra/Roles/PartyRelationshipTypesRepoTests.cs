using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Infra.Common;
using Abc.Infra.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Roles {

    [TestClass]
    public class PartyRelationshipTypesRepoTests :RoleRepoTests<PartyRelationshipTypesRepo,
        PartyRelationshipType, PartyRelationshipTypeData> {

        protected override Type getBaseClass() => typeof(EntityRepo<PartyRelationshipType, PartyRelationshipTypeData>);

        protected override PartyRelationshipTypesRepo getObject(RoleDb c) => new PartyRelationshipTypesRepo(c);

        protected override DbSet<PartyRelationshipTypeData> getSet(RoleDb c) => c.RelationshipTypes;
    }
}