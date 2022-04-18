using Abc.Infra.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Roles {
    [TestClass] public class RoleDbInitializerTests :DbInitializerTests<RoleDb> {
        public RoleDbInitializerTests() {
            type = typeof(RoleDbInitializer);
            db = new RoleDb(options);
            RoleDbInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void AssignedResponsibilitiesTest() => areEqual(0, db.AssignedResponsibilities.Count());
        [TestMethod] public void RelationshipConstraintTypesTest() => areEqual(0, db.RelationshipConstraintTypes.Count());
        [TestMethod] public void RelationshipConstraintsTest() => areEqual(0, db.RelationshipConstraints.Count());
        [TestMethod] public void RelationshipTypesTest() => areEqual(0, db.RelationshipTypes.Count());
        [TestMethod] public void RelationshipsTest() => areEqual(0, db.Relationships.Count());
        [TestMethod] public void ResponsibilitiesTest() => areEqual(0, db.Responsibilities.Count());
        [TestMethod] public void ResponsibilityTypesTest() => areEqual(0, db.ResponsibilityTypes.Count());
        [TestMethod] public void RoleConstraintTypesTest() => areEqual(0, db.RoleConstraintTypes.Count());
        [TestMethod] public void RoleConstraintsTest() => areEqual(0, db.RoleConstraints.Count());
        [TestMethod] public void RoleTypesTest() => areEqual(0, db.RoleTypes.Count());
        [TestMethod] public void RolesTest() => areEqual(0, db.Roles.Count());
    }
}