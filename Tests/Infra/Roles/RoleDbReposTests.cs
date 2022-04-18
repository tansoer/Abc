using Abc.Domain.Common;
using Abc.Domain.Roles;
using Abc.Infra.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Roles {

    [TestClass]
    public class RoleDbReposTests :TestsHost {
        [TestInitialize] public void TestInitialize() => type = typeof(RoleDbRepos);
        [DataTestMethod]
        [DataRow(typeof(IAssignedResponsibilitiesRepo))]
        [DataRow(typeof(IPartyRelationshipsRepo))]
        [DataRow(typeof(IPartyRelationshipTypesRepo))]
        [DataRow(typeof(IPartyRoleConstraintsRepo))]
        [DataRow(typeof(IPartyRoleConstraintTypesRepo))]
        [DataRow(typeof(IPartyRolesRepo))]
        [DataRow(typeof(IPartyRoleTypesRepo))]
        [DataRow(typeof(IRelationshipConstraintsRepo))]
        [DataRow(typeof(IRelationshipConstraintTypesRepo))]
        [DataRow(typeof(IResponsibilitiesRepo))]
        [DataRow(typeof(IResponsibilityTypesRepo))]
        public void RegisterTest(Type t) => Assert.IsNotNull(GetRepo.Instance(t));
    }
}