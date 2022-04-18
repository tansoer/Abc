using Abc.Data.Roles;
using Abc.Infra;
using Abc.Infra.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Roles {

    [TestClass]
    public class RoleDbTests :DbTests<RoleDb, BaseDb<RoleDb>> {
        private class testClass :RoleDb {
            public testClass(DbContextOptions<RoleDb> o) : base(o) { }
            public ModelBuilder RunOnModelCreating() {
                var set = new ConventionSet();
                var mb = new ModelBuilder(set);
                OnModelCreating(mb);
                return mb;
            }
        }
        protected override RoleDb createObject() {
            options = new DbContextOptionsBuilder<RoleDb>().UseInMemoryDatabase("TestDb").Options;
            return new RoleDb(options);
        }
        [TestMethod]
        public void InitializeTablesTest() {
            RoleDb.InitializeTables(null);
            var o = new testClass(options);
            var builder = o.RunOnModelCreating();
            RoleDb.InitializeTables(builder);
            testEntity<AssignedResponsibilityData>(builder);
            testEntity<RelationshipConstraintData>(builder);
            testEntity<RelationshipConstraintTypeData>(builder);
            testEntity<PartyRelationshipData>(builder);
            testEntity<PartyRelationshipTypeData>(builder);
            testEntity<PartyRoleConstraintData>(builder);
            testEntity<PartyRoleConstraintTypeData>(builder);
            testEntity<PartyRoleData>(builder);
            testEntity<PartyRoleTypeData>(builder);
            testEntity<ResponsibilityData>(builder);
            testEntity<ResponsibilityTypeData>(builder);
        }
        [TestMethod] public void AssignedResponsibilitiesTest() => isNullable<DbSet<AssignedResponsibilityData>>();
        [TestMethod] public void RelationshipConstraintsTest() => isNullable<DbSet<RelationshipConstraintData>>();
        [TestMethod] public void RelationshipConstraintTypesTest() => isNullable<DbSet<RelationshipConstraintTypeData>>();
        [TestMethod] public void RelationshipsTest() => isNullable<DbSet<PartyRelationshipData>>();
        [TestMethod] public void RelationshipTypesTest() => isNullable<DbSet<PartyRelationshipTypeData>>();
        [TestMethod] public void RoleConstraintsTest() => isNullable<DbSet<PartyRoleConstraintData>>();
        [TestMethod] public void RoleConstraintTypesTest() => isNullable<DbSet<PartyRoleConstraintTypeData>>();
        [TestMethod] public void RolesTest() => isNullable<DbSet<PartyRoleData>>();
        [TestMethod] public void RoleTypesTest() => isNullable<DbSet<PartyRoleTypeData>>();
        [TestMethod] public void ResponsibilitiesTest() => isNullable<DbSet<ResponsibilityData>>();
        [TestMethod] public void ResponsibilityTypesTest() => isNullable<DbSet<ResponsibilityTypeData>>();
    }
}